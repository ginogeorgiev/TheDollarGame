using System.Linq;
using DollarGame.Utils.Event_Namespace;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic
{
    public class GraphController : MonoBehaviour
    {
        [Header("RuntimeSets")]
        [SerializeField] private NodeRuntimeSet_SO nodeRuntimeSet;
        [SerializeField] private EdgeRuntimeSet_SO edgeRuntimeSet;

        [SerializeField] private GameEvent onLevelCompleted;


        public void OnNodeClicked()
        {
            CheckForWinState();
        }

        private void CheckForWinState()
        {
            if (nodeRuntimeSet.items.Any(node => node.nodeValue < 0))
            {
                return;
            }
            onLevelCompleted?.Raise();
        }
        
    }
}
