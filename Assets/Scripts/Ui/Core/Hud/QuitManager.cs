using System;
using UnityEditor;
using UnityEngine;

namespace Ui.Core.Hud
{
    public class QuitManager : MonoBehaviour
    {
        [SerializeField] private GameObject quitPopup;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                quitPopup.SetActive(!quitPopup.activeInHierarchy);
            }
        }

        public void OnYes()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            Application.Quit();
        }

        public void OnNo()
        {
            quitPopup.SetActive(false);
        }
    }
}