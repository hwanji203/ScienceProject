using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Y_Sorting : MonoBehaviour
{
    private SpriteRenderer _spriteRen;

    void Awake()
    {
        _spriteRen = GetComponent<SpriteRenderer>();

        if (_spriteRen.sortingLayerName != "Default")
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        // ��������Ʈ�� �߽� y��ǥ�� �������� ����
        float centerY = _spriteRen.bounds.center.y;
        _spriteRen.sortingOrder = Mathf.RoundToInt(100 * -centerY);
    }
}
