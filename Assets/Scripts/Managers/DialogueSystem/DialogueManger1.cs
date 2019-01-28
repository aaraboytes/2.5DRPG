using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManger1 : MonoBehaviour {
    public static DialogueManger1 _instance;
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
    Queue<string> sentences;

    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        audio = GetComponent<AudioSource>();
        sentences = new Queue<string>();
        events = new Queue<GameEvent>();
        sentences.Clear();
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
        name.text = dialogue.name;
        photo.sprite = dialogue.photo;
        clip = dialogue.audioClip;
        //Add sentences to FIFO collection
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        //Start with first dialogue
        NextSentence();
        isTalking = true;
    }
    public void NextSentence()
    {
        if (sentences.Count == 0)
        {
            Debug.Log("Conversation has ended");
            EndConversation();
            return;
        }
        audio.clip = clip;
        audio.Play();
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
    void EndConversation()
    {
        //Clean sentences
        sentences.Clear();

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
        sentences = new Queue<string>();
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
