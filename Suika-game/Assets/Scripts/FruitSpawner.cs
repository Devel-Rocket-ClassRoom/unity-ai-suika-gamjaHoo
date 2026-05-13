using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance { get; private set; }

    [SerializeField]
    FruitTable fruitTable;

    [SerializeField]
    float sizeScale = 0.5f;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public GameObject Spawn(int stage, Vector2 position, bool isPreview = false)
    {
        var data = fruitTable.Get(stage);
        if (data?.prefab == null)
            return null;

        var go = Instantiate(data.prefab, position, Quaternion.identity);

        // 스프라이트 실제 bounds 기준으로 콜라이더 맞춤
        go.transform.localScale = Vector3.one * data.radius * 2f * sizeScale;
        var sr = go.GetComponent<SpriteRenderer>();
        var col = go.GetComponent<CircleCollider2D>();
        if (col != null)
        {
            float spriteRadius = sr?.sprite != null ? sr.sprite.bounds.extents.x : 0.5f;
            col.radius = spriteRadius;
        }

        var fruit = go.GetComponent<Fruit>();
        fruit.stage = stage;
        fruit.IsPreview = isPreview;

        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.isKinematic = isPreview;
            // 프리뷰는 완전 고정, 드롭 후에는 자유 회전 허용
            rb.constraints = isPreview
                ? RigidbodyConstraints2D.FreezeAll
                : RigidbodyConstraints2D.None;
        }

        return go;
    }

    public void Merge(Fruit a, Fruit b)
    {
        if (a.IsMerging || b.IsMerging)
            return;

        a.IsMerging = true;
        b.IsMerging = true;

        int stage = a.stage;
        Vector2 mid = ((Vector2)a.transform.position + (Vector2)b.transform.position) / 2f;

        var data = fruitTable.Get(stage);
        if (data != null)
            GameManager.Instance.AddScore(data.mergeScore);

        Destroy(a.gameObject);
        Destroy(b.gameObject);

        if (stage < fruitTable.MaxStage)
            Spawn(stage + 1, mid);
    }
}
