using Cinemachine;
using UnityEngine;

public class AutoAssignCamFollow : MonoBehaviour
{
    [SerializeField]
    string tagToLookAt = "Player";
    void Start()
    {
        GetComponent<CinemachineVirtualCamera>().Follow = GameObject.FindGameObjectWithTag(tagToLookAt).transform;
    }
}