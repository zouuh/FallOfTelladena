using Cinemachine;
using UnityEngine;

public class DollyController : MonoBehaviour
{
    bool play = false;
    [SerializeField]
    CinemachineVirtualCamera dolly;
    void OnEnabled()
    {
        play = true;
    }

    void Update()
    {
        if (play)
        {
            dolly.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition += .025f;
        }
    }
}
