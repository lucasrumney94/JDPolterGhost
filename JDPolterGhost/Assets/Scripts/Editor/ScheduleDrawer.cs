using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomPropertyDrawer(typeof(ScheduleItem))]
public class ScheduleDrawer : PropertyDrawer
{
    float timeWidth = 40;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        float leftOverWidth = position.width - (2 * timeWidth) - 10;

        Rect objectRect = new Rect(position.x, position.y, leftOverWidth, position.height);
        Rect startTimeRect = new Rect(position.x + leftOverWidth + 5, position.y, 40, position.height);
        Rect endTimeRect = new Rect(position.x + leftOverWidth + timeWidth + 10, position.y, 40, position.height);

        EditorGUI.PropertyField(objectRect, property.FindPropertyRelative("location"), GUIContent.none);
        EditorGUI.PropertyField(startTimeRect, property.FindPropertyRelative("startTime"), GUIContent.none);
        EditorGUI.PropertyField(endTimeRect, property.FindPropertyRelative("endTime"), GUIContent.none);

        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }
}
