using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TermsAndConditions : MonoBehaviour
{
    public GameObject termsPopup;
    public Button acceptButton;

    void Start()
    {
        // Show the popup at the start
        //ShowTermsPopup();

        // Add listeners to buttons
        acceptButton.onClick.AddListener(AcceptTerms);
    }

    public void AcceptTerms()
    {
        Debug.Log("AcceptTerms called");
        //SceneManager.LoadScene("OpeningScene");
        termsPopup.SetActive(false);
        Debug.Log("termsPopup isActive after SetActive(false): " + termsPopup.activeSelf);
        
        // Proceed to the next scene or main menu

    }

}
