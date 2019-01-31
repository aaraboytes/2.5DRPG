using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] hpImages = new GameObject[5];
    public GameObject hpHolder;
    PlayerControllerSuperTwoD player;
    int life;
    public static HPManager _instance;
    private void Awake()
    {
        _instance = this;
    }
    private void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            hpImages[i] = hpHolder.transform.GetChild(i).gameObject;
        }
        player = FindObjectOfType<PlayerControllerSuperTwoD>();
        life = player.GetCurrentHealth();
        for(int i = 5; i < life - 1; i++)
        {
            hpImages[i].SetActive(false);
        }
    }
    public void DamageToPlayer()
    {
        life--;
        for (int i = 4; i < life-1; i++)
        {
            hpImages[i].SetActive(false);
        }
    }
    public void TurnOn()
    {
        hpHolder.SetActive(true);
    }
}
