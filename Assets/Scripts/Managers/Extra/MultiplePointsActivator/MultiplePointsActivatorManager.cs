using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplePointsActivatorManager : MonoBehaviour
{
    public static MultiplePointsActivatorManager _instance;
    public int numberOfPointsToActivate;
    public GameEvent eventToBeActivated;
    int currentPoints = 0;
    private void Awake()
    {
        _instance = this;
    }
    public void RegisterNewPoint()
    {
        currentPoints++;
        if(currentPoints == numberOfPointsToActivate)
        {
            Debug.Log("new point registered");
            eventToBeActivated.StartEvent();
        }
    }
}
