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

        wordDictionary.Add(Word.StartWord, "과학 시험 8점을 맞은 다음 날, 눈을 떠보니 이곳이다. 느낌표에 다가가 문제를 풀어야 할 것 같다. 1");
        wordDictionary.Add(Word.Locked, "잠긴 것 같다. 1");
        wordDictionary.Add(Word.GravityDir, "숫자가 필요해 보인다. 3");
        wordDictionary.Add(Word.Yeppy, "문제를 풀었다! 2");
        wordDictionary.Add(Word.Clear, "드디어 집에 갈 수 있어! 4");
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
            Debug.LogWarning($"'{wordType}'에 해당하는 대사가 없습니다.");
        }
    }
}
