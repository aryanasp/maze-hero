using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatableByCharacter : MonoBehaviour
{
    [SerializeField] private GameObject obstacle;
    [SerializeField] private Collider2D colid;
    private bool _isConsumed = false;

    public void Consume()
    {
        colid.enabled = false;
        var temp = gameObject;
        temp.SetActive(false);
        Destroy(temp);
        _isConsumed = true;
    }

    public bool IsConsumed()
    {
        return _isConsumed;
    }
}
