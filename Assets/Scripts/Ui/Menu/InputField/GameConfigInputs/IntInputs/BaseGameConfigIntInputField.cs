using System.Globalization;
using TMPro;
using UnityEngine;

namespace Ui.Menu.InputField
{
    public abstract class BaseGameConfigIntInputField : BaseGameConfigInputField
    {
        [SerializeField] public int defaultValue;
        [SerializeField] public int minRange;
        [SerializeField] public int maxRange;
        
        protected override void Start()
        {
            var textComp = (TextMeshProUGUI) inputField.placeholder;
            textComp.text = $"Enter a number between ({minRange}, {maxRange})";
            base.Start();
        }
        
        protected override string OnInputValidate(string inpString)
        {
            var validateString = inpString;
            if (inpString.Contains("."))
            {
                validateString = inpString.Replace(".", "");
            }
            var canConvert = int.TryParse(validateString, out var convertedNumber);
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
            AssignValue(int.Parse(inpString));
        }

        protected abstract void AssignValue(int finalValue);
        
    }
}