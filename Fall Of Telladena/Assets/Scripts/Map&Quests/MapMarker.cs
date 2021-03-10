/* 
 * Authors : Manon
 */

using UnityEngine;
using UnityEngine.EventSystems;

public class MapMarker : MonoBehaviour, IPointerClickHandler
{
    Vector2 position;

    #region MonoBehaviour
    private void Start()
    {
        this.gameObject.SetActive(false);
    }
    #endregion
    public void activateAtPosition(Vector2 position)
    {
        transform.position = position;
        gameObject.SetActive(true);
    }

    #region IPointerClickHandler
    public void OnPointerClick(PointerEventData e)
    {
        gameObject.SetActive(false);
    }
    #endregion
}
