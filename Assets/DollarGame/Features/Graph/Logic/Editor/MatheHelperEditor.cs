using DollarGame.Features.Graph.Logic;
using UnityEditor;
using UnityEngine;

namespace DollarGame.Utils.Editor
{
    [CustomEditor(typeof(IngameMathHelperBehavior))]
    public class MathHelperEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            IngameMathHelperBehavior e = target as IngameMathHelperBehavior;
            if (GUILayout.Button("CalculateMiddlePosAndAngle"))
                e.CalculateMiddlePosAndAngle();
            if (GUILayout.Button("CalculateLocalDistance"))
                e.CalculateLocalDistance();
            if (GUILayout.Button("CalculateDistance"))
                e.CalculateDistance();
            if (GUILayout.Button("CalculateLocalAngle"))
                e.CalculateLocalAngle();
            if (GUILayout.Button("CalculateAngle"))
                e.CalculateAngle();
            if (GUILayout.Button("CalculateLocalMiddlePosition"))
                e.CalculateLocalMiddlePosition();
            if (GUILayout.Button("CalculateMiddlePosition"))
                e.CalculateMiddlePosition();
        }
    }
}