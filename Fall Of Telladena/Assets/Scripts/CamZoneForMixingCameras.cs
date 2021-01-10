/*
 * Authors : (Notslot), Manon
 */
using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

[RequireComponent(typeof(Collider))]
public class CamZoneForMixingCameras : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private CinemachineMixingCamera virtualCamera = null;

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
