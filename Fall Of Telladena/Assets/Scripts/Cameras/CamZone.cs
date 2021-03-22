/*
 * Authors : (Notslot), Manon
 */

using Cinemachine;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CamZone : MonoBehaviour
{
    #region Fields

    [SerializeField]
    public CinemachineVirtualCamera virtualCamera = null;

    #endregion

    #region MonoBehavior

    private void Start()
    {
        virtualCamera.enabled = false;
        virtualCamera.Priority = 20;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            virtualCamera.enabled = false;
        }
    }

    private void OnValidate()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    #endregion
}
