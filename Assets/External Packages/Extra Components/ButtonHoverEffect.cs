#if DOTWEEN
using DG.Tweening;
#endif
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Scale Effect")]
    [SerializeField] private bool doScale;
    [SerializeField] private float scaleMultiplier;
    private Vector2 originalScale;
    
    #if DOTWEEN
    [Header("DOTween Settings")]
    [SerializeField] private float tweenDuration;
    [SerializeField] private Ease tweenEasing;
    #endif
    
    [Space]
    
    public UnityEvent HoverEnterUnityEvent;
    public UnityEvent HoverExitUnityEvent;

    private void Start()
    {
        originalScale = transform.localScale;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (doScale)
        {
            #if DOTWEEN
            transform
                .DOScale(transform.localScale * scaleMultiplier, tweenDuration)
                .SetEase(tweenEasing);
            #else
            transform.localScale *= scaleMultiplier;
            #endif
        }
        
        HoverEnterUnityEvent?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (doScale)
        {
            #if DOTWEEN
            transform
                .DOScale(originalScale, tweenDuration)
                .SetEase(tweenEasing);
            #else
            transform.localScale originalScale;
            #endif
        }
        
        HoverExitUnityEvent?.Invoke();
    }
}
