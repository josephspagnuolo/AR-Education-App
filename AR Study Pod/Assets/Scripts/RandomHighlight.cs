/*using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class RandomHighlight : MonoBehaviour
{
    public List<GameObject> objectsToHighlight;
    private Renderer[] objectRenderers;
    private Color[] originalColors;
    private Color highlightColor = Color.yellow; // Change the highlight color to yellow
    private Coroutine flashingCoroutine;
    private GameObject currentHighlightedObject;

    private void Start()
    {
        objectRenderers = objectsToHighlight.Select(obj => obj.GetComponent<Renderer>()).ToArray();
        originalColors = objectRenderers.Select(renderer => renderer.material.color).ToArray();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!IsPointerOverUIObject())
            {
                GameObject randomObject = GetRandomObjectToHighlight();
                if (randomObject != null)
                {
                    HighlightObject(randomObject);
                }
            }
        }
    }

    private GameObject GetRandomObjectToHighlight()
    {
        if (objectsToHighlight.Count == 0)
        {
            Debug.LogError("No objects to highlight.");
            return null;
        }

        return objectsToHighlight[Random.Range(0, objectsToHighlight.Count)];
    }

    private void HighlightObject(GameObject obj)
    {
        if (currentHighlightedObject != null)
        {
            StopFlashing(currentHighlightedObject);
        }

        int index = objectsToHighlight.IndexOf(obj);
        flashingCoroutine = StartCoroutine(FlashHighlight(objectRenderers[index]));
        currentHighlightedObject = obj;
    }

    private IEnumerator FlashHighlight(Renderer renderer)
    {
        float cycleDuration = 1.75f;
        float strength = -1f;
        Color initialColor = renderer.material.color;
        Color colorDifference = highlightColor - initialColor;
        float time = 0f;

        while (true)
        {
            float t = Mathf.Sin((2 * Mathf.PI * time / cycleDuration) - (Mathf.PI / 2)) * strength + strength;
            Color interpolatedColor = initialColor + colorDifference * t;

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].color = interpolatedColor;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    private void StopFlashing(GameObject obj)
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            int index = objectsToHighlight.IndexOf(obj);
            for (int i = 0; i < objectRenderers[index].materials.Length; i++)
            {
                objectRenderers[index].materials[i].color = originalColors[index];
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class RandomHighlight : MonoBehaviour
{
    public List<GameObject> objectsToHighlight;
    private Renderer[] objectRenderers;
    private Color[] originalColors;
    private Color highlightColor = Color.yellow; // Change the highlight color to yellow
    private Coroutine flashingCoroutine;
    private GameObject currentHighlightedObject;

    public TextMeshProUGUI questionText;
    public Button option1Button;
    public Button option2Button;
    public Button option3Button;
    public Button option4Button;
    public Color startColor;

    

    private void Start()
    {
        objectRenderers = objectsToHighlight.Select(obj => obj.GetComponent<Renderer>()).ToArray();
        originalColors = objectRenderers.Select(renderer => renderer.material.color).ToArray();
        startColor = option1Button.GetComponent<Image>().color;
        HighlightRandomObject(); // Start by highlighting a random object
     
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
        {
            if (!IsPointerOverUIObject())
            {
                // User clicked outside of UI, highlight a new random object
                HighlightRandomObject();
            }
        }
    }

    private void HighlightRandomObject()
    {

        option1Button.GetComponent<Image>().color = startColor;
        option2Button.GetComponent<Image>().color = startColor;
        option3Button.GetComponent<Image>().color = startColor;
        option4Button.GetComponent<Image>().color = startColor;
        if (currentHighlightedObject != null)
        {
            StopFlashing(currentHighlightedObject);
        }

        GameObject randomObject = GetRandomObjectToHighlight();
        HighlightObject(randomObject);
        UpdateQuestionDisplay(randomObject);
    }

    private GameObject GetRandomObjectToHighlight()
    {
        if (objectsToHighlight.Count == 0)
        {
            Debug.LogError("No objects to highlight.");
            return null;
        }

        return objectsToHighlight[Random.Range(0, objectsToHighlight.Count)];
    }

    private void HighlightObject(GameObject obj)
    {
        int index = objectsToHighlight.IndexOf(obj);
        flashingCoroutine = StartCoroutine(FlashHighlight(objectRenderers[index]));
        currentHighlightedObject = obj;
    }

    private IEnumerator FlashHighlight(Renderer renderer)
    {
        float cycleDuration = 1.75f;
        float strength = -1f;
        Color initialColor = renderer.material.color;
        Color colorDifference = highlightColor - initialColor;
        float time = 0f;

        while (true)
        {
            float t = Mathf.Sin((2 * Mathf.PI * time / cycleDuration) - (Mathf.PI / 2)) * strength + strength;
            Color interpolatedColor = initialColor + colorDifference * t;

            for (int i = 0; i < renderer.materials.Length; i++)
            {
                renderer.materials[i].color = interpolatedColor;
            }

            time += Time.deltaTime;
            yield return null;
        }
    }

    private void StopFlashing(GameObject obj)
    {
        if (flashingCoroutine != null)
        {
            StopCoroutine(flashingCoroutine);
            int index = objectsToHighlight.IndexOf(obj);
            for (int i = 0; i < objectRenderers[index].materials.Length; i++)
            {
                objectRenderers[index].materials[i].color = originalColors[index];
            }
        }
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    private void UpdateQuestionDisplay(GameObject highlightedObject)
    {
        questionText.text = "Which cell component is currently highlighted?";
        List<GameObject> answerOptions = GetAnswerOptions(highlightedObject);

        // Set the answer options text for the buttons
        option1Button.GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[0].name;
        option2Button.GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[1].name;
        option3Button.GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[2].name;
        option4Button.GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[3].name;
    }

    private List<GameObject> GetAnswerOptions(GameObject highlightedObject)
    {
        List<GameObject> answerOptions = new List<GameObject>();

        // Add the highlighted object as the correct answer
        answerOptions.Add(highlightedObject);

        // Add three other random objects from the objectsToHighlight list
        List<GameObject> otherObjects = new List<GameObject>(objectsToHighlight);
        otherObjects.Remove(highlightedObject);
        for (int i = 0; i < 3; i++)
        {
            GameObject randomObject = otherObjects[Random.Range(0, otherObjects.Count)];
            answerOptions.Add(randomObject);
            otherObjects.Remove(randomObject);
        }

        // Shuffle the list to randomize the order of options
        answerOptions = answerOptions.OrderBy(x => Random.value).ToList();

        return answerOptions;
    }

    // Method to handle when an answer is selected

    // Method to handle when an answer is selected
    public void AnswerSelected(Button selectedButton)
    {
        string selectedOption = selectedButton.GetComponentInChildren<TextMeshProUGUI>().text;
        Debug.Log("Selected Option: " + selectedOption);
        Debug.Log("Correct Answer: " + currentHighlightedObject.name);

        // Check if the selected option matches the name of the currentHighlightedObject
        if (selectedOption == currentHighlightedObject.name)
        {
            selectedButton.GetComponent<Image>().color = Color.green;
            Debug.Log("Correct answer!");
            StartCoroutine(waitForNext());
           
            //HighlightRandomObject();
            // Optionally, you can add logic here to handle a correct answer
        }
        else
        {
            selectedButton.GetComponent<Image>().color = Color.red;
            Debug.Log("Incorrect answer. Try again.");
            // Optionally, you can add logic here to handle an incorrect answer
        }

        // Update the question and options for the next round
        
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        HighlightRandomObject();
    }
}

/*    public void AnswerSelected(string selectedOption)
    {
        if (selectedOption == currentHighlightedObject.name)
        {
            Debug.Log("Correct answer!");
            // Optionally, you can add logic here to handle a correct answer
        }
        else
        {
            Debug.Log("Incorrect answer. Try again.");
            // Optionally, you can add logic here to handle an incorrect answer
        }

        // Update the question and options for the next round
        HighlightRandomObject();
    }
}*/


