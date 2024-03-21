using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

public class SmoothHighlight : MonoBehaviour
{
    public List<GameObject> objectsToHighlight;
    private Renderer[] objectRenderers;
    private Color[] originalColours;
    //private Color highlightColour = new Color(1f, 0.86f, 0f); // Bright yellow
    private Color highlightColour = new Color(219, 255, 0f);
    public float strength = -1f;
    private GameObject currentHighlightedObject = null;
    private Coroutine flashingCoroutine;

    List<InfoBehavior> infos;
    public InfoUIManager infoUIManager;

    void Start()
    {
        objectRenderers = objectsToHighlight.Select(obj => obj.GetComponent<Renderer>()).ToArray();
        originalColours = objectRenderers.Select(renderer => renderer.material.color).ToArray();
        infos = FindObjectsOfType<InfoBehavior>().ToList();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!IsPointerOverUIObject())
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Input.touchCount > 0)
                {
                    ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
                }

                if (Physics.Raycast(ray, out hit))
                {
                    GameObject hitObject = hit.collider.gameObject;
                    if (objectsToHighlight.Contains(hitObject))
                    {
                        if (currentHighlightedObject != null && currentHighlightedObject != hitObject)
                        {
                            StopFlashing(currentHighlightedObject);
                            UnhighlightObject(currentHighlightedObject);
			    currentHighlightedObject = hitObject;
                            HighlightObject(currentHighlightedObject);
                            ToggleInfo(currentHighlightedObject.GetComponent<InfoBehavior>());
                        }
			else if (currentHighlightedObject != null && currentHighlightedObject == hitObject)
			{
			    StopFlashing(currentHighlightedObject);
                            UnhighlightObject(currentHighlightedObject);
			    currentHighlightedObject = null;
			}
			else
			{
			    currentHighlightedObject = hitObject;
                            HighlightObject(currentHighlightedObject);
                            ToggleInfo(currentHighlightedObject.GetComponent<InfoBehavior>());
			}
                    }
                }
            }
        }
	if (currentHighlightedObject != null && !currentHighlightedObject.activeSelf)
	{
            StopFlashing(currentHighlightedObject);
	    UnhighlightObject(currentHighlightedObject);
            currentHighlightedObject = null;
	}
    }

    void HighlightObject(GameObject obj)
    {
        int index = objectsToHighlight.IndexOf(obj);
	Color originalColour = originalColours[index];
        flashingCoroutine = StartCoroutine(FlashHighlight(objectRenderers[index], originalColour));

	ObjectInfo objectInfo = obj.GetComponent<ObjectInfo>();
	if (objectInfo != null)
	{
	    string partName = obj.GetComponent<ObjectInfo>().partName;
	    string additionalInfo = obj.GetComponent<ObjectInfo>().additionalInfo;
	    infoUIManager.UpdateInfoText(partName, additionalInfo);
	}
    }

    IEnumerator FlashHighlight(Renderer renderer, Color originalColour)
    {
   	float cycleDuration = 1.75f; // Adjust the speed of the flashing here

    	Color initialColour = renderer.material.color;

    	Color colourDifference = highlightColour - initialColour;

    	float time = 0f;

	if (strength < 0)
	{
	    strength = 0.002f;
	}

    	while (true)
    	{
            float t = Mathf.Sin((2 * Mathf.PI * time / cycleDuration) - (Mathf.PI / 2)) * strength + strength;
	    
            Color interpolatedColour = initialColour + colourDifference * t;
    
            //renderer.material.color = interpolatedColour;
	    for (int i = 0; i < renderer.materials.Length; i++)
            {
        	renderer.materials[i].color = interpolatedColour;
            }

            time += Time.deltaTime;

            yield return null;
  	}
    }


    void StopFlashing(GameObject obj)
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            int index = objectsToHighlight.IndexOf(obj);
            //objectRenderers[index].material.color = originalColours[index];
	    Renderer renderer = objectRenderers[index];
	    for (int i = 0; i < renderer.materials.Length; i++)
            {
        	renderer.materials[i].color = originalColours[index];
            }
        }
    }

    void UnhighlightObject(GameObject obj)
    {
        int index = objectsToHighlight.IndexOf(obj);
        //objectRenderers[index].material.color = originalColours[index];
	Renderer renderer = objectRenderers[index];
	for (int i = 0; i < renderer.materials.Length; i++)
        {
	    renderer.materials[i].color = originalColours[index];
        }
	infoUIManager.UpdateInfoText("", "");
    }

    void ToggleInfo(InfoBehavior infoBehavior)
    {
        foreach (var info in infos)
        {
            if (info == infoBehavior)
            {
                info.openInfo();
            }
            else
            {
                info.closeInfo();
            }
        }
    }

    bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
