using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableByCharacter : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            Destroy(obstacle);
        }
    }
}
