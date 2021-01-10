/*
 * Authors : Manon
 */

using UnityEngine;
using Cinemachine;

public class CamReset : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera virtualCameraReset = null;

    public void resetPositionAndRotation()
    {
        Debug.Log("reset");
        enabled = false;
        /*
        transform.position = virtualCameraReset.transform.position;
        transform.rotation = virtualCameraReset.transform.rotation;
        */
        virtualCameraReset.enabled = false;
        virtualCameraReset.enabled = true;
        //virtualCameraReset.enabled = false;
        enabled = true;
        //virtualCameraReset.enabled = false;
    }
}
