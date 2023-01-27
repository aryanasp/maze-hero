
using UnityEngine.SceneManagement;

namespace Ui.Menu
{
    public class StartButtonUi : BaseButton
    {
        protected override void OnClick()
        {
            SceneManager.LoadScene("Core", LoadSceneMode.Single);
        }
    }
}