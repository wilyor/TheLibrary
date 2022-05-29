using Cinemachine;
using UnityEngine;
using UnityEngine.Events;

public class GlobeManager : InteractableObject
{
    [SerializeField] CinemachineVirtualCamera roofCamera;
    public GameObject secondFloorLibrary;
    [SerializeField] float keyRotation;
    [SerializeField] UnityEvent onCompleteEvent;
    public int[] password;

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
        bool completed = true;
        int [] currentPassword = secondFloorLibrary.GetComponent<SecondFloorManager>().password;
        for (int i = 0; i < currentPassword.Length; i++)
        {
            if (currentPassword[i] != password[i])
            {
                completed = false;
                break;
            }
        }
        if (completed)
        {
            onCompleteEvent?.Invoke();
        }
    }
}
