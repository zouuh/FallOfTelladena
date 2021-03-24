using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematicTimer : MonoBehaviour
{
    [SerializeField]
    GameObject canvasToActive;
    [SerializeField]
    float timer = 111f; // in seconds

    private void Start()
    {
        StartCoroutine("EndCinematic");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StopCoroutine("EndCinematic");
            canvasToActive.SetActive(true);
            gameObject.SetActive(false);

        }
    }

    IEnumerator EndCinematic()
    {
        yield return new WaitForSeconds(timer);
        canvasToActive.SetActive(true);
        gameObject.SetActive(false);
    }
}
