using System.Linq;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Twitch.Editor
{
    [CustomEditor(typeof(TwitchCommands))]
    public class TwitchCommandsEditor : UnityEditor.Editor
    {
        private SerializedProperty _commands;

        private ReorderableList _list;

        private void OnEnable()
        {
            _commands = serializedObject.FindProperty("commands");

            _list = new ReorderableList(serializedObject, _commands, true, true, true, true)
            {
                drawHeaderCallback = DrawHeaderCallback,
                drawElementCallback = DrawElementCallback
            };
        }

        private void DrawHeaderCallback(Rect rect)
        {
            EditorGUI.LabelField(rect, "Commands");
        }

        private void DrawElementCallback(Rect rect, int index, bool isActive, bool isFocused)
        {
            var element = _list.serializedProperty.GetArrayElementAtIndex(index);

            var text = element.FindPropertyRelative("text");

            var gameEvent = element.FindPropertyRelative("gameEvent");

            var rects = CalculateRect(new[] {1f, 2f}, 5f, rect);

            EditorGUI.PropertyField(rects[0], text, GUIContent.none);

            EditorGUI.PropertyField(rects[1], gameEvent, GUIContent.none);
        }

        private Rect[] CalculateRect(float[] weights, float spacing, Rect rect)
        {
            var totalWeight = weights.Sum();

            var totalSpacing = (weights.Length - 1) * spacing;
            
            var rects = new Rect[weights.Length];

            var previousWidth = 0f;

            for (var i = 0; i < rects.Length; i++)
            {
                var width = (rect.width - totalSpacing) * weights[i] / totalWeight;

                rects[i] = new Rect(rect.x + previousWidth + spacing * i, rect.y, width, EditorGUIUtility.singleLineHeight);

                previousWidth += width;
            }

            return rects;
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            _list.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}