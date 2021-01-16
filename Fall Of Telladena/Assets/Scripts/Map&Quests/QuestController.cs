/*
 * Authors : Manon
 */

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class QuestController : MonoBehaviour, IPointerClickHandler
{
    #region Fields
    [SerializeField]
    GameObject description;
    QuestsController questContainer;
    //ScrollView questsScrollView;
    //public int orderInList = 0;

    [SerializeField]
    TextMeshProUGUI tmp;
    int margin = 10;
    int titleFontSize = 16;
    #endregion

    #region MonoBehaviour
    void Start()
    {
        questContainer = GameObject.Find("QuestsContainer").gameObject.GetComponent<QuestsController>();
        //questsScrollView = GameObject.Find("QuestsScrollView").gameObject.GetComponent<ScrollView>();
    }
    #endregion

    #region IPointerClickHandler
    public void OnPointerClick(PointerEventData e)
    {
        if (gameObject.CompareTag("activeQuest"))
        {
            CloseQuest();
        }
        else
        {
            // close other quests if needed
            GameObject[] otherQuests = GameObject.FindGameObjectsWithTag("activeQuest");
            foreach (GameObject quest in otherQuests)
            {
                quest.GetComponent<QuestController>().CloseQuest();
            }
            // open this quest
            OpenQuest();
        }
    }
    #endregion

    void CloseQuest()
    {
        gameObject.tag = "unactiveQuest";

        // aspect change
        GetComponent<Image>().color = Color.white;

        // order in list change
        //GetComponent<RectTransform>().SetSiblingIndex(orderInList);

        // description expend
        resizeWithoutDescription();
        questContainer.resize();
        description.SetActive(false);

        // pointer show PNJ
    }

    void OpenQuest()
    {
        gameObject.tag = "activeQuest";

        // aspect change
        GetComponent<Image>().color = Color.grey;

        // order in list change
        //GetComponent<RectTransform>().SetAsFirstSibling();
        //scroll to top

        // description expend
        // Resize quest to fit content
        resizeWithDescription();
        // Resize container to fit content
        questContainer.resize();
        description.SetActive(true);

        // pointer show PNJ
    }

    public void resizeWithDescription()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 3 * margin + titleFontSize + tmp.fontSize + (Mathf.Floor(tmp.text.Length / (200 / tmp.fontSize)) + 1) * tmp.fontSize);
    }

    public void resizeWithoutDescription()
    {
        this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 3 * margin + titleFontSize + tmp.fontSize);
    }

    public void changeDescription(string newDescription)
    {
        tmp.text = newDescription;
    }
}
