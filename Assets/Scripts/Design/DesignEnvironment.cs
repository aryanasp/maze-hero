using System.Collections.Generic;
using Design.Screens;
using UnityEngine;
using Zenject;

namespace Design
{
    public enum DesignModeCommands
    {
        Select = 0,
        Edit = 1,
        Delete = 2
    }
    public class DesignEnvironment : MonoBehaviour
    {
        [SerializeField] private List<DesignScreen> screens;
        [Inject] private DesignModel _designModel;
        private void OnEnable()
        {
            _designModel.Subscribers += ActiveDesignScreens;
        }

        private void OnDisable()
        {
            _designModel.Subscribers -= ActiveDesignScreens;
        }
        
        
        private void ActiveDesignScreens(bool state)
        {
            screens.ForEach(item => item.gameObject.SetActive(state));
        }
    }
}