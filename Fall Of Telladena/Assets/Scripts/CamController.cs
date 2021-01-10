/*
 * Authors : (Notslot), Manon
 */

using Cinemachine;
using UnityEngine;
using System.Collections.Generic;

public class CamController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private List<CinemachineVirtualCamera> virtualCameras = null;
    [SerializeField]
    private CinemachineVirtualCamera virtualCameraMain = null;
    //[SerializeField]
    //private List<CinemachineVirtualCamera> virtualCamerasMain = null;

    #endregion

    #region MonoBehavior

    private void Start()
    {
        foreach(CinemachineVirtualCamera cam in virtualCameras)
        {
            cam.enabled = false;
        }
        //virtualCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            foreach (CinemachineVirtualCamera cam in virtualCameras)
            {
                cam.enabled = true;
            }
            //virtualCamera.enabled = true;
        }
        else
        {
            foreach (CinemachineVirtualCamera cam in virtualCameras)
            {
                if (cam.enabled)
                {
                    //virtualCameraMain.enabled = false;
                    virtualCameraMain.transform.eulerAngles = new Vector3(30, virtualCameraMain.m_Follow.rotation.eulerAngles.y, 0);
                    cam.enabled = false;
                    //virtualCameraMain.enabled = true;
                }                
            }
            /*
            if (virtualCamera.enabled)
            {
                virtualCamera.enabled = false;
            }
            */
        }
    }

    #endregion
}
