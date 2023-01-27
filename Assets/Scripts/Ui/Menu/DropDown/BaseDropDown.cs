using System;
using TMPro;
using UnityEngine;

namespace Ui.Menu.DropDown
{
    public abstract class BaseDropDown : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI description;
        [SerializeField] public TMP_Dropdown dropdown;
        protected virtual void OnEnable()
        {
            description.text = GetDescription();
            FillOptions();
            Initialize();
            dropdown.onValueChanged.AddListener(OnValueChange);
        }
        protected abstract string GetDescription();
        protected abstract void FillOptions();
        protected abstract void Initialize();
        protected abstract void OnValueChange(int newValue);

        private void OnDestroy()
        {
            dropdown.onValueChanged.RemoveListener(OnValueChange);
        }


    }
}