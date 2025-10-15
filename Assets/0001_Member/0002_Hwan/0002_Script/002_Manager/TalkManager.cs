using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TalkManager : MonoSingleton<TalkManager>
{
    [SerializeField] private float typingSpeed = 0.05f; 
    [SerializeField] private GameObject textBar;
    [SerializeField] private Sprite[] sprites;
    [SerializeField] private Image portraitImage; 

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
            StopCoroutine(typingCoroutine);

        typingCoroutine = StartCoroutine(TypeText(message));
    }

    private IEnumerator TypeText(string message)
    {
        int spriteIndex = -1;
        string trimmedMsg = message;

        if (message.Length > 2 && message[^2] == ' ' && char.IsDigit(message[^1]))
        {
            spriteIndex = int.Parse(message[^1].ToString());
            trimmedMsg = message[..^2];
        }

        if (spriteIndex >= 0 && spriteIndex < sprites.Length)
        {
            portraitImage.sprite = sprites[spriteIndex];
            portraitImage.enabled = true;
        }
        else
        {
            portraitImage.enabled = false; 
        }

        textMeshPro.text = "";

        foreach (char c in trimmedMsg)
        {
            textMeshPro.text += c;
            SoundManager.Instance.Play(SFXSoundType.Type);
            yield return new WaitForSecondsRealtime(typingSpeed);
        }

        yield return new WaitForSecondsRealtime(1.5f);

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
