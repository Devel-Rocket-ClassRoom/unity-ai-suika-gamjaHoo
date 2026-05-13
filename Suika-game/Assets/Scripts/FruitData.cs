using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Suika/FruitData")]
public class FruitData : ScriptableObject
{
    public int stage;
    public float radius = 0.5f;
    public int mergeScore;

    [Range(1, 100)]
    [Tooltip("투하 확률 가중치 (높을수록 자주 등장)")]
    public int dropWeight = 10;

    public Sprite sprite;
    public GameObject prefab;
}
