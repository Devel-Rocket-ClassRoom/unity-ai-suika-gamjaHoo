using UnityEngine;

[CreateAssetMenu(fileName = "FruitTable", menuName = "Suika/FruitTable")]
public class FruitTable : ScriptableObject
{
    public FruitData[] fruits;

    [Tooltip("투하 가능한 최대 단계 인덱스 (0-based)")]
    public int maxDropStage = 4;

    public FruitData Get(int stage)
    {
        if (stage < 0 || stage >= fruits.Length)
            return null;
        return fruits[stage];
    }

    public int MaxStage => fruits.Length - 1;
}
