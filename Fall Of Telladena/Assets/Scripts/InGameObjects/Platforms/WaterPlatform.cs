using UnityEngine;

public class WaterPlatform : MonoBehaviour
{
    [SerializeField]
    WaterPlatformController myController;
    public void animationEnd()
    {
        Debug.Log("animation end");
        // free player
        myController.myPlayer.enabled = true;
        // allow new water
        myController.animationIsEnded = true;
    }
}
