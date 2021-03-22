/*
 * Authors : Manon
 */

using UnityEngine;
using Cinemachine;
using System.Collections;

public class BimbopCave : MonoBehaviour
{
    [SerializeField]
    CinemachineVirtualCamera camForCinematic;

    private void Start()
    {
        camForCinematic.enabled = false;
        camForCinematic.Priority = 20;
    }

    public void Appear()
    {
        // launch cinematic
        StartCoroutine(LaunchCinematic());
    }

    IEnumerator LaunchCinematic()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        // fix player
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<CharacterController>().enabled = false;

        // enable cinematic camera
        camForCinematic.enabled = true;

        Debug.Log("Launch cinematic");
        // move cave
        GetComponent<Animation>().Play("BimbopCave");

        // shake camera
        CinemachineBasicMultiChannelPerlin cinemachineBasicChannelMultiChannelPerlin = camForCinematic.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = 1f;

        yield return new WaitForSeconds(5);

        Debug.Log("End cinematic");

        // stop camShake
        cinemachineBasicChannelMultiChannelPerlin.m_AmplitudeGain = 0f;

        // disable cinematic camera
        camForCinematic.enabled = false;

        // allow player movements
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<CharacterController>().enabled = true;
    }
}
