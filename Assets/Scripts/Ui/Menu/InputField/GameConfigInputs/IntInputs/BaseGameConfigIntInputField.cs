using System.Globalization;
using UnityEngine;

namespace Ui.Menu.InputField
{
    public abstract class BaseGameConfigIntInputField : BaseGameConfigInputField
    {
        [SerializeField] public int defaultValue;
        [SerializeField] public int maxRange;
        [SerializeField] public int minRange;

        protected override string OnInputValidate(string inpString)
        {
            string validateString;
            var canConvert = int.TryParse(inpString, out var convertedNumber);
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
        
        protected override void OnValueSubmit(string inpString)
        {
            AssignValue(int.Parse(inpString));
        }
    }
}