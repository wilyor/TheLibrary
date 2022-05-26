using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GlobeManager : InteractableObject
{
    [SerializeField] CinemachineVirtualCamera roofCamera;
    public GameObject secondFloorLibrary;
    [SerializeField] float keyRotation;
    [SerializeField] UnityEvent onCompleteEvent;

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
        float currentRot = secondFloorLibrary.GetComponent<SecondFloorManager>().GetCurrentRotation();
        if (currentRot == keyRotation)
        {
            onCompleteEvent?.Invoke();
            Debug.Log("Completed");
        }
    }
}
