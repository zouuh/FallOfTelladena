using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineVirtualCamera cinemachineVirtualCamera;
    [SerializeField]
    float timeBetweenShake; // in seconds
    [SerializeField]
    float timeShaking; // in seconds
    [SerializeField]
    float intensity;

    private void Start()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
        InvokeRepeating("ShakeCameraStart", 0.0f, timeBetweenShake);
        InvokeRepeating("ShakeCameraStop", timeShaking, timeBetweenShake);
    }

    void ShakeCameraStart()
    {
        //cinemachineVirtualCamera.GetCinemachineComponent<NoiseSettings>();
        CinemachineBasicMultiChannelPerlin cinemachineBasicChannelMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = intensity;
    }

    void ShakeCameraStop()
    {
        //cinemachineVirtualCamera.GetCinemachineComponent<NoiseSettings>();
        CinemachineBasicMultiChannelPerlin cinemachineBasicChannelMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = 0;
    }
}
