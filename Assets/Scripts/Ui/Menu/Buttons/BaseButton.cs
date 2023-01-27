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
            yield return new WaitForSeconds(rememberTime);
            _currentCountOfClick = 0;
        }

        public void OnButtonClick()
        {
            if (_currentCountOfClick >= timesToClick)
            {
                OnClick();
            }
        }

        protected abstract void OnClick();
    }
}