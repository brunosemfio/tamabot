using UnityEditor;
using UnityEngine;

namespace Tamabot.Editor
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(StatPreset))]
    public class StatPresetEditor : UnityEditor.Editor
    {
        private StatPreset _stat;

        public override void OnInspectorGUI()
        {
            _stat = (StatPreset) target;

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(StatPreset.maxValue)));
            
            OverTimeGUI();

            TimeUntilMaxValue();
            
            ConditionalGUI();

            serializedObject.ApplyModifiedProperties();
        }

        private void OverTimeGUI()
        {
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Over time", EditorStyles.boldLabel);

            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(StatPreset.increaseInterval)));
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(StatPreset.increaseAmount)));
        }

        private void ConditionalGUI()
        {
            EditorGUILayout.Space();
            
            EditorGUILayout.LabelField("Conditional", EditorStyles.boldLabel);
            
            EditorGUILayout.PropertyField(serializedObject.FindProperty(nameof(StatPreset.increaseRate)));
        }

        private void TimeUntilMaxValue()
        {
            var time = Mathf.Abs(_stat.maxValue / (_stat.increaseAmount / _stat.increaseInterval));

            EditorGUILayout.Space();
            
            EditorGUILayout.HelpBox($"{time} second(s) / {time / 60} minute(s)", MessageType.Info, true);
        }
    }
}