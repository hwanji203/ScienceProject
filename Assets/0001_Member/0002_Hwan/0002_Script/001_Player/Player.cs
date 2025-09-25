using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerMovement MoveCompo { get; private set; }
    public PlayerAnimation AnimCompo { get; private set; } 

    private void Awake()
    {
        AnimCompo = GetComponentInChildren<PlayerAnimation>();
        MoveCompo = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        MoveCompo.OnVelocityChange += AnimCompo.SetVelocityAnimation;
    }
}
