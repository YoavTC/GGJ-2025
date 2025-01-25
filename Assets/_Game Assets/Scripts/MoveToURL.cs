using UnityEngine;

public class MoveToURL : MonoBehaviour
{
    [SerializeField] private string url;
    public void OpenURL()
    {
        Application.OpenURL(url);
    }
}
