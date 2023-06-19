using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private CapsuleCollider capsuleCollider;
    private Animator anim;
    [SerializeField]int health;
    public ParticleSystem fxHit;
    bool isDIe;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    IEnumerator Died()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }

    void Hit(int damage)
    {
        if (isDIe) return;

        if(health > 0)
        {
            health -= damage;
            anim.SetTrigger("getHit");
            fxHit.Emit(damage + health);
        }
        else
        {
            isDIe = true;
            anim.SetTrigger("Die");
            capsuleCollider.enabled = false;
            StartCoroutine("Died");
        }
    }
}
