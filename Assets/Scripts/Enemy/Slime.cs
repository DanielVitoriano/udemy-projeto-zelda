using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Slime : MonoBehaviour
{
    private GameManager gameManager;

    private CapsuleCollider capsuleCollider;
    private Animator anim;
    [SerializeField]int health;
    public ParticleSystem fxHit;
    bool isDIe;
    public const float idleWaitTime = 3f;
    public const float patrolWaitTime = 15f;
    private enemyState state;
    public enemyState State { get => state; set => state = value; }

    private NavMeshAgent navMeshAgent;
    private Vector3 destination;
    private int idWayPoint;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        gameManager = FindObjectOfType<GameManager>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        ChangeState(enemyState.IDLE);
    }

    void Update()
    {
        StateManager();
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

    void StateManager()
    {
        switch (state)
        {
            case enemyState.IDLE:
                //ChangeState(enemyState.IDLE);
                break;

            case enemyState.ALERT:
                //asda
                break;

            case enemyState.EXPLORE:
                //asda
                break;

            case enemyState.PATROL:
                //asda
                break;

            case enemyState.FURY:
                //asda
                break;

            case enemyState.FOLLOW:
                //asda
                break;
        }
    }

    void ChangeState(enemyState newState)
    {
        StopAllCoroutines();
        State = newState;
        switch (newState)
        {
            case enemyState.IDLE:
                destination = transform.position;
                navMeshAgent.destination = destination;

                StartCoroutine("IDLE");
                break;

            case enemyState.ALERT:
                StartCoroutine("ALERT");
                break;

            case enemyState.EXPLORE:
                //asda
                break;

            case enemyState.PATROL:
                idWayPoint = Random.Range(0, gameManager.slimeWayPoints.Length);
                destination = gameManager.slimeWayPoints[idWayPoint].position;
                navMeshAgent.destination = destination;

                StartCoroutine("PATROL");
                break;

            case enemyState.FURY:
                //asda
                break;

            case enemyState.FOLLOW:
                //asda
                break;
        }
    }

    IEnumerator IDLE()
    {
        yield return new WaitForSeconds(idleWaitTime);
        StayStill(50);
    }

    IEnumerator PATROL()
    {
        yield return new WaitForSeconds(patrolWaitTime);
        StayStill(30);
    }

    private void StayStill(int number)
    {
        if (Randomize() < number)
        {
            ChangeState(enemyState.IDLE);
        }
        else
        {
            ChangeState(enemyState.PATROL);
        };
    }

    int Randomize()
    {
        return Random.Range(0, 100);
    }
}
