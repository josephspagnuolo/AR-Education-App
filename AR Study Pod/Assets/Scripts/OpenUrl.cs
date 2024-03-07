using UnityEngine;

public class OpenLinkScript : MonoBehaviour
{
    public void OpenLink(string url)
    {
        Application.OpenURL(url);
    }
}
