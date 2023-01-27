using System.Collections;
using UnityEngine;
using UnityEngine.Events;


namespace Ui.Menu
{
    public abstract class BaseButton : MonoBehaviour
    {
        [SerializeField] private int timesToClick = 1;
        [SerializeField] private float rememberTime = 2f;
        
        private int _currentCountOfClick;

        private void Start()
        {
            StartCoroutine(ProcessClick());
        }

        private IEnumerator ProcessClick()
        {
            var timeToUpdate =  new WaitForSeconds(rememberTime);
            while (true)
            {
                yield return timeToUpdate;
                _currentCountOfClick = 0;
            }
        }

        public void OnButtonClick()
        {
            _currentCountOfClick++;
            if (_currentCountOfClick >= timesToClick)
            {
                OnClick();
            }
        }

        protected abstract void OnClick();
    }
}