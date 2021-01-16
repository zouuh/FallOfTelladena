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
    int margin = 10;
    int titleFontSize = 16;
    int containerHeight = 420;

    int nbOfQuests = 0;
    //List<Quest> listOfQuests;

    #endregion

    #region MonoBehaviour
    public void addQuest()
    {
        GameObject clone = Instantiate(questPrefab, transform.position, transform.rotation);

        string text = (Random.Range(0.0f, 1.0f)>0.5f)?"bla":"qlnflqnflqbvlbqlvjlqjcksdvblhsdb vm sdmv bmqkdjsvb jqdsvqùsi nvùsondv ùsdbvùjbs dmbwsm vbwsmdbv mwdbv mwbdm vsdwbm ";
        clone.GetComponent<QuestController>().changeDescription(text);
        //clone.GetComponent<QuestController>().orderInList = nbOfQuests;

        clone.transform.SetParent(transform, false);

        // Resize quest to fit content
        clone.GetComponent<QuestController>().resizeWithoutDescription();

        // Resize container to fit content
        resize();

        ++nbOfQuests;
    }

    public void resize()
    {
        float newHeight = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
            newHeight += margin + transform.GetChild(i).gameObject.GetComponent<RectTransform>().sizeDelta.y;
        }
        newHeight += margin;
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(this.GetComponent<RectTransform>().sizeDelta.x, newHeight > containerHeight ? newHeight : containerHeight);

    }
    #endregion
}
