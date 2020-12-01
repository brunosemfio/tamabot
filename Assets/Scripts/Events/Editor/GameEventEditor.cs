using UnityEditor;
using UnityEngine;

namespace Events.Editor
{
    [CustomEditor(typeof(GameEvent))]
    public class GameEventEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (!Application.isPlaying) return;

            EditorGUILayout.Space();

            if (GUILayout.Button("Raise")) (target as GameEvent)?.Raise();
        }
    }
}