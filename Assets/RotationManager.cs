using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationManager : MonoBehaviour
{
    public float speed;
    void Update()
    {
        transform.Rotate(Vector3.left * Time.deltaTime * speed);
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speed);
    }
}
