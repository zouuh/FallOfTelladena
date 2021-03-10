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
        float scale = mapContainer.transform.localScale.x + Input.mouseScrollDelta.y * 0.1f;
        scale = scale < 1 ? 1 : scale;
        mapContainer.transform.localScale = new Vector2(scale, scale);
    }
}