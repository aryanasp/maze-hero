using DG.Tweening;
using UnityEngine;

namespace Ui.Menu
{
    public class ConfigButton : BaseButton
    {
        [SerializeField] private GameObject menuPage;
        [SerializeField] private GameObject configPage;
        protected override void OnClick()
        {
            var menuCanvasGroup = menuPage.GetComponent<CanvasGroup>();
            var configCanvasGroup = configPage.GetComponent<CanvasGroup>();
            configCanvasGroup.alpha = 0f;
            configPage.SetActive(true);
            var menuTween = menuCanvasGroup.DOFade(0f, 0.3f);
            var configTween = configCanvasGroup.DOFade(1, 0.5f);
            menuTween.onComplete = () =>
            {
                menuPage.SetActive(false);
            };
            menuPage.transform.DOLocalMove(new Vector3(1920, 0f, 0f), 0.5f);
        }
    }
}