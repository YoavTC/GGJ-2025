using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScriptable", menuName = "Scriptable Objects/PlayerScriptable")]
public class playerScriptable : ScriptableObject
{
    [SerializeField] private float m_Value;
    public float Value
    {
        get { return m_Value; }
        set 
        {
            if (value >= 0) 
                m_Value = value; 
        }
    }

    [SerializeField] private Vector3 startPosition;
    public Vector3 StartPosition
    {
        get { return startPosition; }
        set { startPosition = value; }
    }
}
