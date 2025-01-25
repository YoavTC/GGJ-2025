using UnityEngine;
using DG;
using DG.Tweening;

public class peopleController : MonoBehaviour
{
    [SerializeField] Vector2 randomess;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOLocalMoveY(Random.Range(randomess.x, randomess.y), Random.Range(randomess.x,0.3f)).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InFlash);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
