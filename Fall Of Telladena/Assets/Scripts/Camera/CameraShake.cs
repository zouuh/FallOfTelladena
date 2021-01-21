using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    CinemachineFreeLook cinemachineVirtualCamera;
    float shakeTimer;
    public int timeBetweenShake; // in seconds
    int noShakeTimer;
    void Awake()
    {
        cinemachineVirtualCamera = GetComponent<CinemachineFreeLook>();
        noShakeTimer = timeBetweenShake*60;
    }

    public void ShakeCamera(float intensity, float time)
    {
        /*
        cinemachineVirtualCamera.GetRig(0).GetCinemachineComponent<NoiseSettings>()
        CinemachineBasicMultiChannelPerlin cinemachineBasicChannelMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = intensity;
        */
        shakeTimer = time;
    }

    private void Update()
    {
        --noShakeTimer;
        if (noShakeTimer <= 0)
        {
            ShakeCamera(1, shakeTimer);
            noShakeTimer = timeBetweenShake*60;
        }
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if(shakeTimer <= 0)
            {
                // Timer over!
                /*
                CinemachineBasicMultiChannelPerlin cinemachineBasicChannelMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = 0f;
                */
            }
        }
    }
}
