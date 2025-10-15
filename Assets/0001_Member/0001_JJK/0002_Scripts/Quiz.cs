using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI feedbackText;
    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TextMeshProUGUI _content;
    [SerializeField] private QuizContent contentData;
    [SerializeField] private TextMeshProUGUI realText;

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
        if (Input.GetKeyDown(KeyCode.Return) && !string.IsNullOrEmpty(RemoveUTags(realText.text)))
        {
            CheckAnswer(RemoveUTags(realText.text));
        }
    }

    private void CheckAnswer(string playerInput)
    {
        StartCoroutine(DelayCheck(playerInput));
    }
    static string DumpCodePoints(string s)
    {
        if (string.IsNullOrEmpty(s)) return "(empty)";
        var sb = new StringBuilder();
        foreach (var ch in s)
            sb.Append($"U+{(int)ch:X4} ");
        return sb.ToString();
    }
    static string CleanNormalize(string s)
    {
        if (s == null) return string.Empty;

        // 흔히 섞여 들어오는 보이지 않는 문자 제거 (ZW* / BOM)
        s = Regex.Replace(s, "[\u200B-\u200D\uFEFF]", "");

        // NBSP(비분리 공백) → 일반 공백
        s = s.Replace('\u00A0', ' ');

        // 원하시면 특정 태그 제거 (예: <u>…</u>)
        s = Regex.Replace(s, "<\\/?u>", "");

        // 앞뒤 공백 제거
        s = s.Trim();

        // 호환 정규화(FormKC): 전각/반각, 합성/분해 등을 통일
        // (FormC로 충분하면 FormC 사용)
        s = s.Normalize(NormalizationForm.FormKC);

        return s;
    }
    private IEnumerator DelayCheck(string playerInput)
    {
        yield return null;

        string a = CleanNormalize(playerInput);
        string b = CleanNormalize(correctAnswer);

        if (string.Equals(a, b, StringComparison.Ordinal))
        {
            Debug.Log("정답입니다!");
        }
        else
        {
            Debug.Log("오답입니다.");
            Debug.Log($"[A] \"{a}\"  (len={a.Length})");
            Debug.Log($"[B] \"{b}\"  (len={b.Length})");
            Debug.Log($"[A codes] {DumpCodePoints(a)}");
            Debug.Log($"[B codes] {DumpCodePoints(b)}");
        }


        answerInput.text = "";
        answerInput.ActivateInputField();
    }
    public static string RemoveUTags(string input)
    {
        return Regex.Replace(input, "<\\/?u>", "");
    }

    public void Quit()
    {
        gameObject.SetActive(false);
    }
}
