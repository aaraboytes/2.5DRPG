using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySlot : MonoBehaviour
{
    public Item slottedItem = null;
    public void DropThisItem()
    {
        slottedItem = null;
    }
}
