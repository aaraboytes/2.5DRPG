using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogueManger : MonoBehaviour {
    public static DialogueManger _instance;

    public GameObject dialogueBox;
    public Text name;
    public Text message;
    public Image photo;
    NPC currentNPC = null;
    bool isTalking = false;

    Queue<string> sentences; //FIFO collection

    private void Awake()
    {
        _instance = this;
    }
    void Start () {
        sentences = new Queue<string>();
        dialogueBox.SetActive(false);
	}
    public void SetNPC(NPC _currentNPC)
    {
        currentNPC = _currentNPC;
    }
    public void StartConversation(Dialogue dialogue)
    {
        FindObjectOfType<PlayerControllerSuperTwoD>().paused = true;
        FindObjectOfType<PlayerControllerSuperTwoD>().gameObject.GetComponent<Animator>().SetBool("moving", false);

        dialogueBox.SetActive(true);
        
        name.text = dialogue.name;
        photo.sprite = dialogue.photo;
        isTalking = true;

        sentences.Clear();      //Clear past sentences
        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
    }
    private void Update()
    {
        if (isTalking)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                NextSentence();
            }
        }
    }
    public void NextSentence()
    {
        if(sentences.Count == 0)
        {
            EndConversation();
            return;
        }
        string sentence = sentences.Dequeue();
        message.text = sentence;
    }
    void EndConversation()
    {
        dialogueBox.SetActive(false);
        FindObjectOfType<PlayerControllerSuperTwoD>().paused = false;
        isTalking = false;
        //End Conversation with npc
        if (currentNPC != null)
            currentNPC.EndTalking();
        currentNPC = null;
        //Clean sentences
        sentences.Clear();
    }
}
