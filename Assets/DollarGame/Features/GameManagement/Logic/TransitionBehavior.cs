using DollarGame.Utils.Event_Namespace;
using UnityEngine;
using UnityEngine.UI;

namespace DollarGame.Features.GameManagement.Logic
{
    public class TransitionBehavior : MonoBehaviour
    {
        private const float StartAlpha = 0;
        private const float TransitionAlpha = 1;
        
        [SerializeField] private Image image;

        [SerializeField] private float transitionFadeInTime;
        [SerializeField] private float transitionFadeOutTime;
        [SerializeField] private LeanTweenType easeType;

        [SerializeField] private GameEvent onTransitionStarted;
        [SerializeField] private GameEvent onTransitionMiddle;
        [SerializeField] private GameEvent onTransitionEnded;

        private void OnEnable()
        {
            onTransitionStarted?.Raise();
            LeanTween.alpha(image.rectTransform, TransitionAlpha, transitionFadeInTime)
                .setEase(easeType)
                .setOnComplete(FadeOut);
        }

        private void FadeOut()
        {
            onTransitionMiddle?.Raise();
            LeanTween.alpha(image.rectTransform, StartAlpha, transitionFadeOutTime)
                .setEase(easeType)
                .setOnComplete(Deactivate);
        }

        private void Deactivate()
        {
            onTransitionEnded?.Raise();
            gameObject.SetActive(false);
        }
    }
}
