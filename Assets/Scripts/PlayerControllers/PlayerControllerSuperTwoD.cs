using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerSuperTwoD : MonoBehaviour {
    public float speed;
    public float maxSpeed;
    public float walkTime;
    public int health;
    public GameObject z;

    int currentHealth = 0;
    CharacterController body;
    Vector3 movement;
    Vector3 direction;
    float timerWalking = 0;
    Animator anim;
    bool hitting = false;

    [Header("Damage")]
    float mass = 3.0f;
    bool damaged = false;

    public bool paused;
    [SerializeField]
    List<int> items = new List<int>();
    public Item launcheableItem;
    public GameObject bottle;
    Vector3 launchDirection;
    Inventory inventory;

    private void Start()
    {
        currentHealth = health;
        body = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        GameManager._instance.SetCurrentPlayer(this);
        GameManager._instance.LoadSavedElements();
        HPManager._instance.SetCurrentHealth(currentHealth);
        inventory = FindObjectOfType<Inventory>();
    }
    void Update() {
        transform.LookAt(Camera.main.transform);
        if (!paused)
        {
            #region Player movement
            if (!damaged)
            {
                //Input
                movement = (Vector3.forward * Input.GetAxisRaw("Vertical")) + (Vector3.right * Input.GetAxisRaw("Horizontal"));
                if (movement != Vector3.zero)
                {
                    launchDirection = movement;
                }
                movement = movement.normalized * speed * Time.deltaTime;
                if (movement != Vector3.zero)
                {
                    timerWalking += Time.deltaTime;
                    if (timerWalking >= walkTime)
                        movement *= maxSpeed;
                }
                else
                    timerWalking = 0;
            }
            else
            {
                if (movement.magnitude < 0.2)
                    damaged = false;
                movement = Vector3.Lerp(movement, Vector3.zero, 5 * Time.deltaTime);
            }
            //Add gravity
            if (body.isGrounded)
                movement.y = 0;
            else
                movement.y = Physics.gravity.y * Time.deltaTime;
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
            RaycastHit hit;
            if(Physics.Raycast(transform.position,direction,out hit, 0.5f))
            {
                GameObject target = hit.collider.gameObject;
                if (target.GetComponent<Interactuable>() || target.GetComponent<Activable>())
                {
                    if (!z.activeInHierarchy)
                    {
                        z.SetActive(true);
                    }
                    hitting = true;
                }
            }
            else
            {
                if (hitting)
                {
                    hitting = false;
                }
            }
            if(z.activeInHierarchy)
                z.GetComponent<Animator>().SetBool("Visible", hitting);

            if (Input.GetKeyDown(KeyCode.Z) && hitting)
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
            }else if(Input.GetKeyDown(KeyCode.Z) && !hitting)
            {
                //Check if there are bottles in inventory
                if (FindItem(launcheableItem.id))
                {
                    //Launch it
                    GameObject currentBottle = Instantiate(bottle);
                    currentBottle.transform.position = transform.position + launchDirection * 0.3f;
                    currentBottle.GetComponent<Rigidbody>().AddForce(launchDirection * 5.0f + Vector3.up * 3.0f,ForceMode.Impulse);
                    DropItem(launcheableItem.id);
                }
            }
            #endregion
        }
        #region Animation parameters
        //Animation
        if(paused)
            anim.SetBool("moving", false);
        else
            anim.SetBool("moving", Mathf.Abs(movement.x) + Mathf.Abs(movement.z) != 0);
        anim.SetInteger("right", (int)-Input.GetAxisRaw("Horizontal"));
        anim.SetInteger("up", (int)Input.GetAxisRaw("Vertical"));
        #endregion
    }
    #region Gameplay methods
    #region Health
    public int GetHealth()
    {
        return currentHealth;
    }
    public void SetHealth(int _currentHealth)
    {
        currentHealth = _currentHealth;
    }
    public int GetCurrentHealth()
    {
        return currentHealth;
    }
    public void MakeHeal()
    {
        currentHealth++;
        HPManager._instance.TurnOn();
        HPManager._instance.HealPlayer();
    }
    public void MakeDamage(Vector3 dir, float force)
    {
        HPManager._instance.TurnOn();
        currentHealth--;
        HPManager._instance.DamageToPlayer();
        if(currentHealth == 0)
        {
            Die();
        }
        AddImpact(dir, force);
    }
    public void AddImpact(Vector3 dir,float force)
    {
        damaged = true;
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y;
        movement += dir.normalized * force / mass;
    }
    void Die()
    {
        paused = true;
        anim.SetTrigger("Dies");
    }
    #endregion
    #region Attack
    #endregion
    public void GoToGameOverScene()
    {
        GameManager._instance.GameOverScene();
    }
    #endregion
    #region Inventory methods
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
    public bool FindItem(int id)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] == id)
                return true;
        }
        return false;
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
        inventory.ClearInventory();
        inventory.InitializeInventory();
    }
    public void AddItem(int id)
    {
        items[GetNumberOfItems()] = id;
        inventory.ClearInventory();
        inventory.InitializeInventory();
    }
    #endregion
}
