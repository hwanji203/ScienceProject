using UnityEngine;

public enum QuestionType
{
    None
}

public class QuestionManager : MonoBehaviour
{
    public void OpenQuestion(QuestionType questionType)
    {
        switch (questionType)
        {
            case QuestionType.None:
                // �´� UI ����
                // �߸�
                break;
        }
    }
}
