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
        else
        {
            if (Input.GetMouseButtonDown(1))
            {
                TooglePlayerMovement(true);
                DeactivateObject();
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
                if (selectedObject.name == "Book" && Vector3.Distance(transform.position, selectedObject.position) < 3.5f)
                {
                    currentObject = HitInfo.transform.gameObject;
                    currentObject.GetComponent<BookBehaviour>().isOnView = true;
                }
                else
                {
                    if (currentObject) currentObject.GetComponent<BookBehaviour>().isOnView = false;
                    currentObject = null;
                }
            }
        }
    }
    void DeactivateObject()
    {
        if(currentObject)
        {
            currentObject.GetComponent<BookBehaviour>().OpenBook();
            CameraSwitcher.SwitchCamera(playercamera);
        }
    }

    private void ActivateObject()
    {
        if (currentObject)
        {
            currentObject.GetComponent<BookBehaviour>().OpenBook();
            TooglePlayerMovement(false);
            CameraSwitcher.SwitchCamera(bookCamera);
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