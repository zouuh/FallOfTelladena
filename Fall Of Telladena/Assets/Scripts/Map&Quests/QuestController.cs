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
    int titleFontSize = 32;
    float size = 32;
    float newSize = 32;
    bool expanded = true;
    float step = 0.5f;
    float diff = 0.0f;
    #endregion

    #region MonoBehaviour
    void Start()
    {
        questContainer = GameObject.Find("QuestsContainer").gameObject.GetComponent<QuestsController>();
        //questsScrollView = GameObject.Find("QuestsScrollView").gameObject.GetComponent<ScrollView>();
    }

    void Update()
    {
        if (!expanded)
        {
            if (diff < 0)
            {
                if (size >= newSize)
                {
                    expanded = true;
                    // Resize container to fit content
                    questContainer.resize();
                }
                else
                {
                    size += step;
                    this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, size);
                }
            }
            else
            {
                if (size <= newSize)
                {
                    expanded = true;
                    // Resize container to fit content
                    questContainer.resize();
                }
                else
                {
                    size -= step;
                    this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, size);
                }
            }
            
            
        }
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
        //questContainer.resize();
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
        //questContainer.resize();
        description.SetActive(true);

        // pointer show PNJ
    }

    public void resizeWithDescription()
    {
        size = this.GetComponent<RectTransform>().sizeDelta.y;
        newSize = 3 * margin + titleFontSize + tmp.fontSize + (Mathf.Floor(tmp.text.Length / (350 / tmp.fontSize)) + 1) * tmp.fontSize;
        step = (newSize>=size ? (newSize - size) / 5.0f : (size - newSize) / 5.0f);
        diff = size - newSize;
        expanded = false;
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 3 * margin + titleFontSize + tmp.fontSize + (Mathf.Floor(tmp.text.Length / (200 / tmp.fontSize)) + 1) * tmp.fontSize);
    }

    public void resizeWithoutDescription()
    {
        size = this.GetComponent<RectTransform>().sizeDelta.y;
        newSize = 3 * margin + titleFontSize + tmp.fontSize;
        step = (newSize >= size ? (newSize - size) / 5.0f : (size - newSize) / 5.0f);
        diff = size - newSize;
        expanded = false;
        //this.GetComponent<RectTransform>().sizeDelta = new Vector2(200, 3 * margin + titleFontSize + tmp.fontSize);
    }

    public void changeDescription(string newDescription)
    {
        tmp.text = newDescription;
    }
}
