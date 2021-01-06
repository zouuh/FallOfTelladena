using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    public int key = 0;
    public Text keyText;

    void Update() {
        keyText.text = key.ToString();
    }

    public void AddKey() {
        key ++;
    }
    public void SavePlayer() {
        SaveSystem.SavePlayer(this, this.name);
    }
    
    public void LoadPlayer() {
        PlayerData data = SaveSystem.LoadPlayer(this.name);

        key = data.key;

        //fonctionne pas pour la pos car character controller
        // Vector3 position;
        // position.x = data.position[0];
        // position.y = data.position[1];
        // position.z = data.position[2];
        // Debug.Log("pos1 : " + transform.position);
        // transform.position = new Vector3(0f, 0f, 0f);
        // Debug.Log("pos2 : " + transform.position);
    }
}
