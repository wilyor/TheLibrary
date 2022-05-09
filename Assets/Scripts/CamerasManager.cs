using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamerasManager : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playercamera;
    [SerializeField] CinemachineVirtualCamera bookCamera;

    private void OnEnable()
    {
        CameraSwitcher.Register(playercamera);
        CameraSwitcher.Register(bookCamera);
        CameraSwitcher.SwitchCamera(playercamera);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(playercamera);
        CameraSwitcher.Unregister(bookCamera);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CameraSwitcher.IsActivecamera(playercamera))
            {
                CameraSwitcher.SwitchCamera(bookCamera);
            }
            else if (CameraSwitcher.IsActivecamera(bookCamera))
            {
                CameraSwitcher.SwitchCamera(playercamera);
            }
        }
    }
}
