using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    public static FruitSpawner Instance { get; private set; }

    [SerializeField]
    FruitTable fruitTable;

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

        var fruit = go.GetComponent<Fruit>();
        fruit.stage = stage;
        fruit.IsPreview = isPreview;

        var rb = go.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.isKinematic = isPreview;

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
