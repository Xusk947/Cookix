using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
/*
 * 
 * Using to show fields in Inspector when conditional is right
 * 
 * Ex:
 * private bool canUse;
 * [ConditionalField("canUse", true)]
 * private List<Class> useIn = new List<Class>();
 * 
 */
public class ConditionalFieldAttribute : PropertyAttribute
{
    public string fieldName;
    public bool showIfEqual;

    public ConditionalFieldAttribute(string fieldName, bool showIfEqual = true)
    {
        this.fieldName = fieldName;
        this.showIfEqual = showIfEqual;
    }
}

[CustomPropertyDrawer(typeof(ConditionalFieldAttribute))]
public class ConditionalFieldDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Get attribute ConditionalField
        ConditionalFieldAttribute cond = attribute as ConditionalFieldAttribute;
        // Get ConditionalField
        SerializedProperty sourceField = property.serializedObject.FindProperty(cond.fieldName);

        // If field which provided in ConditionalField, equalt to waited variable we show it
        if (sourceField.boolValue == cond.showIfEqual)
        {
            EditorGUI.PropertyField(position, property, label, true);
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        ConditionalFieldAttribute cond = attribute as ConditionalFieldAttribute;
        SerializedProperty sourceField = property.serializedObject.FindProperty(cond.fieldName);

        if (sourceField.boolValue == cond.showIfEqual)
        {
            return EditorGUI.GetPropertyHeight(property, label);
        }
        else
        {
            return 0f;
        }
    }
}