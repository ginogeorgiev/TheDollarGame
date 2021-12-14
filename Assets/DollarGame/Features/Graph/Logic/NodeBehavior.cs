using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using DollarGame.Utils.Event_Namespace;
using DollarGame.Utils.Variables_Namespace;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DollarGame.Features.Graph.Logic
{
    [SuppressMessage("ReSharper", "Unity.NoNullPropagation")]
    public class NodeBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [Header("Enable Related")] [SerializeField]
        private NodeRuntimeSet_SO nodeRuntimeSet;

        [SerializeField] private float scaleTime = 0.3f;
        [SerializeField] private Vector3 startScale = new Vector3(0f, 0f, 0f);

        [Header("Hover Related")]
        [SerializeField] private GameObject hoverEffect;

        [Header("Value Related")]
        public int nodeValue;
        public TMP_Text valueText;

        [SerializeField] private FloatVariable nodeSpawnScale;

        [SerializeField] private float bounceTime = 0.1f;
        [SerializeField] private Vector3 valueTextBounceSize = new Vector3(1.5f, 1.5f, 1.5f);
        [SerializeField] private LeanTweenType valueTextInEaseType;
        [SerializeField] private LeanTweenType valueTextOutEaseType;

        [Header("Click Related")]
        [SerializeField] private GameEvent onNodeClicked;
        
        [Header("Refs to Graph-Elements")]
        public List<NodeBehavior> neighbourNodes;

        [Header("States")]
        [SerializeField] private BoolVariable collectState;
        [SerializeField] private BoolVariable nodesAreClickable;

        private void OnEnable()
        {
            nodeRuntimeSet.Add(this);
            UpdateValueText();
            transform.localScale = startScale;
            LeanTween.scale(gameObject,new Vector3(nodeSpawnScale.floatValue, nodeSpawnScale.floatValue, nodeSpawnScale.floatValue), scaleTime)
                .setEaseOutBack();
        }

        private void OnDestroy()
        {
            nodeRuntimeSet.Remove(this);
        }

        public void UpdateValueText()
        {
            LeanTween.scale(valueText.gameObject, valueTextBounceSize, bounceTime)
                .setEase(valueTextInEaseType);
            LeanTween.scale(valueText.gameObject, new Vector3(1f, 1f, 1f), bounceTime)
                .setEase(valueTextOutEaseType)
                .setDelay(bounceTime);

            valueText.text = nodeValue.ToString();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (!nodesAreClickable.boolValue) return;
            hoverEffect.SetActive(true);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            hoverEffect.SetActive(false);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!nodesAreClickable.boolValue) return;
            
            modifyNode();
        }

        private void modifyNode() {
            nodeValue += (collectState.boolValue ? neighbourNodes.Count : -neighbourNodes.Count);
            UpdateValueText();
            
            foreach (NodeBehavior node in neighbourNodes)
            {
                node.nodeValue += (collectState.boolValue ? -1 : 1);
                node.UpdateValueText();
            }
            
            onNodeClicked?.Raise();
        }

        public void modifyNodeWithoutTextUpdate() {
            nodeValue += (collectState.boolValue ? neighbourNodes.Count : -neighbourNodes.Count);
            
            foreach (NodeBehavior node in neighbourNodes)
            {
                node.nodeValue += (collectState.boolValue ? -1 : 1);
            }
        }

        public void GivePositionInformation()
        {
            Debug.Log("LocalPosition is: " + transform.localPosition);
            Debug.Log("Position is: " + transform.position);
        }
    }
}
