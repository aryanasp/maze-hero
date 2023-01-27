using UnityEngine;

namespace Ui.Menu
{
    public class QuitButtonUi : BaseButton
    {
        protected override void OnClick()
        {
            Application.Quit();
        }
    }
}