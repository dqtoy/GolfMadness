using UnityEngine;

public class TextureDisplacementEffect : MonoBehaviour
{

    [SerializeField] Vector2 _baseTexture = new Vector2(0.5f, 0f);
    [SerializeField] Vector2 _scrollSpeed = new Vector2(0.5f, 0f);
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        var offset = Time.time * _scrollSpeed;
        rend.material.SetTextureOffset("_MainTex", _baseTexture + offset);
    }
}