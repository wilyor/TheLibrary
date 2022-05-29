using System.Collections;
using UnityEngine;

public class SecondFloorManager : MonoBehaviour
{

    bool moving = false;
    Quaternion currentAngle = Quaternion.Euler(0, 90f, 0);
    float currentYRotation = 90;
    public float timeToMove = 1;
    public float speedToMove = 0.05f;
    public GameObject[] BookCases;
    int[] password;

    void Start()
    {
        transform.rotation = Quaternion.Euler(0, 90f, 0);
        password = new int[BookCases.Length];
    }

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, currentAngle, speedToMove);
    }

    public void ActivateBookCase(int index)
    {
        BookCases[index]?.GetComponent<SingleBookCaseManager>().AddValue();
        for (int i = 0; i < password.Length; i++)
        {
            password[i] = BookCases[index].GetComponent<SingleBookCaseManager>().value;
        }
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

    public float GetCurrentRotation()
    {
        return currentYRotation;
    }
}
