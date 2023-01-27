using UnityEditor;
using UnityEngine;

namespace Ui.Menu
{
    public class QuitButtonUi : BaseButton
    {
        protected override void OnClick()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
            return;
#endif
            Application.Quit();
        }
    }
}