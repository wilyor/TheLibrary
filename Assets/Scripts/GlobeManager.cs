using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeManager : InteractableObject
{
    [SerializeField] CinemachineVirtualCamera roofCamera;

    private void OnEnable()
    {
        CameraSwitcher.Register(roofCamera);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(roofCamera);
    }
    private void Update()
    {
        if (isOnView && !isActivated) Highlight(true);
    }
    public override void Initialize()
    {
        
    }

    public override void Interact()
    {
        isActivated = !isActivated;
        Highlight(false);
        CameraSwitcher.SwitchCamera(roofCamera);
    }
}
