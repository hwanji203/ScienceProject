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
        // 스프라이트의 중심 y좌표를 기준으로 정렬
        float centerY = _spriteRen.bounds.center.y;
        _spriteRen.sortingOrder = Mathf.RoundToInt(100 * -centerY);
    }
}
