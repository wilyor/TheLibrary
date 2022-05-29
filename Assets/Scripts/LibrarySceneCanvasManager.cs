using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibrarySceneCanvasManager : MonoBehaviour
{
    public GameObject curtain;
    Animator anim;
    void Start()
    {
        anim = curtain.GetComponent<Animator>();
    }
    public void HideCurtain()
    {
        curtain.SetActive(false);
    }

    public void ShowCurtain(){
        curtain.SetActive(true);
        anim.SetTrigger("Activate");
    }
}
