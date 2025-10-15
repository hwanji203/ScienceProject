using System;
using System.Collections;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private QuizContent contentData;
    
    [Header("Quiz Data")]
    [SerializeField] private string correctAnswer;
    [SerializeField] private int passwordDigit = 0;

    private string playerInput;

    private void Start()
    {
        answerInput.onValueChanged.AddListener(UpdateAnswer);
        _content.text = contentData.content;
    }

    private void UpdateAnswer(string arg0)
    {
        playerInput = arg0;
        Debug.Log("update");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CheckAnswer(playerInput);
        }
    }

    private void CheckAnswer(string playerInput)
    {
        StartCoroutine(DelayCheck(playerInput));
    }
    
    private IEnumerator DelayCheck(string playerInput)
    {
        yield return null;
        
        playerInput = playerInput.Trim().Normalize(NormalizationForm.FormC);
        string normalizedAnswer = correctAnswer.Trim().Normalize(NormalizationForm.FormC);

        if (normalizedAnswer == correctAnswer)
        {
            Debug.Log("정답입니다!");
            //ShowHint();
        }
        else
        {
            Debug.Log("오답입니다.");
            Debug.Log(playerInput);
            Debug.Log(normalizedAnswer);
        }

        // 입력창 초기화
        answerInput.text = "";
        answerInput.ActivateInputField();
    }

    public void Quit()
    {
        gameObject.SetActive(false);
    }
}
