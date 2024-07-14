using UnityEngine;
using TMPro;

public class QuizRandomHighlight : MonoBehaviour
{
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI[] optionTexts; // Assuming you have four multiple choice options
    public GameObject quizPanel; // Reference to the panel containing the quiz UI elements

    private GameObject highlightedObject;

    // Start is called before the first frame update
    void Start()
    {
        // Disable the quiz panel at the start
        //quizPanel.SetActive(false);
    }

    // Method to start the quiz with a highlighted object
    public void StartQuiz(GameObject highlightedObj)
    {
        highlightedObject = highlightedObj;

        // Generate and display the quiz question based on the highlighted object
        GenerateQuestion();
    }

    // Method to generate and display the quiz question
    private void GenerateQuestion()
    {
        // Set the question text based on the highlighted object
        questionText.text = "Which part of the cell is currently highlighted?";

        // Generate and set the multiple choice options (you can replace these with actual options)
        string[] options = GetRandomOptions();
        for (int i = 0; i < optionTexts.Length; i++)
        {
            optionTexts[i].text = options[i];
        }

        // Enable the quiz panel to display the question and options
        quizPanel.SetActive(true);
    }

    // Method to check the user's answer
    public void CheckAnswer(string selectedOption)
    {
        // Get the name of the highlighted object
        string correctAnswer = highlightedObject.name;

        // Compare the selected option with the correct answer
        if (selectedOption == correctAnswer)
        {
            Debug.Log("Correct answer!");
        }
        else
        {
            Debug.Log("Incorrect answer. Try again.");
        }

        // Disable the quiz panel after checking the answer
       // quizPanel.SetActive(false);
    }

    // Method to generate random multiple choice options
    private string[] GetRandomOptions()
    {
        // Example options
        string[] options = { "Option A", "Option B", "Option C", "Option D" };
        return options;
    }
}
