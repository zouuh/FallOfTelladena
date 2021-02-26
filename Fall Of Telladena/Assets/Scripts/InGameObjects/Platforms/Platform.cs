/*
 * Authors : Manon
 */

using UnityEngine;

public class Platform : MonoBehaviour
{
    public int nbOfSteps;
    public string axisToAnimate; // x, y, z
    public int axisId = 1;
    public int currStep = 0;
    public float stepSize = 1.0f;
    public int forwardOrBackward = 1; // 1 = forward, -1 = backward
    public bool animationEnd = true;

    public void EndAnimation()
    {
        animationEnd = true;
    }

    private void Start()
    {
        switch (axisToAnimate)
        {
            case "x":
                axisId = 0;
                break;
            case "y":
                axisId = 1;
                break;
            case "z":
                axisId = 2;
                break;
            default:
                axisId = 0;
                break;
        }
    }
}
