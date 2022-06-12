using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableObject
{
    public string secondaryText;
    public bool activeDoor = false;
    public override void CloseInteraction()
    {
    }

    public void ActivateDoorToExit()
    {
        string flagMessage = interactText;
        interactText = secondaryText;
        secondaryText = flagMessage;
        activeDoor = true;
    }
    public override void Interact()
    {
        if (!activeDoor) return;
        Highlight(false);
        GameObject.Find("Canvas").GetComponent<LibrarySceneCanvasManager>().ShowCurtain();
    }
}
