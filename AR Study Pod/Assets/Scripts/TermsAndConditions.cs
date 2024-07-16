using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TermsAndConditions : MonoBehaviour
{
    public GameObject termsPopup;
    public Button acceptButton;

    void Start()
    {
	int accepted = PlayerPrefs.GetInt("accepted");
	if (accepted == 1)
	{
	    termsPopup.SetActive(false);
	}
	else
	{
	    termsPopup.SetActive(true);
	}
        acceptButton.onClick.AddListener(AcceptTerms);
    }

    public void AcceptTerms()
    {
        termsPopup.SetActive(false);
	PlayerPrefs.SetInt("accepted", 1);
	PlayerPrefs.Save();
    }
}
