using Cinemachine;
using UnityEngine;

public class CamController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private CinemachineVirtualCamera virtualCamera = null;
    [SerializeField]
    private CinemachineVirtualCamera virtualCameraMain = null;

    #endregion

    #region MonoBehavior

    private void Start()
    {
        virtualCamera.enabled = false;
    }

    private void Update()
    {
        if (Input.GetMouseButton(1))
        {
            virtualCamera.enabled = true;
        }
        else
        {
            if (virtualCamera.enabled)
            {
                virtualCamera.enabled = false;
            }
        }
    }

    #endregion
}
