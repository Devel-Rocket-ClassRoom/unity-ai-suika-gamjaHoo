using UnityEngine;
using UnityEngine.UI;

public class NextFruitPreview : MonoBehaviour
{
    [SerializeField]
    Image previewImage;

    [SerializeField]
    FruitDropper fruitDropper;

    void OnEnable()
    {
        fruitDropper.OnNextFruitChanged += Refresh;
    }

    void OnDisable()
    {
        if (fruitDropper != null)
            fruitDropper.OnNextFruitChanged -= Refresh;
    }

    void Refresh(Sprite sprite)
    {
        previewImage.sprite = sprite;
        previewImage.enabled = sprite != null;
    }
}
