using UnityEngine;

public class GameManager : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.Play(BGMSoundType.GameScene);
    }
}
