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

        wordDictionary.Add(Word.StartWord, "���� ���� 8���� ���� ���� ��, ���� ������ �̰��̾�. ����ǥ�� �ٰ��� eŰ�� ���� ������ Ǯ��� �� �� ����. 1");
        wordDictionary.Add(Word.Locked, "��� �� ����.. �̷� �� �˾����� ���� ���� ������ �ϴ� �ǵ�... 1");
        wordDictionary.Add(Word.GravityDir, "��й�ȣ�� �ʿ��� �� ����! 3");
        wordDictionary.Add(Word.Yeppy, "������ Ǯ����! ���ڱ� ��й�ȣ�� �ϳ� ���ö���! 2");
        wordDictionary.Add(Word.Clear1, "���� ���� �� �� �ִ�!\n\r ���� ���н��� 100���� ���� ���ڴ°�? 4");
        wordDictionary.Add(Word.Wrong, "??? 5");
        wordDictionary.Add(Word.PassWrong, "�� Ʋ������? 6");
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
            Debug.LogWarning($"'{wordType}'�� �ش��ϴ� ��簡 �����ϴ�.");
        }
    }
}
