using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour, IChangeable
{
    [SerializeField] private float speed = 3f;
    public Vector2 moveDir { get; private set; }
    public event Action<Vector2> OnVelocityChange;

    private Rigidbody2D rb;

    private Vector2 currentDirection = Vector2.zero;
    private List<Key> pressedKeys = new List<Key>();


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // 키 입력 순서를 기억할 리스트 (마지막에 누른 키가 뒤에 있음)

    void Update()
    {
        OnVelocityChange?.Invoke(Time.timeScale != 0 ? currentDirection : Vector2.zero);

        HandleKey(Key.W, Vector2.up);
        HandleKey(Key.A, Vector2.left);
        HandleKey(Key.S, Vector2.down);
        HandleKey(Key.D, Vector2.right);

        // 모든 키가 다 떼어진 경우
        if (pressedKeys.Count == 0)
        {
            currentDirection = Vector2.zero;
        }
    }

    private void HandleKey(Key key, Vector2 dir)
    {
        var k = Keyboard.current[key];

        // 키 눌렀을 때 → 리스트에 추가 & 방향 갱신
        if (k.wasPressedThisFrame)
        {
            pressedKeys.Remove(key);   // 중복 방지
            pressedKeys.Add(key);      // 최신 입력은 항상 맨 뒤
            currentDirection = dir;
        }

        // 키 뗐을 때 → 리스트에서 제거
        if (k.wasReleasedThisFrame)
        {
            pressedKeys.Remove(key);

            // 아직 남아 있는 키가 있으면 마지막 키로 방향 갱신
            if (pressedKeys.Count > 0)
            {
                Key lastKey = pressedKeys[pressedKeys.Count - 1];
                currentDirection = KeyToDirection(lastKey);
            }
        }
    }

    private Vector2 KeyToDirection(Key key)
    {
        return key switch
        {
            Key.W => Vector2.up,
            Key.A => Vector2.left,
            Key.S => Vector2.down,
            Key.D => Vector2.right,
            _ => Vector2.zero
        };
    }


    private void FixedUpdate()
    {
        Move();
        FaceDirection(currentDirection);
    }

    private void Move()
    {
        rb.linearVelocity = currentDirection * speed * 10;
    }

    public void FaceDirection(Vector2 moveDir)
    {
        if (moveDir.x != 0) transform.eulerAngles = moveDir.x > 0 ? Vector3.zero : new Vector3(0, 180f, 0);
    }

    public void OnChange()
    {
        speed = PlayerPrefs.GetFloat(SliderType.Speeeed.ToString());
    }
}