using UnityEngine;

public class Potal : MonoBehaviour
{
    [SerializeField] private Transform movePos;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.position = movePos.position;
    }
}
