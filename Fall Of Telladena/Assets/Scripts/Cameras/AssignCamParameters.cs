/*
 * Authors : Manon
 */

using UnityEngine;
using Cinemachine;

/*
 * Assign cameras parameters when scenes reload.
 */
public class AssignCamParameters : MonoBehaviour
{
    [SerializeField]
    string followTargetTag = "";

    void Awake()
    {
        Transform target = GameObject.FindGameObjectWithTag(followTargetTag).transform;
        this.GetComponent<CinemachineVirtualCamera>().Follow = target;
        this.GetComponent<CinemachineVirtualCamera>().LookAt = target;
    }
}
