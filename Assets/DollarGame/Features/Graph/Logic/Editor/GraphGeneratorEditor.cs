using UnityEditor;
using UnityEngine;

namespace DollarGame.Features.Graph.Logic.Editor
{
    [CustomEditor(typeof(GraphGenerator))]
    public class GraphGeneratorEditor : UnityEditor.Editor
    {
        private int previousGraphTypeIndex;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GraphGenerator e = target as GraphGenerator;
            
            if (GUILayout.Button("GenerateNewGraph"))
                e.GenerateNewGraph();

            // Assign new graphTypeIndex if changed in Editor
            int graphTypeIndex = serializedObject.FindProperty("graphType").intValue;
            
            if (graphTypeIndex != previousGraphTypeIndex)
            {
                e.ApplyGraphType();
            }
            
            previousGraphTypeIndex = graphTypeIndex;
        }
    }
}