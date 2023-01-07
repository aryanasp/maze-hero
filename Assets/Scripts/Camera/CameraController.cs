using System;
using Design;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Inject] public DesignModel _designModel; 
        private void Update()
        {
            if (_designModel.IsDesignMode)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    transform.position += new Vector3(0, 5, 0);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position -= new Vector3(0, 5, 0);
                }
            
                if (Input.GetKeyDown(KeyCode.A))
                {
                    transform.position += new Vector3(5, 0, 0);
                }
                else if (Input.GetKeyDown(KeyCode.S))
                {
                    transform.position -= new Vector3(5, 0, 0);
                }
            }
        }
    }
}