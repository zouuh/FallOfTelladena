using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Cinemachine;

public class FloattingText : MonoBehaviour
{
    [SerializeField]
    float fontSize = 16f;
    [SerializeField]
    Color normalColor = new Color(0, 0, 0);
    [SerializeField]
    Color disableColor = new Color(100, 100, 100);
    [SerializeField]
    Color normalTextColor = new Color(0, 0, 0);
    [SerializeField]
    Color disableTextColor = new Color(100, 100, 100);
    [SerializeField]
    string actionName = "Use";

    // Children
    [SerializeField]
    TextMeshProUGUI text;
    [SerializeField]
    Image bg;

    public bool disabled = false;

    public void UpdateText(string missingTool = "")
    {
        if (missingTool.Length > 0)
        {
            // can perform the action
            text.color = disableTextColor;
            bg.color = disableColor;

            text.text = actionName + " (missing " + missingTool + ")";

            disabled = true;


        }
        else
        {
            // cannot perform the action (disabled text)
            // can perform the action
            text.color = normalTextColor;
            bg.color = normalColor;

            text.text = actionName;

            disabled = false;
        }
    }
    public void activate(string missingTool = "")
    {
        UpdateText(missingTool);
        gameObject.SetActive(true);
    }

    public void desactivate()
    {
        gameObject.SetActive(false);
    }

    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        //var targetRot = Quaternion.LookRotation(Camera.main.transform.position - transform.position);
        //transform.rotation = targetRot;
        //transform.LookAt(Camera.main.transform);
        //transform.rotation *= Quaternion.FromToRotation(Vector3.left, Vector3.forward);

        Camera camera = Camera.main;
        transform.LookAt(camera.transform.position + camera.transform.rotation * Vector3.forward, camera.transform.rotation * Vector3.up);

        //transform.LookAt(transform.position + Quaternion.Angle(transform.rotation, camera.transform.rotation)* Vector3.forward, camera.transform.rotation * Vector3.up);
    }
}
