using UnityEngine;

[CreateAssetMenu(fileName = "textScriptable", menuName = "Scriptable Objects/textScriptable")]
public class textScriptable : ScriptableObject
{
    [SerializeField]private string m_Value;
    public string Value
    {
        get { return m_Value; }
        set { m_Value = value; }
    }
}
