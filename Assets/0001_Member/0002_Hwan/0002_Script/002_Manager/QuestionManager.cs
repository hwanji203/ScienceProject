using UnityEngine;

public class QuestionManager : MonoSingleton<QuestionManager>
{
    private bool opened = false;

    public void OpenQuestion(GameObject obj)
    {
        if (opened == true) return;
        opened = false;

        obj.SetActive(true);
        Time.timeScale = 0;
    }
}
