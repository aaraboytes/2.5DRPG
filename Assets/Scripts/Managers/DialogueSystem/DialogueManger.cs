using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManger : MonoBehaviour {
    public static DialogueManger _instance;
    [Header("UI")]
    public GameObject dialogueBox;
    public Text name;
    public Text message;
    public Image photo;
    
    AudioSource audio = null;
    AudioClip clip;

    NPC currentNPC = null;
    bool isTalking = false;

    //FIFO collections
    Queue<GameEvent> events;
    Queue<Dialogue> dialogues;

    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        audio = GetComponent<AudioSource>();
        dialogues = new Queue<Dialogue>();
        events = new Queue<GameEvent>();
        dialogues.Clear();
        dialogueBox.SetActive(false);
	}
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X) && isTalking)
        {
            NextSentence();
        }
    }
    #region Conversation methods
    public void StartConversation(Dialogue dialogue)
    {
        //Initialize
        PlayerControllerSuperTwoD player = FindObjectOfType<PlayerControllerSuperTwoD>();
        if (player)
        {
            player.paused = true;
            player.gameObject.GetComponent<Animator>().SetBool("moving", false);
        }
        ResetDialogueSystem();
        //Setup in UI
        dialogueBox.SetActive(true);
        
        //Add sentences to FIFO collection
        foreach (Dialogue d in dialogues)
        {
            dialogues.Enqueue(d);
        }
        //Start with first dialogue
        NextSentence();
        isTalking = true;
    }
    public void NextSentence()
    {
        if (dialogues.Count == 0)
        {
            Debug.Log("Conversation has ended");
            EndConversation();
            return;
        }
        
        Dialogue dialogue = dialogues.Dequeue();
        name.text = dialogue.name;
        photo.sprite = dialogue.photo;
        clip = dialogue.audioClip;

        audio.clip = clip;
        audio.Play();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(dialogue.sentence));
    }
    void EndConversation()
    {
        //Clean sentences
        dialogues.Clear();

        dialogueBox.SetActive(false);
        PlayerControllerSuperTwoD player = FindObjectOfType<PlayerControllerSuperTwoD>();
        if (player)
            player.paused = false;
        isTalking = false;

        //End Conversation with npc
        if (currentNPC != null)
            currentNPC.EndTalking();
        currentNPC = null;
        
        //Make an event
        if (events.Count>0)
        {
            ResetDialogueSystem();
            Invoke("ExecuteEvent", 0.5f);
        }

        //End audio if is still playing
        audio.Stop();
    }
    IEnumerator TypeSentence(string sentence)
    {
        message.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            message.text += letter;
            yield return null;
        }
        audio.Stop();
    }
    public void ResetDialogueSystem()
    {
        dialogues = new Queue<Dialogue>();
        dialogueBox.SetActive(false);
    }
    #endregion
    #region Events Management
    public void AddEvent(GameEvent gameEvent)
    {
        events.Enqueue(gameEvent);
    }
    void ExecuteEvent()
    {
        GameEvent currentEvent = events.Dequeue();
        currentEvent.StartEvent();
    }
    void CleanEvents()
    {
        events.Clear();
    }
    #endregion
    #region Extra
    public void SetNPC(NPC _currentNPC)
    {
        currentNPC = _currentNPC;
    }
    #endregion
}
