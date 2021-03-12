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
        if(nbOfOpenDoors < nbOfRequiredOpenDoors - 1)
        {
            GetComponent<Platform>().stepSize = 0.5f;
            return;
        }
        else
        {
            GetComponent<Platform>().stepSize = 10f;
        }
    }


}
