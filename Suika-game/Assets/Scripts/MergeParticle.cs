using UnityEngine;

public class MergeParticle : MonoBehaviour
{
    public static MergeParticle Instance { get; private set; }

    [SerializeField]
    ParticleSystem mergeBurstPrefab;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void Play(Vector2 position, int stage)
    {
        if (mergeBurstPrefab == null)
            return;

        var ps = Instantiate(mergeBurstPrefab, position, Quaternion.identity);
        ps.transform.localScale = Vector3.one * (1f + stage * 0.12f);
        ps.Play();
    }
}
