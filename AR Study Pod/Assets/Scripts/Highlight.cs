using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Highlight : MonoBehaviour
{
    public GameObject objectToHighlight;
    private Renderer objectRenderer;
    private Color originalColor;
    private Color referenceColor = new Color(255, 219, 0f);
    private bool isHighlighted = false;
    private bool flashingIn = true;
    private bool startedFlashing = false;

    List<InfoBehavior> infos = new List<InfoBehavior>();

    private void Start()
    {
        objectRenderer = objectToHighlight.GetComponent<Renderer>();
        originalColor = objectRenderer.material.color;
        infos = FindObjectsOfType<InfoBehavior>().ToList();

    }

    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            if (objectToHighlight == GetClickedObject(out RaycastHit hit))
            {
                GameObject go = hit.collider.gameObject;
                Debug.Log("Clicked");
                ChangeObjectMaterial();
                ToggleInfo(go.GetComponent<InfoBehavior>());
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("not Clicked");
        }
    }

    void ToggleInfo(InfoBehavior desiredInfo)
    {
        foreach (InfoBehavior info in infos)
        {
            if (info == desiredInfo)
            {
                if (isHighlighted)
                {
                    info.openInfo();
                }
                else
                {
                    info.closeInfo();
                }
                
               
            }
            else
            {
                info.closeInfo();
            }
        }
    }

    void CloseALl()
    {
        foreach (InfoBehavior info in infos)
        {
            info.closeInfo();
        }
    }

    GameObject GetClickedObject(out RaycastHit hit)
    {
        GameObject target = null;
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray.origin, ray.direction * 10, out hit))
        {
            if (!isPointerOverUIObject())
            {
                target = hit.collider.gameObject;
            }
        }
        return target;
    }
    private bool isPointerOverUIObject()
    {
        PointerEventData ped = new PointerEventData(EventSystem.current);
        ped.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(ped, results);
        return results.Count > 0;
    }











    public void ChangeObjectMaterial()
    {
        isHighlighted = !isHighlighted;
        if (isHighlighted)
        {
            startedFlashing = !startedFlashing;
            startedFlashing = true;
            StartCoroutine(HighlightFlash());
            Debug.Log("isHighlighted: " + isHighlighted);
            Debug.Log("Original Red: " + originalColor.r * 255);
        }
        else
        {
            startedFlashing = false;
            Debug.Log("isHighlighted: " + isHighlighted);
        }
    }

    private IEnumerator HighlightFlash()
    {
        objectRenderer.material.color = referenceColor;
        while (startedFlashing)
        {
            yield return new WaitForSeconds(0.08f);

            if (flashingIn)
            {
                if (objectRenderer.material.color.r * 255 <= 30)
                {
                    flashingIn = false;
                }
                else
                {
                    objectRenderer.material.color = new Color32(
                        (byte)(objectRenderer.material.color.r * 255 - 25),
                        (byte)(objectRenderer.material.color.g * 255 - 25),
                        (byte)(objectRenderer.material.color.b), 255);
                }
            }
            else
            {
                if (objectRenderer.material.color.r * 255 >= 150)
                {
                    flashingIn = true;
                }
                else
                {
                    objectRenderer.material.color = new Color32(
                        (byte)(objectRenderer.material.color.r * 255 + 25),
                        (byte)(objectRenderer.material.color.g * 255 + 25),
                        (byte)(objectRenderer.material.color.b), 255);
                }
            }
        }
        objectRenderer.material.color = originalColor;
    }
}
