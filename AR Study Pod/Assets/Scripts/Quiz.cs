using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    public List<GameObject> objectsToHighlight;
    private List<GameObject> askedObjects = new List<GameObject>();
    private Renderer[] objectRenderers;
    private Color[][] originalColours;
    private Color highlightColour = new Color(219, 255, 0f);
    public float strength = -1f;
    private GameObject currentHighlightedObject = null;
    private Coroutine flashingCoroutine;

    public TextMeshProUGUI questionText;
    public GameObject[] options;
    public Color startColor;
    public string modelName;
    public GameObject GOPanel;
    public TextMeshProUGUI ScoreText;
    private int totalQuestions = 0;
    private int score;
    public List<QuestionsAndAnswers> QnA;
    private int currentQuestion;
    private bool interactive = true;
    public GameObject Cover;

    private void Start()
    {
        objectRenderers = objectsToHighlight.Select(obj => obj.GetComponent<Renderer>()).ToArray();
	originalColours = objectRenderers.Select(renderer => renderer.sharedMaterials.Select(material => material.color).ToArray()).ToArray();
        startColor = options[0].GetComponent<Image>().color;
	GOPanel.SetActive(false);
	totalQuestions = QnA.Count + objectsToHighlight.Count;

	if (interactive)
	{
	    HighlightRandomObject();
	}
	else
	{
	    generateQuestions();
	}
    }

    private void HighlightRandomObject()
    {
	for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = startColor;
        }
        if (currentHighlightedObject != null)
        {
            StopFlashing(currentHighlightedObject);
        }

	List<GameObject> availableObjects = objectsToHighlight.Except(askedObjects).ToList();

	if (availableObjects.Count == 0)
        {
            Debug.LogWarning("No objects to highlight.");
            GOPanel.SetActive(true);
            ScoreText.text = score + "/" + totalQuestions;
        }
	else
	{
	    GameObject randomObject = availableObjects[Random.Range(0, availableObjects.Count)];
            HighlightObject(randomObject);
            UpdateQuestionDisplay(randomObject);
	} 
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

	Color[] initialColours = new Color[renderer.materials.Length];
	Color[] colourDifferences = new Color[renderer.materials.Length];

	for (int i = 0; i < renderer.materials.Length; i++)
	{
            initialColours[i] = renderer.materials[i].color;
            colourDifferences[i] = highlightColour - initialColours[i];
	}

        float time = 0f;

	if (strength < 0)
	{
	    strength = 0.002f;
	}

        while (true)
    	{
            float t = Mathf.Sin((2 * Mathf.PI * time / cycleDuration) - (Mathf.PI / 2)) * strength + strength;

	    for (int i = 0; i < renderer.materials.Length; i++)
            {
        	Color interpolatedColour = initialColours[i] + colourDifferences[i] * t;
        	renderer.materials[i].color = interpolatedColour;
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
	    Renderer renderer = objectRenderers[index];
	    for (int i = 0; i < renderer.materials.Length; i++)
            {
        	renderer.materials[i].color = originalColours[index][i];
            }
        }
    }

    private void UpdateQuestionDisplay(GameObject highlightedObject)
    {
	if (modelName != null && modelName != "")
	{
            questionText.text = "What part of the " + modelName + " is currently highlighted?";
	}
	else
	{
	    questionText.text = "What part of the model is currently highlighted?";
	}
        List<GameObject> answerOptions = GetAnswerOptions(highlightedObject);

        // Set the answer options text for the buttons
	for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = answerOptions[i].name;
        }
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
    public void AnswerSelected(Button selectedButton)
    {
	if (!interactive)
	{
	    Answers answers = selectedButton.GetComponent<Answers>();
        
            if (answers != null)
            {
		Cover.SetActive(true);
        	answers.Answer();
            }
            else
            {
        	Debug.LogError("The specified component is not attached to the button.");
            }
	}
	else
	{
            string selectedOption = selectedButton.GetComponentInChildren<TextMeshProUGUI>().text;
	    Cover.SetActive(true);
	    askedObjects.Add(currentHighlightedObject);
            if (selectedOption == currentHighlightedObject.name)
            {
		score += 1;
        	selectedButton.GetComponent<Image>().color = Color.green;
        	Debug.Log("Correct answer!");
        	StartCoroutine(waitForNext());
            }
            else
            {
        	selectedButton.GetComponent<Image>().color = Color.red;
        	Debug.Log("Incorrect answer. Try again.");
		StartCoroutine(waitForNext());
            }
	}
    }

    IEnumerator waitForNext()
    {
	interactive = !interactive;
        yield return new WaitForSeconds(1);
	Cover.SetActive(false);
        if (interactive)
	{
	    HighlightRandomObject();
	}
	else
	{
	    if (currentHighlightedObject != null)
            {
        	StopFlashing(currentHighlightedObject);
            }
	    generateQuestions();
	}
    }

    public void correct()
    {
        // Check if the list contains elements before attempting to remove
        if (QnA.Count > 0 && currentQuestion < QnA.Count)
        {
            score += 1;
            QnA.RemoveAt(currentQuestion);
            StartCoroutine(waitForNext());
        }
        else
        {
            Debug.LogWarning("No more questions to remove or currentQuestion index is out of bounds.");
            // Optionally handle this situation (e.g., end the quiz or display a message)
        }
    }

    public void wrong()
    {
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }

    public void GameOver()
    {
        GOPanel.SetActive(true);
        ScoreText.text = score + "/" + totalQuestions;
    }

    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Image>().color = startColor;
            TextMeshProUGUI optionText = options[i].GetComponentInChildren<TextMeshProUGUI>();

            if (optionText != null)
            {
                optionText.text = QnA[currentQuestion].Answers[i];

                options[i].GetComponent<Answers>().isCorrect = (QnA[currentQuestion].CorrectAnswer == i + 1);
            }
            else
            {
                Debug.LogError("Text component not found on option " + i + " or its children.");
            }
        }
    }

    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            // Check if QuestionTxt is not null before accessing its properties
            if (questionText != null)
            {
                questionText.text = QnA[currentQuestion].Question;
                SetAnswer();
            }
            else
            {
                Debug.LogError("QuestionTxt is null. Make sure to assign the Text component in the inspector.");
            }
        }
        else
        {
            Debug.Log("Out of Questions");
            GameOver();
        }
    }
}