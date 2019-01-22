using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProximityMessage : MonoBehaviour
{
    public float maxDistance;
    public float minDistance;
    public Text messageText;
    public Image messageImage;
    Transform player;
    float mag;
    float extra;
    private void Start()
    {
        player = FindObjectOfType<PlayerControllerSuperTwoD>().transform;
        mag = maxDistance - minDistance;
        extra = minDistance / mag;
    }
    private void Update()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        Color imageColor = messageImage.color;
        Color textColor = messageText.color;
        //Calculate new alpha
        float alpha;
        if(distance > minDistance)
        {
            alpha = 1 - (distance/ mag) + extra;
        }
        else
        {
            alpha = 1;
        }
        //Set new alpha to local color variables
        imageColor.a = alpha;
        textColor.a = alpha;
        //Update color variables
        messageImage.color = imageColor;
        messageText.color = textColor;
    }
}
