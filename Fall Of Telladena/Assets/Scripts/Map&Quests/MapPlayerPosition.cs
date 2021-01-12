using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayerPosition : MonoBehaviour
{
    GameObject player;
    GameObject map;

    List<Vector2> origins = new List<Vector2>();
    List<Vector2> zones = new List<Vector2>();

    private void Awake()
    {
        player = GameObject.Find("Oksusu");
        map = GameObject.Find("MapContainer");

        // Create origins array
        origins.Add(new Vector2(-21f, 153f)); // Inside Castle
        origins.Add(new Vector2(-21f, 129f)); // Outside Castle
        origins.Add(new Vector2(-11f, 77f)); // Test Village
        origins.Add(new Vector2(-11f, 77f)); // Village
        origins.Add(new Vector2(-21f, 153f)); // Maze
        origins.Add(new Vector2(-21f, 153f)); // Outside Stairs

        // Create zones array
        zones.Add(new Vector2(.1f, .05f)); // Inside Castle
        zones.Add(new Vector2(.15f, .07f)); // Outside Castle
        zones.Add(new Vector2(.26f, .2f)); // Test Village
        zones.Add(new Vector2(.26f, .2f)); // Village
        zones.Add(new Vector2(.1f, .05f)); // Maze
        zones.Add(new Vector2(.1f, .05f)); // Outside Stairs
    }
    public void updatePlayerPosition()
    {
        Debug.Log(transform.localPosition);
        //float widthMap = map.transform.localScale.x;
        float widthWorld = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        Vector2 origin = origins[currentScene.buildIndex];
        Vector2 zone = zones[currentScene.buildIndex];

        transform.localPosition = new Vector2(origin.x + zone.x * player.transform.localPosition.x / widthWorld, origin.y + zone.y * player.transform.localPosition.y / widthWorld);
    }
}
