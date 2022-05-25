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
        isActivated = true;
        Highlight(false);
        CameraSwitcher.SwitchCamera(roofCamera);
        interactionCanvas?.SetActive(true);
    }

    public override void CloseInteraction()
    {
        interactionCanvas?.SetActive(false);
        isActivated = false;
    }
}
