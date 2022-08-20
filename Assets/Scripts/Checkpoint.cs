using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public bool Passed { get; private set; }
    void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("a checkpoint was triggered");
        var player = collision.GetComponent<Player>();
        if (player != null)
        {
            Passed = true;
            Debug.Log("a checkpoint is true");
        }
    }
}
