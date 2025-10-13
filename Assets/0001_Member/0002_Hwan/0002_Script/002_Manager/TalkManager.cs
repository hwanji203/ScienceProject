using System.Collections;
using TMPro;
using UnityEngine;

public class TalkManager : MonoSingleton<TalkManager>
{
    [SerializeField] private float typingSpeed = 0.05f; // 글자당 출력 속도
    [SerializeField] private GameObject textBar;
    private TextMeshProUGUI textMeshPro;
    private Coroutine typingCoroutine;

    protected override void Awake()
    {
        base.Awake();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();

        textBar.SetActive(false);
    }

    public void StartTyping(string message)
    {
        textBar.SetActive(true);
        Time.timeScale = 0;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }
        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        textMeshPro.text = "";

        foreach (char c in message)
        {
            textMeshPro.text += c;
            SoundManager.Instance.Play(SFXSoundType.Type);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(2.5f);

        typingCoroutine = null;

        textBar.SetActive(false);
        Time.timeScale = 1;
    }


    public void CompleteTyping(string message)
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
        textMeshPro.text = message;
    }
}

