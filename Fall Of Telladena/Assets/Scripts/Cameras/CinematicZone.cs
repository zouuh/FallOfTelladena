/*
 * Authors : Manon
 */

using Cinemachine;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CinematicZone : MonoBehaviour
{
    [SerializeField]
    bool playOnce = true;
    [SerializeField]
    string cinematicName;

    CinematicController cinematicController;

    [SerializeField]
    List<CinemachineVirtualCamera> virtualCameras = new List<CinemachineVirtualCamera>();
    [SerializeField]
    float durationForEachCam = 2f; // in seconds

    GameObject player = null;

    private void Start()
    {
        cinematicController = GameObject.FindGameObjectWithTag("GameManager").GetComponent<CinematicController>();
        if (cinematicController.CinematicPlayed.Contains(cinematicName))
        {
            RemoveEverything();
        }
        else
        {
            foreach (CinemachineVirtualCamera cam in virtualCameras)
            {
                cam.enabled = false;
                cam.Priority = 20;
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (playOnce)
            {
                cinematicController.CinematicPlayed.Add(cinematicName);

            }

            if(player == null)
            {
                player = other.gameObject;
            }

            StartCoroutine("Cinematic");
        }
    }

    IEnumerator Cinematic()
    {
        // stop player
        player.GetComponent<CharacterController>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;

        foreach(CinemachineVirtualCamera cam in virtualCameras)
        {
            // play cinematic
            cam.enabled = true;

            float savedDuration = durationForEachCam;
            /*
            if (cam.GetComponent<Animation>() != null)
            {
                cam.GetComponent<Animation>().enabled = true;
                //cam.GetComponent<Animation>().Play(cam.GetComponent<Animation>().clip.name);
                durationForEachCam = 3f;
            }
            */
            if (cam.GetComponent<Animator>() != null)
            {
                cam.GetComponent<Animator>().enabled = true;
                durationForEachCam = 3f;
            }

            // wait
            yield return new WaitForSeconds(durationForEachCam);

            // stop cinematic
            durationForEachCam = savedDuration;
            cam.enabled = false;
        }


        // enable player again
        player.GetComponent<CharacterController>().enabled = true;
        player.GetComponent<PlayerMovement>().enabled = true;
        //player.GetComponent<Animator>().SetFloat("speed", 0f);

        if (playOnce)
        {
            RemoveEverything();
        }
    }

    void RemoveEverything()
    {
        foreach(CinemachineVirtualCamera cam in virtualCameras)
        {
            Destroy(cam.m_LookAt.gameObject);
            Destroy(cam.gameObject);
        }
        Destroy(gameObject);
    }
}
