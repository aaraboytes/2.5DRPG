using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSuperTwoD : MonoBehaviour {
    public float speed;
    public int health;

    int currentHealth = 0;
    Rigidbody body;
    Vector3 movement;
    Vector3 direction;
    Animator anim;
    public bool paused;
    [SerializeField]
    List<int> items = new List<int>();
    Inventory inventory;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        GameManager._instance.SetCurrentPlayer(this);
        GameManager._instance.LoadSavedElements();
        inventory = FindObjectOfType<Inventory>();
    }
    void Update() {
        if (!paused)
        {
            transform.LookAt(Camera.main.transform);
            #region Player movement
            //Input
            movement = (Vector3.forward * Input.GetAxisRaw("Vertical")) + (Vector3.right * Input.GetAxisRaw("Horizontal"));
            //Direction
            anim.SetBool("moving", Mathf.Abs(movement.x) + Mathf.Abs(movement.z) != 0);
            anim.SetFloat("xVel", -movement.x);
            anim.SetFloat("yVel", movement.z);
            if (movement.z > 0)
                direction = Vector3.forward;
            else if (movement.z < 0)
                direction = -Vector3.forward;
            else if (movement.x > 0)
                direction = Vector3.right;
            else if (movement.x < 0)
                direction = -Vector3.right;
            //Move
            body.velocity = movement * speed;
            #endregion

            #region Interact with objects
            if (Input.GetKeyDown(KeyCode.Z))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position,direction,out hit,0.5f))
                {
                    Debug.Log(hit.collider.name);
                    if (hit.collider.gameObject.GetComponent<Interactuable>())
                    {
                        if (hit.collider.gameObject.GetComponent<NPC>())
                        {
                            NPC currentNPC = hit.collider.gameObject.GetComponent<NPC>();
                            currentNPC.StartToTalk();
                            DialogueManger._instance.SetNPC(currentNPC);
                        }
                        if (hit.collider.gameObject.GetComponent<TookeableItem>())
                        {
                            TookeableItem tookeableItem = hit.collider.gameObject.GetComponent<TookeableItem>();
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
                        Interactuable currentInteractuableObj = hit.collider.gameObject.GetComponent<Interactuable>();
                        DialogueManger._instance.StartConversation(currentInteractuableObj.dialogue);
                    }
                }
            }
            #endregion
        }
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
