using System;
using UnityEngine;
using UnityEngine.InputSystem;

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

    public event Action<Sprite> OnNextFruitChanged;

    GameObject _preview;
    int _nextStage;
    float _cooldownTimer;
    Camera _cam;

    void Start()
    {
        _cam = Camera.main;
        _nextStage = fruitTable.GetWeightedRandomDropStage();
        SpawnPreview();
    }

    void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        _cooldownTimer -= Time.deltaTime;
        MovePreview();

        if (
            Mouse.current != null
            && Mouse.current.leftButton.wasPressedThisFrame
            && _cooldownTimer <= 0f
        )
            Drop();
    }

    void MovePreview()
    {
        if (_preview == null || Mouse.current == null)
            return;

        var mouseWorld = _cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
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
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.isKinematic = false;
        }

        _preview = null;
        _cooldownTimer = dropCooldown;
        SpawnPreview();
    }

    void SpawnPreview()
    {
        int currentStage = _nextStage;
        _nextStage = fruitTable.GetWeightedRandomDropStage();
        _preview = FruitSpawner.Instance.Spawn(currentStage, transform.position, isPreview: true);
        OnNextFruitChanged?.Invoke(fruitTable.Get(_nextStage)?.sprite);
    }
}
