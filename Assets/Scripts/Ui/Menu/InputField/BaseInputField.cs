using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace Ui.Menu.InputField
{
    public abstract class BaseInputField : MonoBehaviour
    {
        [SerializeField] public TextMeshProUGUI description;
        [SerializeField] public TMP_InputField inputField;
        
        protected virtual void OnEnable()
        {
            Initialize();
            inputField.onSubmit.AddListener(OnInputChange);
        }

        protected abstract void Initialize();

        protected virtual void Start()
        {
            description.text = GetDescription();
        }

        protected abstract string GetDescription();

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
        
        protected virtual void OnDisable()
        {
            inputField.onSubmit.RemoveListener(OnInputChange);
        }
    }
}