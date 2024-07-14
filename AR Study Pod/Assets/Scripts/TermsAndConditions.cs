using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TermsAndConditions : MonoBehaviour
{
    public GameObject termsPopup;
    public Button acceptButton;
    public Button declineButton;

    void Start()
    {
        // Show the popup at the start
        //ShowTermsPopup();

        // Add listeners to buttons
        acceptButton.onClick.AddListener(AcceptTerms);
        declineButton.onClick.AddListener(DeclineTerms);
    }

/*    void ShowTermsPopup()
    {
        Debug.Log("ShowTermsPopup called");
        termsPopup.SetActive(true);
        Debug.Log("termsPopup isActive: " + termsPopup.activeSelf);
    }
*/
    void AcceptTerms()
    {
        Debug.Log("AcceptTerms called");
        //SceneManager.LoadScene("OpeningScene");
        termsPopup.SetActive(false);
        Debug.Log("termsPopup isActive after SetActive(false): " + termsPopup.activeSelf);
        
        // Proceed to the next scene or main menu

    }

    void DeclineTerms()
    {
        Debug.Log("DeclineTerms called");
        termsPopup.SetActive(false);
        Debug.Log("termsPopup isActive after SetActive(false): " + termsPopup.activeSelf);
        // Optionally, show a message or close the app
        Application.Quit();
    }
}
