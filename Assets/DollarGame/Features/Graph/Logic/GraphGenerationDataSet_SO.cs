using System.Collections.Generic;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    [CreateAssetMenu(fileName = "new GraphGenerationDataSet", menuName = "Graph/GraphGenerationDataSet")]
    public class GraphGenerationDataSet_SO : ScriptableObject
    {
        public List<GraphGenerationData_SO> set;
    }
}
