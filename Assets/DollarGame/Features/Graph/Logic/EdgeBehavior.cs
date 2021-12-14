using DollarGame.Utils.Variables_Namespace;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    [RequireComponent(typeof(LineRenderer))]
    public class EdgeBehavior : MonoBehaviour
    {
        [SerializeField] private EdgeRuntimeSet_SO edgeRuntimeSet;
        
        private LineRenderer lineRenderer;
        
        [SerializeField] private FloatVariable edgeSpawnScale;

        private void OnEnable()
        {
            edgeRuntimeSet.Add(this);
            lineRenderer = GetComponent<LineRenderer>();

            lineRenderer.widthMultiplier = edgeSpawnScale.floatValue;
        }

        private void OnDestroy()
        {
            edgeRuntimeSet.Remove(this);
        }

        public void GivePositionInformation()
        {
            Debug.Log("EdgePosition Node A: " + lineRenderer.GetPosition(0));
            Debug.Log("EdgePosition Node B: " + lineRenderer.GetPosition(1));
        }
    }
}
