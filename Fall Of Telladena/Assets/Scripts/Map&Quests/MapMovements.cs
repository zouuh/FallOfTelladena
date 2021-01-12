/*
 * AUthors : Manon
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MapMovements : ScrollRect
{
    GameObject mapContainer;
    protected override void Start()
    {
        mapContainer = transform.GetChild(0).gameObject;
    }
    public override void OnScroll(PointerEventData data)
    {
        /*
        Vector2 currentScale = mapContainer.transform.localScale;
        currentScale.x += Input.mouseScrollDelta.y * 0.1f;
        currentScale.y += Input.mouseScrollDelta.y * 0.1f;
        if (currentScale.x < 1){
            currentScale.x = 1;
        }
        if(currentScale.y < 1)
        {
            currentScale.y = 1;
        }*/
        float scale = mapContainer.transform.localScale.x + Input.mouseScrollDelta.y * 0.1f;
        scale = scale < 1 ? 1 : scale;
        //Debug.Log(Input.mouseScrollDelta.y);
        mapContainer.transform.localScale = new Vector2(scale, scale);
    }
}
