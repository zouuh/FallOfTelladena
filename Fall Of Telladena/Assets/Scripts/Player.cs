using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int level = 3;
    public int health = 40;
    public Text levelText;
    public Text healthText;

    void Update() {
        levelText.text = level.ToString();
        healthText.text = health.ToString();
    }

    public void DicreaseHealth() {
        health --;
    }

    public void AddLevel() {
        level ++;
    }
    public void SavePlayer() {
        Debug.Log("Save BOB");
        SaveSystem.SavePlayer(this);
    }
    
    public void LoadPlayer() {
        Debug.Log("Load BOB");
        PlayerData data = SaveSystem.LoadPlayer();

        level = data.level;
        health = data.health;

        //fonctionne pas pour la pos car character controller
        Vector3 position;
        position.x = data.position[0];
        position.y = data.position[1];
        position.z = data.position[2];
        Debug.Log("pos1 : " + transform.position);
        transform.position = new Vector3(0f, 0f, 0f);
        Debug.Log("pos2 : " + transform.position);
    }
}
