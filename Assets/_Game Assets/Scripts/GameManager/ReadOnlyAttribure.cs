using UnityEngine;
using UnityEditor;

// Place this in a non-Editor folder
public class ReadOnlyAttribute : PropertyAttribute { }

// Place this in an Editor folder
/* `[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]` is a custom attribute in Unity that allows you
to create a custom property drawer for a specific attribute. In this case, it is used to create a
custom property drawer for the `ReadOnlyAttribute` class. This custom property drawer defines how
properties marked with the `ReadOnly` attribute should be displayed in the Unity Editor. The
`ReadOnlyDrawer` class specifies that the property should be displayed as read-only, meaning it
cannot be edited directly in the Inspector. */
/* `[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]` is a custom attribute in Unity that allows you
to define a custom property drawer for a specific attribute. In this case, it is used to create a
custom property drawer for the `ReadOnlyAttribute` class. This custom property drawer is responsible
for drawing the property in the Unity Inspector as read-only, meaning the property cannot be edited
directly in the Inspector. */
[CustomPropertyDrawer(typeof(ReadOnlyAttribute))]
public class ReadOnlyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}