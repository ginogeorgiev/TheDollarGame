using UnityEditor;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic.Editor
{
    [CustomEditor(typeof(EdgeBehavior))]
    public class EdgeBehaviorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            EdgeBehavior e = target as EdgeBehavior;
            
            if (GUILayout.Button("GivePositionInformation"))
                e.GivePositionInformation();
        }
    }
}