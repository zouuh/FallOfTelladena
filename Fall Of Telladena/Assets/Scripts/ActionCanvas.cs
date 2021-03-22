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
    [SerializeField]
    Color baseColor = new Color(0, 0, 0);
    [SerializeField]
    Color disableColor = new Color(100, 100, 100);


    public void UpdateText(string action, int amount = 1, string tool = "", bool active = true)
    {
        actionText.text = action;
        if (!active)
        {
            actionText.text += " (nécessite " + (amount != 1 ? amount.ToString() + " " : "") + tool + ")";
            background.color = disableColor;
            background.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 40f, 250f);
        }
        else
        {
            background.color = baseColor;
            background.GetComponent<RectTransform>().SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, 40f, 150f);
        }
    }
}
