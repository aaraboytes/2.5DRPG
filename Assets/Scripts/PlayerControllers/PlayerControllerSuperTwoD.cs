using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSuperTwoD : MonoBehaviour {
    public float speed;
    public int health;

    int currentHealth = 0;
    CharacterController body;
    Vector3 movement;
    Vector3 direction;
    Animator anim;

    public bool paused;
    [SerializeField]
    List<int> items = new List<int>();
    Inventory inventory;
    private void Start()
    {
        body = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        GameManager._instance.SetCurrentPlayer(this);
        GameManager._instance.LoadSavedElements();
        inventory = FindObjectOfType<Inventory>();
    }
    void Update() {
        transform.LookAt(Camera.main.transform);
        if (!paused)
        {
            #region Player movement
            //Input
            movement = (Vector3.forward * Input.GetAxisRaw("Vertical")) + (Vector3.right * Input.GetAxisRaw("Horizontal"));
            //Reinsert gravity and add speed
            movement = movement.normalized * speed * Time.deltaTime;
            if (body.isGrounded)
                movement.y = 0;
            else
                movement.y = Physics.gravity.y;
            //Move
            body.Move(movement);
            #endregion
            #region Player direction
            //Direction
            if (movement.z > 0)
                direction = Vector3.forward;
            else if (movement.z < 0)
                direction = -Vector3.forward;
            else if (movement.x > 0)
                direction = Vector3.right;
            else if (movement.x < 0)
                direction = -Vector3.right;
            #endregion
            #region Interact with objects
            if (Input.GetKeyDown(KeyCode.Z))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position,direction,out hit,0.5f))
                {
                    GameObject target = hit.collider.gameObject;
                    //Interact with interactuable objs
                    if (target.GetComponent<Interactuable>())
                    {
                        if (target.GetComponent<Activable>())
                        {
                            Activable act = target.GetComponent<Activable>();
                            act.Activate();
                        }
                        //Speak with NPCs
                        if (target.GetComponent<NPC>())
                        {
                            NPC currentNPC = target.GetComponent<NPC>();
                            currentNPC.StartToTalk();
                            DialogueManger._instance.SetNPC(currentNPC);
                        }
                        //Speak with Items and take them
                        if (target.GetComponent<TookeableItem>())
                        {
                            TookeableItem tookeableItem = target.GetComponent<TookeableItem>();
                            if (inventory.AddItem())
                            {
                                AddItem(tookeableItem.item.id);
                                inventory.ClearInventory();
                                inventory.InitializeInventory();
                                tookeableItem.SetSuccessDialogue();
                            }
                            else
                                tookeableItem.SetFailureDialogue();
                        }
                        //Try to open a door
                        if (target.GetComponent<Door>())
                        {
                            Door currentDoor = target.GetComponent<Door>();
                            if (currentDoor.isDoorOpened)
                            {
                                currentDoor.PassDoor();
                            }
                        }
                        Interactuable currentInteractuableObj = target.GetComponent<Interactuable>();
                        currentInteractuableObj.InitConversation();
                    }else if (target.GetComponent<Activable>())
                    {
                        Activable activable = target.GetComponent<Activable>();
                        activable.Activate();
                    }
                }
            }
            #endregion
        }
        #region Animation parameters
        //Animation
        anim.SetBool("moving", Mathf.Abs(movement.x) + Mathf.Abs(movement.z) != 0);
        anim.SetFloat("xVel", -movement.x);
        anim.SetFloat("yVel", movement.z);
        #endregion
    }
    #region Save data methods
    public int GetHealth()
    {
        return currentHealth;
    }
    public void SetHealth(int _currentHealth)
    {
        currentHealth = _currentHealth;
    }
    public int GetNumberOfItems()
    {
        int count = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] != 0) count++;
        }
        return count;
    }
    public int[] GetItems()
    {
        int[] currentItems = new int[20];
        for(int i = 0; i < items.Count; i++)
        {
            currentItems[i] = items[i];
        }
        return currentItems;
    }
    public void SetItems(int[] _items)
    {
        items.Clear();
        for(int i = 0; i < _items.Length; i++)
        {
            items.Add(_items[i]);
        }
    }
    public void DropItem(int id)
    {
        int index = 0;
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == id)
            {
                index = i;
            }
        }
        items[index] = 0;
    }
    public void AddItem(int id)
    {
        items[GetNumberOfItems()] = id;
    }
    #endregion
}
