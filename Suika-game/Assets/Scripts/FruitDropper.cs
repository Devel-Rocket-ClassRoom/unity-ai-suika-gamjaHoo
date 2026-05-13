using UnityEngine;

public class FruitDropper : MonoBehaviour
{
    [SerializeField]
    FruitTable fruitTable;

    [SerializeField]
    float dropCooldown = 0.5f;

    [SerializeField]
    float minX = -2.3f;

    [SerializeField]
    float maxX = 2.3f;

    GameObject _preview;
    float _cooldownTimer;
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        SpawnPreview();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        _cooldownTimer -= Time.deltaTime;
        MovePreview();

        if (Input.GetMouseButtonDown(0) && _cooldownTimer <= 0f)
            Drop();
    }

    void MovePreview()
    {
        if (_preview == null)
            return;

        var mouseWorld = _cam.ScreenToWorldPoint(Input.mousePosition);
        float x = Mathf.Clamp(mouseWorld.x, minX, maxX);
        _preview.transform.position = new Vector3(x, transform.position.y, 0f);
    }

    void Drop()
    {
        if (_preview == null)
            return;

        var fruit = _preview.GetComponent<Fruit>();
        if (fruit != null)
            fruit.IsPreview = false;

        var rb = _preview.GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.isKinematic = false;

        _preview = null;
        _cooldownTimer = dropCooldown;
        SpawnPreview();
    }

    void SpawnPreview()
    {
        int stage = Random.Range(0, fruitTable.maxDropStage + 1);
        _preview = FruitSpawner.Instance.Spawn(stage, transform.position, isPreview: true);
    }
}
