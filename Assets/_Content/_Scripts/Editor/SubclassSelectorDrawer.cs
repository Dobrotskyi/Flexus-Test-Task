using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using _Scripts.Attributes;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor {
    [CustomPropertyDrawer(typeof(SubclassSelectorAttribute))]
    public class SubclassSelectorDrawer : PropertyDrawer {
        private List<Type> _types;
        private GUIContent[] _typeNames;

        private void EnsureTypes() {
            if (_types != null) return;

            var baseType = fieldInfo.FieldType;

            _types = TypeCache.GetTypesDerivedFrom(baseType)
                .Where(t => !t.IsAbstract && !t.IsInterface)
                .OrderBy(t => t.Name)
                .ToList();

            _typeNames = _types.Select(t => new GUIContent(t.Name)).ToArray();
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EnsureTypes();

            EditorGUI.BeginProperty(position, label, property);

            Rect labelRect = new Rect(
                position.x,
                position.y,
                position.width,
                EditorGUIUtility.singleLineHeight
            );
            EditorGUI.LabelField(labelRect, label);

            if (_types.Count == 1 && property.managedReferenceValue == null) {
                property.managedReferenceValue = Activator.CreateInstance(_types[0]);
                property.serializedObject.ApplyModifiedProperties();
            }

            Rect dropdownRect = new Rect(
                position.x,
                position.y + EditorGUIUtility.singleLineHeight + 2,
                position.width,
                EditorGUIUtility.singleLineHeight
            );

            int currentIndex = -1;
            if (property.managedReferenceValue != null)
                currentIndex = _types.IndexOf(property.managedReferenceValue.GetType());

            if (_types.Count > 1) {
                int newIndex = EditorGUI.Popup(dropdownRect, currentIndex, _typeNames);
                if (newIndex != currentIndex && newIndex >= 0) {
                    if (_types[newIndex].GetConstructor(Type.EmptyTypes) == null)
                        property.managedReferenceValue = FormatterServices.GetUninitializedObject(_types[newIndex]);
                    else
                        property.managedReferenceValue = Activator.CreateInstance(_types[newIndex]);
                    property.serializedObject.ApplyModifiedProperties();
                }
            }
            else {
                EditorGUI.LabelField(dropdownRect, $"Type: {_types[0].Name}");
            }

            if (property.managedReferenceValue != null) {
                Rect fieldRect = new Rect(
                    position.x,
                    position.y + EditorGUIUtility.singleLineHeight * 2 + 4,
                    position.width,
                    EditorGUI.GetPropertyHeight(property, true)
                );

                EditorGUI.indentLevel++;
                EditorGUI.PropertyField(fieldRect, property, true);
                EditorGUI.indentLevel--;
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            EnsureTypes();

            float height = EditorGUIUtility.singleLineHeight * 2 + 4;

            if (_types.Count == 1 && property.managedReferenceValue == null)
                height += EditorGUIUtility.singleLineHeight * 2;

            if (property.managedReferenceValue != null)
                height += EditorGUI.GetPropertyHeight(property, true);

            return height;
        }
    }
}