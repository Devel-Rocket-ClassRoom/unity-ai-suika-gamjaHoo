using UnityEngine;

public class MergeParticle : MonoBehaviour
{
    public static MergeParticle Instance { get; private set; }

    [SerializeField]
    Material particleMaterial;

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
        var go = new GameObject("MergeBurst");
        go.transform.position = position;
        var ps = go.AddComponent<ParticleSystem>();

        var main = ps.main;
        main.duration = 0.01f;
        main.loop = false;
        main.startLifetime = new ParticleSystem.MinMaxCurve(0.3f, 0.5f + stage * 0.05f);
        main.startSpeed = new ParticleSystem.MinMaxCurve(1f, 2.5f + stage * 0.3f);
        main.startSize = new ParticleSystem.MinMaxCurve(0.06f, 0.12f + stage * 0.01f);
        main.startColor = new ParticleSystem.MinMaxGradient(Color.yellow, Color.white);
        main.gravityModifier = -0.3f;
        main.stopAction = ParticleSystemStopAction.Destroy;

        var emission = ps.emission;
        emission.rateOverTime = 0;
        emission.SetBursts(new[] { new ParticleSystem.Burst(0f, (short)(8 + stage * 2)) });

        var shape = ps.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 0.1f;

        var col = ps.colorOverLifetime;
        col.enabled = true;
        var gradient = new Gradient();
        gradient.SetKeys(
            new[] { new GradientColorKey(Color.yellow, 0f), new GradientColorKey(Color.white, 1f) },
            new[] { new GradientAlphaKey(1f, 0f), new GradientAlphaKey(0f, 1f) }
        );
        col.color = gradient;

        if (particleMaterial != null)
            ps.GetComponent<ParticleSystemRenderer>().material = particleMaterial;

        ps.Play();
    }
}
