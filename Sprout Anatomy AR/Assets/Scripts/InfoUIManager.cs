using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InfoUIManager : MonoBehaviour
{
    public TMP_Text nameText;
    public TMP_Text infoText;
    public Image backgroundImage;

    public void UpdateInfoText(string partName, string additionalInfo)
    {
        if (infoText != null && backgroundImage != null)
        {
            StopAllCoroutines();
	    if (partName == "")
	    {
		StartCoroutine(TypeText(partName, additionalInfo));
		backgroundImage.gameObject.SetActive(false);
	    }
	    else 
	    {
		StartCoroutine(TypeText(partName, additionalInfo));
		backgroundImage.gameObject.SetActive(true);
	    }
        }
    }

    IEnumerator TypeText(string partName, string additionalInfo)
    {
        nameText.text = partName;
	infoText.text = "";
        foreach (char character in additionalInfo.ToCharArray())
        {
            infoText.text += character;
            yield return new WaitForSeconds(0.03f);
        }
    }
}
