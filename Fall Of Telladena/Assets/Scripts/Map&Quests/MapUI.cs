/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private GameObject mapUI; // the entire map UI

    private GameObject player; // the player

    [SerializeField]
    private MapPlayerPosition playerPositionSprite; // the marker for player

    #endregion

    #region MonoBehaviour

    private void Start()
    {
        mapUI.SetActive(false);
        player = GameObject.Find("Oksusu");
    }

    void Update()
	{
		if (Input.GetButtonDown("OpenMap"))
		{
            // Open the map
            mapUI.SetActive(true);

            // Desactivate player movements
            player.GetComponent<CharacterController>().enabled = false;
            player.GetComponent<PlayerMovement>().enabled = false;

            // Update player position
            playerPositionSprite.UpdatePlayerPosition();
		}
        else if (Input.GetButtonDown("CloseMap"))
        {
            // Open the map
            mapUI.SetActive(false);

            // Desactivate player movements
            player.GetComponent<CharacterController>().enabled = true;
            player.GetComponent<PlayerMovement>().enabled = true;
        }
    }

	#endregion
}
