using TMPro;
using UnityEngine;

public class GameManager : MonoSingleton<GameManager>
{
    [SerializeField] private PasswordSO passwordSO;
    [SerializeField] private TextMeshProUGUI tmp;

    private int count = 0;
    public int iniCount = 0;
    protected override void Awake()
    {
        passwordSO.passWord = Random.Range(1000, 10000);
    }

    void Start()
    {
        SoundManager.Instance.Play(BGMSoundType.GameScene);
        tmp.text = "비밀번호 : ????"; // 시작 시 초기화
    }

    public void SetPassword()
    {
        if (count >= 4) return;
        count++;

        string pw = passwordSO.passWord.ToString();
        string visiblePart = pw.Substring(0, count);
        string hiddenPart = new string('?', 4 - count);

        // "비밀번호 : " 문구 포함
        tmp.text = "비밀번호 : " + visiblePart + hiddenPart;
    }
}
