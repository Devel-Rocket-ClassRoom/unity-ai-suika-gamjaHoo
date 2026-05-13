using UnityEngine;

public class Fruit : MonoBehaviour
{
    public int stage;

    public bool IsMerging { get; set; }

    // 투하 전 프리뷰 상태 — 충돌 이벤트 무시
    public bool IsPreview { get; set; }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (IsPreview || IsMerging)
            return;

        var other = col.gameObject.GetComponent<Fruit>();
        if (other == null || other.IsPreview || other.IsMerging)
            return;

        if (other.stage != stage)
            return;

        FruitSpawner.Instance.Merge(this, other);
    }
}
