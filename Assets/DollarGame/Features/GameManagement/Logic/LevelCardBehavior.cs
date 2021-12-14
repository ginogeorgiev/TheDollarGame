using DollarGame.Features.Graph.Logic;
using DollarGame.Utils.Event_Namespace;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DollarGame.Features.GameManagement.Logic
{
    public class LevelCardBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Hover Related")]
        [SerializeField] private GameObject hoverEffect;

        public GraphGenerationDataSetIndex graphType;
        [SerializeField] private GraphGenerator graphGenerator;

        [SerializeField] private GameEvent onLevelCardClicked;
        
        [SerializeField] private float scaleTime = 0.3f;
        [SerializeField] private Vector3 startScale = new Vector3(0f, 0f, 0f);
        
        [SerializeField] private float hoverLoopTime = 1.2f;
        [SerializeField] private float hoverLoopTimeOffset = 0.1f;
        [SerializeField] private float hoverLoopHeight = -60f;
        [SerializeField] private LeanTweenType easeType;
        
        private void OnEnable()
        {
            transform.localScale = startScale;
            LeanTween.scale(gameObject,new Vector3(1f, 1f, 1f), scaleTime)
                .setEaseOutBack();

            LeanTween.moveLocalY(gameObject, transform.position.y + hoverLoopHeight, hoverLoopTime)
                .setDelay(scaleTime + hoverLoopTimeOffset)
                .setEase(easeType)
                .setLoopPingPong();
        }
    
        public void OnPointerEnter(PointerEventData eventData)
        {
            hoverEffect.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoverEffect.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            graphGenerator.graphType = graphType;
            onLevelCardClicked?.Raise();
            hoverEffect.SetActive(false);
        }
    }
}
