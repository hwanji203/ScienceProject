using UnityEngine;

[CreateAssetMenu(fileName = "QuizContent", menuName = "SO/QuizContent")]
public class QuizContent : ScriptableObject
{
    [SerializeField, TextArea] public string content;
}
