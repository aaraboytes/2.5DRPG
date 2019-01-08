using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateItemEffect : MonoBehaviour
{
    public static ActivateItemEffect _instance;
    private void Awake()
    {
        if (_instance == null)
            _instance = this;
    }
    public bool Activate(Item item)
    {
        PlayerControllerSuperTwoD player = FindObjectOfType<PlayerControllerSuperTwoD>();
        switch (item.id)
        {
            //Apple
            case 1:
                int currentHealth = player.GetHealth();
                if (currentHealth != player.health)
                    currentHealth++;
                player.SetHealth(currentHealth);
                return true;
                break;
            //Poison
            case 2:
                player.SetHealth(player.GetHealth() - 1);
                return true;
                break;
            //Portal
            case 3:
                player.transform.position = new Vector3(0, 10f, 0);
                break;
        }
        return false;
    }
}
