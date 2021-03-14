/*
 * Authors : Manon
 */

using UnityEngine;

[RequireComponent(typeof(Platform))]
public class MazeDoorController : MonoBehaviour
{
    public int nbOfOpenDoors;
    [SerializeField]
    int nbOfRequiredOpenDoors = 4;

    public void ChangeStepSize()
    {
        if (GetComponent<Platform>().forwardOrBackward == -1)
        {
            if (nbOfOpenDoors <= nbOfRequiredOpenDoors - 1)
            {
                GetComponent<Platform>().stepSize = 0.5f;
                GetComponent<Platform>().animationDuration = 4f;
            }
            else
            {
                GetComponent<Platform>().stepSize = 5f;
                GetComponent<Platform>().animationDuration = 6f;
            }
        }
        else
        {
            if (nbOfOpenDoors > nbOfRequiredOpenDoors - 1)
            {
                GetComponent<Platform>().stepSize = 0.5f;
                GetComponent<Platform>().animationDuration = 4f;
            }
            else
            {
                GetComponent<Platform>().stepSize = 5f;
                GetComponent<Platform>().animationDuration = 6f;
            }
        }
    }


}
