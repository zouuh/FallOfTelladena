using UnityEngine;

public class WaterPlatform : MonoBehaviour
{
    [SerializeField]
    WaterPlatformController myController;
    public void animationEnd()
    {
        // free player
        myController.myPlayer.enabled = true;
        // allow new water
        myController.animationIsEnded = true;
    }
}
