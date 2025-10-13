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

        wordDictionary.Add(Word.StartWord, "과학 시험 8점을 맞은 다음 날, 눈을 떠보니 이곳이다. 어떤 분이 끌고 오셨는지는 하나도 모르겠지만 느낌표에 다가가 문제를 풀어야 할 것 같다.");
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
