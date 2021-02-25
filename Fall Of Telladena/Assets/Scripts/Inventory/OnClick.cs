using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OnClick : MonoBehaviour {
	public Button yourButton;
    public static int test = 0;
    public static bool result = false;

	void Start () {
		Button btn = yourButton.GetComponent<Button>();
		btn.onClick.AddListener(TaskOnClick);
	}

	public static void TaskOnClick(){
		Debug.Log ("You have clicked the button!" + test);
        test += 1;
        if (test == 1){
            result = true;

        }
        else {
            result = false;
        }
	}
}
