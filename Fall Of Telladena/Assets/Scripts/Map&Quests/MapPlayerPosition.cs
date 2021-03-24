using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapPlayerPosition : MonoBehaviour
{
    GameObject player;
    GameObject map;

    List<Vector2> origins = new List<Vector2>();
    List<Vector2> zones = new List<Vector2>();
    List<Vector2> values = new List<Vector2>();

    private void Awake()
    {
        player = GameObject.Find("Oksusu");
        map = GameObject.Find("MapContainer");

        // Create origins array
        origins.Add(Vector2.zero); // Menu
        origins.Add(new Vector2(-297.8f, 59.9f)); // Village
        origins.Add(new Vector2(-470f, 156.2f)); // Outside Castle
        origins.Add(new Vector2(-470f, 177f)); // Inside Castle
        origins.Add(new Vector2(-470f, 177f)); // Outside Stairs
        origins.Add(new Vector2(-470f, 177f)); // Maze
        origins.Add(new Vector2(438f, -44f)); // Forest
        origins.Add(new Vector2(91f, -95f)); // Lands

        // Create zones array
        zones.Add(Vector2.zero); // Menu
        zones.Add(new Vector2(-440f, .2f)); // Village
        zones.Add(new Vector2(.15f, .07f)); // Outside Castle
        zones.Add(new Vector2(.1f, .05f)); // Inside Castle
        zones.Add(new Vector2(.1f, .05f)); // Outside Stairs
        zones.Add(new Vector2(.1f, .05f)); // Maze
        zones.Add(new Vector2(-21f, 153f)); // Forest
        zones.Add(new Vector2(-21f, 153f)); // Lands

        // Create array
        values.Add(Vector2.zero); // Menu
        //values.Add(new Vector2(-280f/-11.7f, 160f/-43.57f)); // Village
        //values.Add(new Vector2(.7266f, .4780f)); // Village
        values.Add(new Vector2(-1.7f, -2f)); // Village
        values.Add(new Vector2(0f, 0f)); // Outside Castle
        values.Add(new Vector2(0f, 0f)); // Inside Castle
        values.Add(new Vector2(0f, .0f)); // Outside Stairs
        values.Add(new Vector2(0f, .0f)); // Maze
        values.Add(new Vector2(0f, 0f)); // Forest
        values.Add(new Vector2(-1.7f, -2f)); // Lands
    }
    public void UpdatePlayerPosition()
    {
        //float widthMap = map.transform.localScale.x;
        float widthWorld = 1.0f;
        Scene currentScene = SceneManager.GetActiveScene();
        Vector2 origin = origins[currentScene.buildIndex];
        Vector2 zone = zones[currentScene.buildIndex];
        Vector2 value = values[currentScene.buildIndex];

        //transform.localPosition = new Vector2(origin.x + zone.x * player.transform.localPosition.x / widthWorld, origin.y + zone.y * player.transform.localPosition.z / widthWorld);
        transform.localPosition = new Vector2(origin.x + (player.transform.localPosition.x) * value.x, origin.y + (player.transform.localPosition.z) * value.y);
        //transform.localPosition = new Vector2((player.transform.localPosition.x) * value.x, (player.transform.localPosition.z) * value.y);

        Debug.Log(player.transform.localPosition);
        Debug.Log(transform.localPosition);
    }
}
