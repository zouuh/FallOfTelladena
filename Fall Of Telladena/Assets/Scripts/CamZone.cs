﻿/*
 * Authors : (Notslot), Manon
 */
using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;
    [SerializeField]
    private CinemachineVirtualCamera virtualCameraReset = null;

    #endregion

    #region MonoBehavior

    private void Start()
    {
        /*
        foreach(CinemachineVirtualCamera cam in virtualCameras)
        {
            cam.enabled = false;
        }
        */
        virtualCamera.enabled = false;
        virtualCameraReset.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*
            foreach (CinemachineVirtualCamera cam in virtualCameras)
            {
                cam.enabled = true;
            }
            */
            /*
            virtualCameraReset.enabled = true;
            virtualCameraReset.enabled = false;
            */
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            /*
            foreach (CinemachineVirtualCamera cam in virtualCameras)
            {
                cam.enabled = false;
            }
            */
            virtualCamera.enabled = false;
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    #endregion
}
