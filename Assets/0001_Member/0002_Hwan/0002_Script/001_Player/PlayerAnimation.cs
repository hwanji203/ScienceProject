using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator AnimCompo { get; private set; }
    private readonly int isWalkHash = Animator.StringToHash("IsMove");
    private readonly int yVelocity = Animator.StringToHash("YVelocity");
    private readonly int xVelocity = Animator.StringToHash("XVelocity");

    private void Awake()
    {
        AnimCompo = GetComponent<Animator>();
    }

    private void SetWalkAnimation(bool value)
    {
        AnimCompo.SetBool(isWalkHash, value);
    }

    public void SetVelocityAnimation(Vector2 dir)
    {
        if (dir.magnitude > 0.5f)
        {
            AnimCompo.SetFloat(xVelocity, dir.x);
            AnimCompo.SetFloat(yVelocity, dir.y);
            SetWalkAnimation(true);
        }
        else
        {
            SetWalkAnimation(false);
        }
    }
}
