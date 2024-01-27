using UnityEngine;
using UnityEditor;

public class SpriteToMeshConverter : EditorWindow
{
    [MenuItem("Tools/Sprite to Mesh Converter")]
    static void ConvertAllSpriteRenderers()
    {
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        SpriteRenderer[] spriteRenderers = FindObjectsOfType<SpriteRenderer>();

        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            var go = spriteRenderer.gameObject;
            if (!go.activeSelf) continue;

            GameObject gameObject = spriteRenderer.gameObject;
            Sprite sprite = spriteRenderer.sprite;
            Texture texture = sprite.texture;

            // メッシュの生成
            Mesh mesh = new Mesh();
            mesh.vertices = System.Array.ConvertAll(sprite.vertices, i => (Vector3)i);
            mesh.triangles = System.Array.ConvertAll(sprite.triangles, i => (int)i);
            mesh.uv = sprite.uv;

            // MeshRenderer と MeshFilter の追加
            DestroyImmediate(spriteRenderer);
            MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = mesh;

            MeshRenderer meshRenderer = gameObject.AddComponent<MeshRenderer>();
            Material material = new Material(shader);
            material.mainTexture = texture;
            meshRenderer.sharedMaterial = material;

            // 元の SpriteRenderer の削除
            DestroyImmediate(spriteRenderer);

            AssetDatabase.CreateAsset(mesh, $"Assets/Meshes/{go.name}.asset");
            AssetDatabase.CreateAsset(material, $"Assets/Materials/{go.name}.mat");
        }
    }
}
