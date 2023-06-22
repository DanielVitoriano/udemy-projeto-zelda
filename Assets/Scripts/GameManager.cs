using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum enemyState
{
    IDLE, ALERT, PATROL, FURY, FOLLOW, EXPLORE
};

public class GameManager : MonoBehaviour
{
    [Header("SlimeIA")]
    public Transform[] slimeWayPoints;
}
