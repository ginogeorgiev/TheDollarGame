using DollarGame.Utils.Variables_Namespace;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    [CreateAssetMenu(fileName = "new GraphGenerationData", menuName = "Graph/GraphGenerationData")]
    public class GraphGenerationData_SO : ScriptableObject
    {
        [Header("Graph Generation Data")]
        public FloatReference timeBetweenGraphElementGeneration;
        public int shuffleAmount;
        
        public IntVariable boarderYMin;
        public IntVariable boarderYMax;
        public IntVariable boarderXMin;
        public IntVariable boarderXMax;

        [Header("Node Related")]
        public GameObject nodePrefab;
        public FloatVariable nodeSpawnScale;
        public FloatVariable nodeSpawnGraphScale;
        [Range(50,500)] public float minNodeDistance;
        
        [Range(5,100)] public int exactNodesAmount;
        // public bool useExactNodeAmount;
        // [MinMaxSlider(5, 50)] public Vector2Int minMaxNodeAmount;

        [Header("Edge Related")]
        [Range(50,1000)] public float maxDistanceRangeForEdges;
        public GameObject edgePrefab;
        public FloatVariable edgeSpawnScale;
        public FloatVariable edgeSpawnGraphScale;
    }
}
