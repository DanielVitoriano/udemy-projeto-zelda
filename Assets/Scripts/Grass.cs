using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    public ParticleSystem particle;
    private bool isCut;

    void Hit(int damage)
    {
        if (!isCut)
        {
            isCut = true;
            transform.localScale = new Vector3(1f, 1f, 1f);
            particle.Emit(10);
        }
       
    }
}
