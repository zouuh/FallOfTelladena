/*
 * Authors : (Notslot), Manon
 */

using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private string virtualCameraTag = "";
    [SerializeField]
    private string virtualCameraMainTag = "";

    private CinemachineVirtualCamera virtualCamera = null;
    private CinemachineVirtualCamera virtualCameraMain = null;
    //CinemachineConfiner virtualCameraMainConfiner;
    /*
    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;
    [SerializeField]
    private CinemachineVirtualCamera virtualCameraMain = null;
    */

    #endregion

    #region MonoBehavior

    private void Start()
    {
        virtualCamera = GameObject.FindGameObjectWithTag(virtualCameraTag).GetComponent<CinemachineVirtualCamera>();
        virtualCameraMain = GameObject.FindGameObjectWithTag(virtualCameraMainTag).GetComponent<CinemachineVirtualCamera>();
        //virtualCameraMainConfiner = virtualCameraMain.GetComponent<CinemachineConfiner>();
        virtualCamera.enabled = false;
        //virtualCameraMainConfiner.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            virtualCamera.enabled = true;
            virtualCameraMain.enabled = false;
            //virtualCameraMainConfiner.enabled = true;
        }
        else
        {
            if (virtualCamera.enabled)
            {
                /*
                // refocus camera on player's rotation
                virtualCameraMain.transform.eulerAngles = new Vector3(30, virtualCameraMain.m_Follow.rotation.eulerAngles.y, 0);
                */
                //virtualCameraMain.enabled = true;
                //virtualCameraMainConfiner.enabled = false;

                // Maintain position between cams
                //virtualCameraMain.GetCinemachineComponent<CinemachineComposer>().m_TrackedObjectOffset = new Vector3(virtualCamera.transform.position.x, 0.9f, virtualCamera.transform.position.z);
                virtualCameraMain.transform.position = virtualCamera.transform.position;
                // Disable this cam
                virtualCamera.enabled = false;
                virtualCameraMain.enabled = true;
            }
        }
    }

    #endregion
}
