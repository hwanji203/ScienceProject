using UnityEngine;

public enum QuestionType
{
    None
}

public class QuestionManager : MonoSingleton<QuestionManager>
{
    private bool opened = false;

    public void OpenQuestion(QuestionType questionType)
    {
        if (opened == true) return;
        opened = false;

        switch (questionType)
        {
            case QuestionType.None:
                break;
        }
        Time.timeScale = 0;
    }
}
