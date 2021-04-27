/*
 * Authors : Manon
 */

using Cinemachine;
using UnityEngine;

public class AutoAssignCamLookAt : MonoBehaviour
{
    [SerializeField]
    string tagToLookAt = "Player";

    // Assign player to camera's look at
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag(tagToLookAt).transform;
    }
}
