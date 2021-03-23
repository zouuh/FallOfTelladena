/*
 * Authors : Manon
 */

using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionCanvas : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI actionText;
    [SerializeField]
    Image background;
    /*
    [SerializeField]
    Sprite baseColor;
    [SerializeField]
    Sprite disableColor;


    public void UpdateText(string action, int amount = 1, string tool = "", bool active = true)
    {
        actionText.text = action;
        if (!active)
        {
            actionText.text += " (need " + (amount != 1 ? amount.ToString() + " " : "") + tool + ")";
            background.sprite = disableColor;
            background.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 40f, 250f);
        }
        else
        {
            background.sprite = baseColor;
            background.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 40f, 150f);
        }
    }
}
