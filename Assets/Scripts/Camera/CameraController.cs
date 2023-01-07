using System;
using Design;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Camera
{
    public class CameraController : MonoBehaviour
    {
        [Inject] public DesignModel _designModel;
        [SerializeField] private float cameraSpeed = 0.5f;
        private void Update()
        {
            if (_designModel.IsDesignMode)
            {
                if (Input.GetKey(KeyCode.W))
                {
                    transform.position += new Vector3(0, cameraSpeed, 0);
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    transform.position -= new Vector3(0, cameraSpeed, 0);
                }
            
                if (Input.GetKey(KeyCode.D))
                {
                    transform.position += new Vector3(cameraSpeed, 0, 0);
                }
                else if (Input.GetKey(KeyCode.A))
                {
                    transform.position -= new Vector3(cameraSpeed, 0, 0);
                }
            }
        }

        [Button]
        public void ResetCameraPosition()
        {
            transform.position = new Vector3(0, 0, -10);
        }
    }
}