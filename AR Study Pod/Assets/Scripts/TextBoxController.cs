using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TextBoxController : MonoBehaviour
{
    public void showchat(GameObject obj)
    {
        obj.SetActive(true);
    }
    public void hidechat(GameObject obj)
    {
        obj.SetActive(false);
    }
}
