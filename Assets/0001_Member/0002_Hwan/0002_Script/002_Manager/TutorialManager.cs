using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum Word
{
    StartWord,
    Locked,
    GravityDir,
    Yeppy,
    Clear
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    private TalkManager talkManager;

    private Trigger[] triggers;

    private Dictionary<Word, string> wordDictionary = new Dictionary<Word, string>();

    protected override void Awake()
    {
        talkManager = TalkManager.Instance;
        triggers = GetComponentsInChildren<Trigger>();

        wordDictionary.Add(Word.StartWord, "���� ���� 8���� ���� ���� ��, ���� ������ �̰��̴�. ����ǥ�� �ٰ��� ������ Ǯ��� �� �� ����. 1");
        wordDictionary.Add(Word.Locked, "��� �� ����. 1");
        wordDictionary.Add(Word.GravityDir, "���ڰ� �ʿ��� ���δ�. 3");
        wordDictionary.Add(Word.Yeppy, "������ Ǯ����! 2");
        wordDictionary.Add(Word.Clear, "���� ���� �� �� �־�! 4");
    }

    private void Start()
    {
        Talk(Word.StartWord);
    }

    public void Talk(Word wordType)
    {
        if (wordDictionary.TryGetValue(wordType, out string message))
        {
            talkManager.StartTyping(message);
        }
        else
        {
            Debug.LogWarning($"'{wordType}'�� �ش��ϴ� ��簡 �����ϴ�.");
        }
    }
}
