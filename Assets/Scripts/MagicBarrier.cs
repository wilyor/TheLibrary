using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBarrier : MonoBehaviour
{
    public ParticleSystem paperDust;
    public bool active = true;
    void Start()
    {
        paperDust?.Play();
    }

    public void DestroyBarrier (){
        GetComponent<CapsuleCollider>().enabled = false;
        paperDust?.Stop();
    }
}
