using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : InteractableObject
{
    public override void CloseInteraction()
    {
        throw new System.NotImplementedException();
    }

    public override void Interact()
    {
        Highlight(false);
        GameObject.Find("Cameras/Camera").GetComponent<MouseLook>().gotKey = true;
        GameObject.Find("Hall/door").GetComponent<Door>().ActivateDoorToExit();
        Destroy(gameObject);
    }
}
