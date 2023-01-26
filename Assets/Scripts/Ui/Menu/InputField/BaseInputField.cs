using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Menu.InputField
{
    public abstract class BaseInputField : MonoBehaviour
    {
        [SerializeField] public TMP_InputField inputField;

        protected virtual void OnEnable()
        {
            inputField.onSubmit.AddListener(OnValueSubmit);
            inputField.onValueChanged.AddListener(OnInputChange);
        }

        private void OnInputChange(string inpString)
        {
            var validatedString = OnInputValidate(inpString);
            if (validatedString != inpString)
            {
                inputField.text = validatedString;
            }
            else
            {
                OnValueChange(validatedString);
            }
        }

        protected abstract string OnInputValidate(string inpString);
        protected abstract void OnValueChange(string inpString);
        protected abstract void OnValueSubmit(string inpString);
        
        protected virtual void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(OnInputChange);
            inputField.onSubmit.RemoveListener(OnValueSubmit);
        }
    }
}