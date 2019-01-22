using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {
    Transform player;
    public Vector3 offset;
    public Vector2 horizontalLimits;
    public Vector2 verticalLimits;
    public float speed;
	void Start () {
        player = Transform.FindObjectOfType<PlayerControllerSuperTwoD>().transform;
	}

	void Update () {
        Vector3 newPosition = player.position + offset;
        if (newPosition.x <= horizontalLimits.x)
            newPosition.x = horizontalLimits.x;
        if (newPosition.x >= horizontalLimits.y)
            newPosition.x = horizontalLimits.y;
        if (newPosition.z <= verticalLimits.x)
            newPosition.z = verticalLimits.x;
        if (newPosition.z >= verticalLimits.y)
            newPosition.z = verticalLimits.y;
        transform.position = Vector3.Lerp(transform.position, newPosition, speed);
	}

    public void SetTarget(Transform transform)
    {
        Debug.Log("New target adquired " + transform.gameObject.name);
        player = transform;
    }
}
