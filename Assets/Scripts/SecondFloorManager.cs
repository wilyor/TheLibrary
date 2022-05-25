using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondFloorManager : MonoBehaviour
{

    bool moving = false;
    Quaternion currentAngle = Quaternion.Euler(0, 90f, 0);
    float currentYRotation = 90;
    public float timeToMove = 1;
    public float speedToMove = 0.05f;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90f, 0);
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, currentAngle, speedToMove);
    }

    public void RotateBody(float movementAngle)
    {
        if (moving) return;
        currentYRotation += movementAngle;
        currentAngle = Quaternion.Euler(0, currentYRotation, 0);
        StartCoroutine(MovingDelay());
    }

    IEnumerator MovingDelay()
    {
        moving = true;
        yield return new WaitForSeconds(timeToMove);
        moving = false;
    }
}
