using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    public Vector2 moveDir { get; private set; }
    public event Action<Vector2> OnVelocityChange;

    private Rigidbody2D rb;

    private Vector2 beforeVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.linearVelocity = moveDir * speed;
    }

    public void OnMove(InputValue value)
    {
        moveDir = value.Get<Vector2>();
        if (beforeVector != null)
        {
            if (moveDir.x % 1 != 0 || moveDir.y % 1 != 0)
            {
                moveDir = Mathf.Abs(beforeVector.x) > 0 ? new Vector2(0, moveDir.y) : new Vector2(moveDir.x, 0);
            }
        }
        beforeVector = moveDir;
        OnVelocityChange?.Invoke(moveDir);
        FaceDirection(moveDir);
    }
    public void FaceDirection(Vector2 moveDir)
    {
        if (moveDir.x != 0) transform.eulerAngles = moveDir.x > 0 ? Vector3.zero : new Vector3(0, 180f, 0);
    }
}