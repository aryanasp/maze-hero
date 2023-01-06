using System;
using System.Collections;
using UnityEngine;

namespace BaseClass
{
    public abstract class JobBehaviour : MonoBehaviour
    {
        public void Start()
        {
            StartCoroutine(StartJob());
        }
        
        
        protected abstract IEnumerator StartJob();
    }
}