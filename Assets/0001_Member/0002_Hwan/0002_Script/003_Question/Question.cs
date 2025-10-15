using UnityEngine;
using UnityEngine.InputSystem;

public class Question : MonoBehaviour
{
    [SerializeField] private GameObject myQuiz;

    private bool playerIn;

    private void Awake()
    {
        myQuiz.SetActive(false);
    }

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && playerIn == true && Time.timeScale != 0)
        {
            SoundManager.Instance.Play(SFXSoundType.OpenBook);
            QuestionManager.Instance.OpenQuestion(myQuiz);
            Destroy(gameObject);
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
