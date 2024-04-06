using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishGame : MonoBehaviour
{

    public float fadeDuration = 2.0f;

    [SerailizeField] SpriteRenderer spriteRenderer;

    private float alphaValue = 0.5f;
    private Material characterMaterial;

    // Start is called before the first frame update
    void Start()
    {
        if (spriteRenderer != null)
        {
            Color color = spriteRenderer.color;
            color.a = alphaValue;
            spriteRenderer.color = color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
