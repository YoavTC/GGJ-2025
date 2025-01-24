using UnityEngine;

[CreateAssetMenu(fileName = "floatScriptable", menuName = "Scriptable Objects/floatScriptable")]
public class floatScriptable : ScriptableObject
{
    [SerializeField]private float m_Value;
    public float Value
    {
        get { return m_Value; }
        set 
        {
            if (value > 0) 
                m_Value = value; 
        }
    }
}
