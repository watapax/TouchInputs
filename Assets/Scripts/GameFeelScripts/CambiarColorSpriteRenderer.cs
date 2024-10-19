using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CambiarColorSpriteRenderer : MonoBehaviour
{
    public Color color;
    public SpriteRenderer sr;

    public void CambiarColor()
    {
        sr.color = color;
    }
}
