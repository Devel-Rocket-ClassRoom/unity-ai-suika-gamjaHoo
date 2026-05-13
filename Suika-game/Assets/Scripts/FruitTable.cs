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

    public int GetWeightedRandomDropStage()
    {
        int total = 0;
        for (int i = 0; i <= maxDropStage && i < fruits.Length; i++)
            total += fruits[i].dropWeight;

        int roll = UnityEngine.Random.Range(0, total);
        for (int i = 0; i <= maxDropStage && i < fruits.Length; i++)
        {
            roll -= fruits[i].dropWeight;
            if (roll < 0)
                return i;
        }
        return Mathf.Min(maxDropStage, fruits.Length - 1);
    }
}
