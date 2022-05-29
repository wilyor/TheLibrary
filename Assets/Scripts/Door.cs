using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public string secondaryText;
    public override void CloseInteraction()
    {
    }

    public void ActivateDoorToExit()
    {
        string flagMessage = interactText;
        interactText = secondaryText;
        secondaryText = flagMessage;
    }
    public override void Interact()
    {
        Highlight(false);
        GameObject.Find("Canvas").GetComponent<LibrarySceneCanvasManager>().ShowCurtain();
    }
}
