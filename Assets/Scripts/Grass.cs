using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grass : MonoBehaviour
{
    void Hit(int damage)
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }
}
