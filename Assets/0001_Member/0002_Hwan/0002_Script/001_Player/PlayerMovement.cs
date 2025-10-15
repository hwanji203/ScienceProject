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

    // Ű �Է� ������ ����� ����Ʈ (�������� ���� Ű�� �ڿ� ����)

    void Update()
    {
        OnVelocityChange?.Invoke(Time.timeScale != 0 ? currentDirection : Vector2.zero);

        HandleKey(Key.W, Vector2.up);
        HandleKey(Key.A, Vector2.left);
        HandleKey(Key.S, Vector2.down);
        HandleKey(Key.D, Vector2.right);

        // ��� Ű�� �� ������ ���
        if (pressedKeys.Count == 0)
        {
            currentDirection = Vector2.zero;
        }
    }

    private void HandleKey(Key key, Vector2 dir)
    {
        var k = Keyboard.current[key];

        // Ű ������ �� �� ����Ʈ�� �߰� & ���� ����
        if (k.wasPressedThisFrame)
        {
            pressedKeys.Remove(key);   // �ߺ� ����
            pressedKeys.Add(key);      // �ֽ� �Է��� �׻� �� ��
            currentDirection = dir;
        }

        // Ű ���� �� �� ����Ʈ���� ����
        if (k.wasReleasedThisFrame)
        {
            pressedKeys.Remove(key);

            // ���� ���� �ִ� Ű�� ������ ������ Ű�� ���� ����
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