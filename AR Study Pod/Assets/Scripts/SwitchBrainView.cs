using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBrainView : MonoBehaviour
{

    public GameObject BrainObject1;
    public GameObject BrainObject2;
    public GameObject BrainObject3;
    public Button Button1;
    public Button Button2;
    public Button Button3;
    private int activeView = 1;
    public string hexColor = "#9FBAA7";

    public void SwitchView(int viewIndex)
    {
	activeView = viewIndex;
        if (activeView == 1)
        {
            BrainObject1.SetActive(true);
            BrainObject2.SetActive(false);
            BrainObject3.SetActive(false);
            Button1.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
            Button2.image.color = Color.white;
            Button3.image.color = Color.white;
        }
        else if (activeView == 2)
        {
            BrainObject1.SetActive(false);
            BrainObject2.SetActive(true);
            BrainObject3.SetActive(false);
            Button1.image.color = Color.white;
            Button2.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
            Button3.image.color = Color.white;
        }
        else
        {
            BrainObject1.SetActive(false);
            BrainObject2.SetActive(false);
            BrainObject3.SetActive(true);
            Button1.image.color = Color.white;
            Button2.image.color = Color.white;
            Button3.image.color = ColorUtility.TryParseHtmlString(hexColor, out Color color) ? color : Color.green;
        }
    }
}
