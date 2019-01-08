using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public int maxCarryElements;
    [SerializeField]
    int currentCarryElements;
    [SerializeField]
    List<Item> items = new List<Item>();
    public Item[] allItems;
    public Sprite defaultBGImage;

    public Image infoImage;
    public Text infoText;

    public GameObject slotsHolder;
    GameObject[] slots = new GameObject[20];
    PlayerControllerSuperTwoD player;

    Button currentSelected;

    void Awake()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
    }
    private void Start()
    {
       
    }
    public void InitializeInventory()
    {
        UpdateInventory();
        currentSelected = slots[0].GetComponent<Button>();
        SetItemInfo();
    }
    public bool AddItem()
    {
        //Add item only if the inventory isnt filled
        if (currentCarryElements < maxCarryElements)
        {
            return true;
        }
        return false;
    }

    public void SelectItem(Button self)
    {
        InventorySlot invItem = self.GetComponent<InventorySlot>();
        if (invItem.slottedItem != null)
        {
            currentSelected = self;
            SetItemInfo();
        }
        
    }
    void SetItemInfo()
    {
        InventorySlot invItem =currentSelected.GetComponent<InventorySlot>();
        if (invItem.slottedItem != null)
        {
            infoImage.sprite = invItem.slottedItem.itemImg;
            infoText.text = invItem.slottedItem.description;
        }
        else
        {
            infoImage.sprite = defaultBGImage;
            infoText.text = "";
        }
    }
    public void UseItem()
    {
        InventorySlot invItem = currentSelected.GetComponent<InventorySlot>();
        if (invItem.slottedItem == null)
            return;
        ActivateItemEffect._instance.Activate(invItem.slottedItem);
        DropItem();
    }
    public void DropItem()
    {
        InventorySlot invItem = currentSelected.GetComponent<InventorySlot>();
        if (invItem.slottedItem == null)
            return;
        player.DropItem(invItem.slottedItem.id);
        ClearInventory();
        InitializeInventory();
    }
    public void ClearInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            InventorySlot currentInventorySlot = slots[i].GetComponent<InventorySlot>();
            if (currentInventorySlot.slottedItem != null)
                currentInventorySlot.DropThisItem();
            slots[i].GetComponent<Image>().sprite = defaultBGImage;
        }
        items.Clear();
    }
    public void UpdateInventory()
    {
        #region Fill list inventory
        //Get all items and current player items ids
        int[] itemsIds = player.GetItems();
        //Count current carry elements
        currentCarryElements = player.GetNumberOfItems();
        Debug.Log("Current carry elements are " + currentCarryElements);
        //Fill the list with only the items that the player currently have
        for (int i = 0; i < maxCarryElements; i++)
        {
            for (int k = 0; k < allItems.Length; k++)
            {
                if (itemsIds[i] == allItems[k].id)
                {
                    Debug.Log("Adding a " + allItems[k].name+"\nThe current i number is "+i+"\nThe current k number is "+k);
                    items.Add(allItems[k]);
                }
            }
        }

        #endregion
        #region Fill UI inventory
        //Fill the slots with sprites
        //Get all slots of the UI
        for (int i = 0; i < maxCarryElements; i++)
        {
            slots[i] = slotsHolder.transform.GetChild(i).gameObject;
        }
        for (int i = 0; i < maxCarryElements; i++)
        {
            Image currentImg = slots[i].GetComponent<Image>();
            currentImg.sprite = defaultBGImage;
            if (i < currentCarryElements)
            {
                currentImg.sprite = items[i].itemImg;
                slots[i].GetComponent<InventorySlot>().slottedItem = items[i]; 
            }
        }
        #endregion
    }
}
