using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{

    public float mouseSensitivity = 100f;
    public Transform playerBody;
    float xrotation = 0;
    public GameObject currentObject;
    bool isActive = true;
    public bool gotKey = false;
    public float distanceTointeract = 4.5f;
    [SerializeField] CinemachineVirtualCamera playercamera;

    private void OnEnable()
    {
        CameraSwitcher.Register(playercamera);
        CameraSwitcher.SwitchCamera(playercamera);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(playercamera);
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        MoveCameraAround();
        checkray();
        Interact();
    }

    void MoveCameraAround()
    {
        if (isActive)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xrotation -= mouseY;
            xrotation = Mathf.Clamp(xrotation, -90f, 90f);

            playercamera.transform.localRotation = Quaternion.Euler(xrotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
    }

    public void Interact()
    {
        if (isActive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                ActivateObject();
            }
        }
    }

    void checkray()
    {
        if (isActive)
        {
            var RayOrigin = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            RaycastHit HitInfo;
            if (Physics.Raycast(RayOrigin, out HitInfo, 100.0f) )
            {
                var selectedObject = HitInfo.transform;
                if (selectedObject.tag == "Interactable" && Vector3.Distance(transform.position, selectedObject.position) < distanceTointeract)
                {
                    currentObject = HitInfo.transform.gameObject;
                    currentObject.GetComponent<InteractableObject>().isOnView = true;
                    if (!currentObject.GetComponent<InteractableObject>().isActivated) currentObject.GetComponent<InteractableObject>().Highlight(true);
                }
                else
                {
                    if (currentObject)
                    {
                        currentObject.GetComponent<InteractableObject>().isOnView = false;
                        currentObject.GetComponent<InteractableObject>().Highlight(false);
                    }
                    currentObject = null;
                }
            }
        }
    }

    public void DeactivateObject()
    {
        if(currentObject)
        {
            TooglePlayerMovement(true);
            CameraSwitcher.SwitchCamera(playercamera);
        }
    }

    private void ActivateObject()
    {
        if (currentObject)
        {
            currentObject.GetComponent<InteractableObject>().Interact();
            if (currentObject.name != "Key" && currentObject.name != "door") TooglePlayerMovement(false);   
        }
        
    }

    void TooglePlayerMovement(bool movement)
    {
        isActive = movement;
        playerBody.GetComponent<PlayerMovement>().ToogleMovement(movement);
        if (movement) Cursor.lockState = CursorLockMode.Locked;
        else Cursor.lockState = CursorLockMode.None;
    }
}
