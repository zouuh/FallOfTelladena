using Cinemachine;
using UnityEngine;

public class AutoAssignCamLookAt : MonoBehaviour
{
    [SerializeField]
    string tagToLookAt = "Player";
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag(tagToLookAt).transform;
    }
}
