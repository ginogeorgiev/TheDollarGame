using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    [CreateAssetMenu(fileName = "new GraphData", menuName = "Graph/GraphData")]
    public class GraphData_SO : ScriptableObject
    {
        [Header("Graph Data")]
        public int genus;
        public int totalNodeValue;
        
        [Header("RuntimeSets")]
        public NodeRuntimeSet_SO nodeRuntimeSet;
        public EdgeRuntimeSet_SO edgeRuntimeSet;
    }
}
