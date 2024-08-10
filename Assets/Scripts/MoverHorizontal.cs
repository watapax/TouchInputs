using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverHorizontal : MonoBehaviour
{
    public float velocidad;
    public float min_x, max_x;
    public Transform elTransform;
    Touch touch;
    Vector3 posicionFinal;

    private void Start()
    {
        posicionFinal = elTransform.localPosition;
    }
    private void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            Vector2 vp = Camera.main.ScreenToViewportPoint(touch.position);
            float direccion = vp.x > 0.5f ? 1 : -1;
            posicionFinal.x += direccion * velocidad * Time.deltaTime;
            posicionFinal.x = Mathf.Clamp(posicionFinal.x, min_x, max_x);
            elTransform.localPosition = posicionFinal;
        }
    }
}
