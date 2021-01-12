/*
 * Authors : Manon
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestsController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    GameObject questPrefab;
    //List<Quest> listOfQuests;

    #endregion

    #region MonoBehaviour
    public void addQuest()
    {
        GameObject clone = Instantiate(questPrefab, transform.position, transform.rotation);
        GameObject desc = clone.transform.Find("Description").gameObject;
        TextMeshProUGUI tmp = desc.GetComponent<TextMeshProUGUI>();
        tmp.text = (Random.Range(0.0f, 1.0f)>0.5f)?"bla":"qlnflqnflqbvlbqlvjlqjcksdvblhsdb vm sdmv bmqkdjsvb jqdsvqùsi nvùsondv ùsdbvùjbs dmbwsm vbwsmdbv mwdbv mwbdm vsdwbm ";
        //tmp.text = "bla";
        clone.transform.SetParent(transform, false);

        // Resize quest to fit content
        int margin = 10;
        int titleFontSize = 16;
        clone.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 3*margin+titleFontSize+(Mathf.Floor(tmp.text.Length/(200/tmp.fontSize))+1)*tmp.fontSize);
        
        // Resize container to fit content
        int containerHeight = 420;
        float newHeight = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            newHeight += margin + transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta.y;
        }
        newHeight += margin;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, newHeight>containerHeight?newHeight:containerHeight);

    }
    #endregion
}
