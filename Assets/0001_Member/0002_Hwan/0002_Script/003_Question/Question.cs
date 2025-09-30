using UnityEngine;
using UnityEngine.InputSystem;

public class Question : MonoBehaviour
{
    [SerializeField] private QuestionType myType;

    private bool playerIn;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIn == true)
        {
            QuestionManager.Instance.OpenQuestion(myType);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIn = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIn = false;
    }
}
