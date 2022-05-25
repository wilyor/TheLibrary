using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public abstract class InteractableObject : MonoBehaviour
{
    public bool isActivated;
    public bool isOnView = false;
    protected Animator anim;
    public string interactText;
    public GameObject UIInteractionText;
    public GameObject interactionCanvas;

    public abstract void Initialize();
    public abstract void Interact();
    public void Highlight(bool status)
    {
        if (status)
        {
            ShowInteractionText();
        }
        else
        {
            HideInteractionText();
        }
    }

    public void ShowInteractionText()
    {
        if (UIInteractionText)
        {
            UIInteractionText.SetActive(true);
            UIInteractionText.GetComponent<TextMeshProUGUI>().SetText(interactText);
        }
    }

    public void HideInteractionText()
    {
        if (UIInteractionText)
        {
            UIInteractionText.SetActive(false);
        }
    }

    public abstract void CloseInteraction();
}
