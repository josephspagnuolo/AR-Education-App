using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public GameObject QuizPanel;
    public GameObject GOPanel;
    public TextMeshProUGUI ScoreText;
    int totalQuestions = 0;
    public int score;
    //public Color startColor;

    //public GameObject questionTxtObj;
    //private Text QuestionTxt;
    public TextMeshProUGUI QuestionTxt;

    private void Start()
    {
      
        //startColor = GetComponent<Image>().color;
        
        totalQuestions = QnA.Count;
        GOPanel.SetActive(false);
        //QuestionTxt = questionTxtObj.GetComponent<Text>();
        generateQuestions();

    }
    /*
    public void correct()
    {
        QnA.RemoveAt(currentQuestion);
        generateQuestions();
        
    }
    */
    public void retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void GameOver()
    {
        QuizPanel.SetActive(false);
        GOPanel.SetActive(true);
        ScoreText.text = score + "/" + totalQuestions;
        
    }

    public void wrong()
    {
        
        QnA.RemoveAt(currentQuestion);
        StartCoroutine(waitForNext());
    }

    IEnumerator waitForNext()
    {
        yield return new WaitForSeconds(1);
        generateQuestions();
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
    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            // Assuming that the Text component is directly on the option GameObject
            options[i].GetComponent<Image>().color = options[i].GetComponent<Answers>().startColor;
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

    /*
    void SetAnswer()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answers>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<Text>().text = QnA[currentQuestion].Answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<Answers>().isCorrect = true;
            }

        }
    }
    */
    /*
    void generateQuestions()
    {
        currentQuestion = Random.Range(0, QnA.Count);

        QuestionTxt.text = QnA[currentQuestion].Question;
        SetAnswer();

        
    }
    */
    void generateQuestions()
    {
        if (QnA.Count > 0)
        {
            currentQuestion = Random.Range(0, QnA.Count);

            // Check if QuestionTxt is not null before accessing its properties
            if (QuestionTxt != null)
            {
                QuestionTxt.text = QnA[currentQuestion].Question;
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
