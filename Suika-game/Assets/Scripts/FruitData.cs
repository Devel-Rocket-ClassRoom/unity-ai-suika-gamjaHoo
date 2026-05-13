using UnityEngine;

[CreateAssetMenu(fileName = "FruitData", menuName = "Suika/FruitData")]
public class FruitData : ScriptableObject
{
    public int stage;
    public float radius = 0.5f;
    public int mergeScore;
    public Sprite sprite;
    public GameObject prefab;
}
