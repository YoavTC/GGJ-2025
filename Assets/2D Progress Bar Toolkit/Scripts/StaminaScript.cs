using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.UIElements;
public class StaminaScript : MonoBehaviour
{

    public float BarIndex;

    [SerializeField] UnityEngine.UI.Image ProgressBar;
    [SerializeField] float DefaultSpeed;
    [SerializeField] UnityEvent onProgress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private Coroutine Animation;

    private void Start()
    {
        //if (ProgressBar.type!=Image.Type.Filled)
    }

    public void SetProgress(float progress)
    {
        SetProgress(progress, DefaultSpeed);
    }

   // public void SetProgress(float progress, float speed)
    public void SetProgress(float progress, float fillTime)
    {
        if (progress < 0) progress = 0;
        if (progress > 1) progress = 1;
        
        if (progress!=ProgressBar.fillAmount)
        {
            if (Animation != null) StopCoroutine(Animation);
           
        }
        Animation = StartCoroutine(Animate(progress, fillTime));
    }

    public void SetProgressTime(float progress, float fillTime)
    {
        // Calculate the time it takes to fill from 0 to 1
        float fillDuration = fillTime;

        // Calculate the current time
        float currentTime = Time.time;

        // Calculate the progress over time
        float progressOverTime = Mathf.Lerp(0f, 1f, (currentTime % fillDuration) / fillDuration);

        // Set the progress
        progress = Mathf.Clamp01(progressOverTime);

        // Update the progress bar
        ProgressBar.fillAmount = progress;
    }

    private IEnumerator Animate(float targetProgress, float fillTime)
    {
        float initialProgress = ProgressBar.fillAmount;
        float elapsedTime = 0f;
        Debug.Log("Animate Bar "+targetProgress);
        while (elapsedTime < fillTime)
        {
            elapsedTime += Time.deltaTime;
            ProgressBar.fillAmount = Mathf.Lerp(initialProgress, targetProgress, elapsedTime / fillTime);
            Debug.Log("Animate Bar2 " + ProgressBar.fillAmount);
            onProgress.Invoke();
            yield return null;
        }

        // Ensure the final value is set
        ProgressBar.fillAmount = targetProgress;
        onProgress.Invoke();
    }

    private IEnumerator AnimateOld(float progress, float speed)
    {
        /*
        float time=0;
        float initialProgress = ProgressBar.fillAmount;*/
        while (ProgressBar.fillAmount != progress)
        {
            ProgressBar.fillAmount = Mathf.MoveTowards(ProgressBar.fillAmount, progress, speed * Time.deltaTime);
            onProgress.Invoke();
            yield return null;
        }
    }



}
