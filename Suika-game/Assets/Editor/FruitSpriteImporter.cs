using UnityEditor;
using UnityEngine;

public static class FruitSpriteImporter
{
    [MenuItem("Tools/Fix Fruit Sprite Import Settings")]
    public static void FixImportSettings()
    {
        var guids = AssetDatabase.FindAssets("t:Texture2D", new[] { "Assets/Sprites/Fruits" });
        int count = 0;

        foreach (var guid in guids)
        {
            var path = AssetDatabase.GUIDToAssetPath(guid);
            var importer = AssetImporter.GetAtPath(path) as TextureImporter;
            if (importer == null)
                continue;

            importer.textureType = TextureImporterType.Sprite;
            importer.spriteImportMode = SpriteImportMode.Single;
            importer.alphaIsTransparency = true;
            importer.mipmapEnabled = false;
            importer.filterMode = FilterMode.Bilinear;
            importer.spritePixelsPerUnit = 100;

            var settings = new TextureImporterSettings();
            importer.ReadTextureSettings(settings);
            settings.spriteMeshType = SpriteMeshType.Tight;
            settings.spriteExtrude = 1;
            importer.SetTextureSettings(settings);

            importer.SaveAndReimport();
            count++;
        }

        Debug.Log($"[FruitSpriteImporter] {count}개 스프라이트 임포트 설정 완료 (Tight mesh)");
    }
}
