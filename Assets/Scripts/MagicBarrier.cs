using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBarrier : MonoBehaviour
{
    public ParticleSystem [] paperDust;
    public bool active = true;
    void Start()
    {
        foreach (ParticleSystem particle in paperDust)
        {
            particle?.Play();
        }
    }

    public void DestroyBarrier (){
        GetComponent<CapsuleCollider>().enabled = false;
        foreach(ParticleSystem particle in paperDust)
        {
            particle?.Stop();
        }
    }
}
