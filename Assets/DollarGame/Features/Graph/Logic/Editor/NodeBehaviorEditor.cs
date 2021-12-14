using UnityEditor;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic.Editor
{
    [CustomEditor(typeof(NodeBehavior))]
    public class NodeBehaviorEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            NodeBehavior e = target as NodeBehavior;
            
            if (GUILayout.Button("GivePositionInformation"))
                e.GivePositionInformation();
            
            if (GUILayout.Button("UpdateValueText"))
                e.UpdateValueText();
        }
    }
}