using Unity.Cinemachine;
using UnityEngine;
using DG;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] float minDistance = 1, maxDistance = 30;
    [SerializeField] float minCameraDistance = 5, maxCameraDistance = 20;
    [SerializeField] float rotateSpeed = 1;
    CinemachineCamera vcamera;
    CinemachinePositionComposer composer;
    CinemachineTargetGroup targetGroup;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        vcamera = GetComponent<CinemachineCamera>();
        composer = GetComponent<CinemachinePositionComposer>();
        targetGroup = FindFirstObjectByType<CinemachineTargetGroup>();
        vcamera.Follow = targetGroup.transform;
        //transform.DORotate(new Vector3(0, 360, 0), 60f, RotateMode.FastBeyond360).SetRelative(true).SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        updateCameraDistance();
        if (Input.GetKeyDown(KeyCode.Space))
            StartCoroutine(shakeCamera(3,0.5f));
    }

    void updateCameraDistance()
    {
        float distance = maxTransformDistance();

        float normalizeDistance = Mathf.InverseLerp(minDistance, maxDistance, distance);

        float desiredDistance = Mathf.Lerp(minCameraDistance, maxCameraDistance, normalizeDistance);

        composer.CameraDistance = desiredDistance;
    }

    float maxTransformDistance()
    {
        float maxTransformDistance = Vector3.Distance(targetGroup.Targets[0].Object.position, targetGroup.Targets[0].Object.position);

        for (int i = 0; i < targetGroup.Targets.Count; i++)
        {
            for (int j = 0; j < targetGroup.Targets.Count; j++)
            {
                float distance = Vector3.Distance(targetGroup.Targets[i].Object.position, targetGroup.Targets[j].Object.position);
                if (distance > maxTransformDistance)
                    maxTransformDistance = distance;
            }
        }
        return maxTransformDistance;
    }

    public IEnumerator shakeCamera(float shakeStrange = 1, float time = 1)
    {
        CinemachineBasicMultiChannelPerlin channel = vcamera.GetComponent<CinemachineBasicMultiChannelPerlin>();
        channel.FrequencyGain = shakeStrange;
        Time.timeScale = 0f;
        for (int i = 0; i < time * 10; i++)
        {
            yield return new WaitForSecondsRealtime(0.1f);
            channel.FrequencyGain -= shakeStrange / (time * 10);
            Time.timeScale += 0.1f;
        }
        channel.FrequencyGain = 0;
        Time.timeScale = 1;
    }
}