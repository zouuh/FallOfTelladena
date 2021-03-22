/*
 * Authors : Manon
 */

using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class MapClickHandler : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    List<MapMarker> markers = new List<MapMarker>(3);
    int nbOfActiveMarkers = 0;

    #region MonoBehaviour
    void Start() {
        /*
        markers.Add(transform.Find("Marker (1)").GetComponent<MapMarker>());
        markers.Add(transform.Find("Marker (2)").GetComponent<MapMarker>());
        markers.Add(transform.Find("Marker (3)").GetComponent<MapMarker>());
        */
    }
    #endregion

    #region IPointerCLickHandler
    public void OnPointerClick(PointerEventData e)
    {
        foreach (MapMarker marker in markers)
        {
            if (!marker.isActiveAndEnabled)
            {
                marker.activateAtPosition(e.pressPosition);
                break;
            }
        }
    }
    #endregion
}
