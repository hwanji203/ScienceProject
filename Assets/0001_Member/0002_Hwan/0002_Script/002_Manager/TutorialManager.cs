using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
public enum Word
{
    StartWord,
    Locked,
    GravityDir,
    Yeppy,
    Clear1,
    Wrong,
    PassWrong
}

public class TutorialManager : MonoSingleton<TutorialManager>
{
    private TalkManager talkManager;

    private Dictionary<Word, string> wordDictionary = new Dictionary<Word, string>();

    protected override void Awake()
    {
        talkManager = TalkManager.Instance;

        wordDictionary.Add(Word.StartWord, "과학 시험 8점을 맞은 다음 날, 눈을 떠보니 이곳이야. 느낌표에 다가가 e키를 눌러 문제를 풀어야 할 것 같아. 1");
        wordDictionary.Add(Word.Locked, "잠긴 것 같아.. 이럴 줄 알았으면 과학 공부 열심히 하는 건데... 1");
        wordDictionary.Add(Word.GravityDir, "비밀번호가 필요한 것 같아! 3");
        wordDictionary.Add(Word.Yeppy, "문제를 풀었다! 갑자기 비밀번호가 하나 떠올랐어! 2");
        wordDictionary.Add(Word.Clear1, "드디어 집에 갈 수 있다!\n\r 이제 과학시험 100점도 문제 없겠는걸? 4");
        wordDictionary.Add(Word.Wrong, "??? 5");
        wordDictionary.Add(Word.PassWrong, "왜 틀린거지? 6");
    }

    private void Start()
    {
        Talk(Word.StartWord);
    }


    public void Talk(Word wordType)
    {
        switch (wordType)
        {
            case Word.StartWord:
                SoundManager.Instance.Play(SFXSoundType.Haa);
                break;
            case Word.Locked:
                SoundManager.Instance.Play(SFXSoundType.Haa);
                break;
            case Word.GravityDir:
                SoundManager.Instance.Play(SFXSoundType.Haa);
                break;
            case Word.Yeppy:
                SoundManager.Instance.Play(SFXSoundType.Yea);
                break;
            case Word.Clear1:
                SoundManager.Instance.Play(SFXSoundType.Giyaa);
                break;
            case Word.Wrong:
                SoundManager.Instance.Play(SFXSoundType.Why);
                break;
            case Word.PassWrong:
                SoundManager.Instance.Play(SFXSoundType.Why);
                break;
        }

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
