using System.Globalization;
using TMPro;
using UnityEngine;

namespace Ui.Menu.InputField
{
    public abstract class BaseGameConfigFloatInputField : BaseGameConfigInputField
    {
        [SerializeField] public float defaultValue;
        [SerializeField] public float maxRange;
        [SerializeField] public float minRange;
        
        protected void Start()
        {
            var textComp = (TextMeshProUGUI) inputField.placeholder;
            textComp.text = $"Enter a number between ({minRange}, {maxRange})";
        }
        
        protected override string OnInputValidate(string inpString)
        {
            string validateString;
            var canConvert = float.TryParse(inpString, out var convertedNumber);
            if (!canConvert)
            {
                validateString = defaultValue.ToString(CultureInfo.InvariantCulture);
                return validateString;
            }
            if (convertedNumber > maxRange)
            {
                convertedNumber = maxRange;
            }
            if (convertedNumber < minRange)
            {
                convertedNumber = minRange;
            }
            validateString = convertedNumber.ToString(CultureInfo.InvariantCulture);
            return validateString;
        }

        protected override void OnValueChange(string inpString)
        {
            AssignValue(float.Parse(inpString));
        }

        protected abstract void AssignValue(float finalValue);
        
        protected override void OnValueSubmit(string inpString)
        {
            AssignValue(float.Parse(inpString));
        }
    }
}