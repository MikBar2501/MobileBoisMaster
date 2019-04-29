using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destoryer : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        Destroy(col.gameObject);
    }
}
