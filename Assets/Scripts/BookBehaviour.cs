using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : InteractableObject
{
    public GameObject gameBoard;
    [SerializeField] CinemachineVirtualCamera bookCamera;

    private void OnEnable()
    {
        CameraSwitcher.Register(bookCamera);
    }

    private void OnDisable()
    {
        CameraSwitcher.Unregister(bookCamera);
    }
    void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (isOnView && !isActivated) Highlight(true);
    }

    public override void Initialize()
    {
        anim = GetComponent<Animator>();
        gameBoard.SetActive(false);
    }

    public override void Interact()
    {
        isActivated = true;
        anim.SetBool("Open", isActivated);
        Highlight(false);
        CameraSwitcher.SwitchCamera(bookCamera);
        interactionCanvas?.SetActive(true);
    }

    public override void CloseInteraction()
    {
        isActivated = false;
        anim.SetBool("Open", isActivated);
        Highlight(false);
        interactionCanvas?.SetActive(false);
    }

    public void ShowBoard()
    {
        if (gameBoard)
        {
            gameBoard.SetActive(true);
        }
    }

    public void HideBoard()
    {
        if (gameBoard)
        {
            gameBoard.SetActive(false);
        }
    }


}
