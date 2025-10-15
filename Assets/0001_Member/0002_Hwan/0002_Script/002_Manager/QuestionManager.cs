using UnityEngine;

public enum QuestionType
{
    None,
    Que_1,
    Que_2,
    Que_3,
    Que_4
}

public class QuestionManager : MonoSingleton<QuestionManager>
{
    private bool opened = false;

    public void OpenQuestion(QuestionType questionType)
    {
        if (opened == true) return;
        opened = false;

        Debug.Log(questionType);
        Time.timeScale = 0;
    }
}
