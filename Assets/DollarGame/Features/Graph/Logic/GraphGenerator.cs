using System.Collections;
using System.Linq;
using DollarGame.Utils.Event_Namespace;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DollarGame.Features.Graph.Logic
{
    public enum GraphGenerationDataSetIndex
    {
        EasyGraph,
        NormalGraph,
        BigGraph,
        BigComplexGraph
    }
    public class GraphGenerator : MonoBehaviour
    {
        [Header("Graph Generation Data")]
        [SerializeField] private GraphGenerationDataSet_SO graphGenerationDataSet;
        [SerializeField] private GraphGenerationData_SO graphGenerationData;
        [SerializeField] private GraphData_SO graphData;

        [Header("Events for Generation")]
        [SerializeField] private GameEvent onGraphGenerationStarted;
        [SerializeField] private GameEvent onGraphGenerationCompleted;
        
        [Header("Container for generated Graph-Elements")]
        public Transform nodeGenerationContainer;
        public Transform edgeGenerationContainer;
        
        [Space]
        public GraphGenerationDataSetIndex graphType;

        private void Start()
        {
            graphGenerationData = graphGenerationDataSet.set[(int)graphType];
            // StartCoroutine(GenerateGraph());
        }

        public void GenerateNewGraph()
        {
            ClearGraph();
            StartCoroutine(GenerateGraph());
        }

        public void ApplyGraphType()
        {
            graphGenerationData = graphGenerationDataSet.set[(int)graphType];
        }

        private void CalculateGenus()
        {
            graphData.genus = graphData.edgeRuntimeSet.items.Count - graphData.nodeRuntimeSet.items.Count + 1;
        }
        
        private void CalculateTotalNodeValue()
        {
            graphData.totalNodeValue = graphData.nodeRuntimeSet.items.Sum(node => node.gameObject.GetComponent<NodeBehavior>().nodeValue);
        }

        private void ClearGraph()
        {
            // clear nodeGenerationContainer
            foreach (Transform node in nodeGenerationContainer)
            {
                Destroy(node.gameObject);
            }

            // clear edgeGenerationContainer
            foreach (Transform edge in edgeGenerationContainer)
            {
                Destroy(edge.gameObject);
            }
        }

        private IEnumerator GenerateGraph()
        {
            graphGenerationData = graphGenerationDataSet.set[(int)graphType];
            
            onGraphGenerationStarted?.Raise();
            
            yield return new WaitForSeconds(0.1f);

            graphGenerationData.nodeSpawnScale.SetValue(graphGenerationData.nodeSpawnGraphScale);
            graphGenerationData.edgeSpawnScale.SetValue(graphGenerationData.edgeSpawnGraphScale);
            
            yield return new WaitForSeconds(0.1f);
            
            // place Nodes at random locations inside the Canvas
            for (int i = 0; i < graphGenerationData.exactNodesAmount; i++)
            {
                yield return new WaitForSeconds(graphGenerationData.timeBetweenGraphElementGeneration);
                GenerateNode();
            }
            
            yield return new WaitForSeconds(0.1f);
            
            // connect Nodes with Edges
            foreach (NodeBehavior node in graphData.nodeRuntimeSet.items)
            {
                yield return new WaitForSeconds(graphGenerationData.timeBetweenGraphElementGeneration);
                GenerateEdges(node);
            }
            
            yield return new WaitForSeconds(0.1f);
            
            CalculateGenus();
            
            // Add genus to first node in RuntimeSet to make game winnable
            graphData.nodeRuntimeSet.items[0].nodeValue += graphData.genus;

            // Shuffle nodeValues
            for (int i = 0; i < graphGenerationData.shuffleAmount; i++)
            {
                int randomIndex = Random.Range(0, graphData.nodeRuntimeSet.items.Count);
                graphData.nodeRuntimeSet.items[randomIndex].modifyNodeWithoutTextUpdate();
            }
            
            CalculateTotalNodeValue();
            
            foreach (NodeBehavior node in graphData.nodeRuntimeSet.items)
            {
                node.GetComponent<NodeBehavior>().UpdateValueText();
            }
            
            yield return new WaitForSeconds(0.1f);
            
            onGraphGenerationCompleted?.Raise();
            
            yield return null;
        }

        private void GenerateNode()
        {
            if (graphData.nodeRuntimeSet.items.Count == 0)
            {
                // for first node
                Vector2 randomPos = new Vector2(
                    Random.Range(graphGenerationData.boarderXMin.intValue, graphGenerationData.boarderXMax.intValue),
                    Random.Range(graphGenerationData.boarderYMin.intValue, graphGenerationData.boarderYMax.intValue));
                GameObject node = Instantiate(graphGenerationData.nodePrefab, nodeGenerationContainer);
                node.transform.localPosition = randomPos;
            }
            else
            {
                int whileSaveCount = 0;
                bool goodDistanceFound = false;
                Vector2 randomPos = Vector2.zero;
                while (!goodDistanceFound && whileSaveCount < 10000)
                {
                    whileSaveCount++;
                    randomPos = new Vector2(
                        Random.Range(graphGenerationData.boarderXMin.intValue, graphGenerationData.boarderXMax.intValue),
                        Random.Range(graphGenerationData.boarderYMin.intValue, graphGenerationData.boarderYMax.intValue));

                    foreach (NodeBehavior child in graphData.nodeRuntimeSet.items)
                    {
                        float distance = Vector2.Distance(randomPos,
                        child.transform.localPosition);
                        if (distance < graphGenerationData.minNodeDistance)
                        {
                            goodDistanceFound = false;
                            break;
                        }
                        goodDistanceFound = true;
                    }
                }

                if (whileSaveCount == 10000)
                {
                    Debug.LogWarning("To many iterations no good position found");
                }
                GameObject node = Instantiate(graphGenerationData.nodePrefab, nodeGenerationContainer);
                node.transform.localPosition = randomPos;
            }
        }

        private void GenerateEdges(NodeBehavior node)
        {
            foreach (NodeBehavior child in graphData.nodeRuntimeSet.items)
            {
                if (child.Equals(node))
                {
                    continue;
                }
                
                float distance = Vector2.Distance(node.transform.localPosition, child.transform.localPosition);
                if (!(distance < graphGenerationData.maxDistanceRangeForEdges)) continue;
                
                //TODO can be optimized, so that the edge do not need to be destroyed (just compare the positions and instantiate the edge later)
                GameObject edge = Instantiate(graphGenerationData.edgePrefab, edgeGenerationContainer);
                LineRenderer edgeLineRenderer = edge.GetComponent<LineRenderer>();
                edgeLineRenderer.SetPosition(0, node.transform.position);
                edgeLineRenderer.SetPosition(1, child.transform.position);

                bool edgeWasDestroyed = false;

                foreach (EdgeBehavior item in graphData.edgeRuntimeSet.items)
                {
                    LineRenderer itemLineRenderer = item.GetComponent<LineRenderer>();

                    if (edge.Equals(item.gameObject)) continue;

                    if ((itemLineRenderer.GetPosition(0) != edgeLineRenderer.GetPosition(1) ||
                         itemLineRenderer.GetPosition(1) != edgeLineRenderer.GetPosition(0)) &&
                        (itemLineRenderer.GetPosition(0) != edgeLineRenderer.GetPosition(0) ||
                         itemLineRenderer.GetPosition(1) != edgeLineRenderer.GetPosition(1))) continue;
                    
                    Destroy(edge);
                    edgeWasDestroyed = true;
                }

                if (edgeWasDestroyed) continue;
                // Add Node to neighbourNodes list of both nodes
                node.gameObject.GetComponent<NodeBehavior>().neighbourNodes.Add(child.gameObject.GetComponent<NodeBehavior>());
                child.gameObject.GetComponent<NodeBehavior>().neighbourNodes.Add(node.gameObject.GetComponent<NodeBehavior>());
            }
        }
    }
}
