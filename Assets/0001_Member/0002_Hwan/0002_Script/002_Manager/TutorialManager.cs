using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    private TalkManager talkManager;

    public enum Word
    {
        StartWord
    }

    private Dictionary<Word, string> wordDictionary = new Dictionary<Word, string>();

    private void Awake()
    {
        talkManager = TalkManager.Instance;

        wordDictionary.Add(Word.StartWord, "���� ���� 8���� ���� ���� ��, ���� ������ �̰��̴�. � ���� ���� ���̴����� �ϳ��� �𸣰����� ����ǥ�� �ٰ��� ������ Ǯ��� �� �� ����.");
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
