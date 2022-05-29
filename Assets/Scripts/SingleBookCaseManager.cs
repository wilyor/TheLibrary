using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleBookCaseManager : MonoBehaviour
{
    public int value = 3;
    Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void AddValue()
    {
        value++;
        if (value > 3) value = 1;
        anim.SetInteger("visibleBookCases", value);
    }
}
