using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookBehaviour : MonoBehaviour
{
    public bool isOpen;
    public bool isOnView = false;
    public GameObject outlineBook;
    public GameObject gameBoard;
    Animator anim;    

    void Start()
    {
        anim = GetComponent<Animator>();
        gameBoard.SetActive(false);
    }

    private void Update()
    {
        if (isOnView && !isOpen) Highlight(true);
        else
        {
            Highlight(false);
        }
    }
    public void OpenBook()
    {
        isOpen = !isOpen;
        anim.SetBool("Open", isOpen);
        Highlight(false);
    }

    public void Highlight(bool active)
    {
         outlineBook.SetActive(active);
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
