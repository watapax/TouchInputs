using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JoystickTouch : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform stickTransform;
    public CanvasGroup canvasGroup;
    public  Canvas canvas;
    public float radio;
    public float horizontal, vertical;
    Vector2 stickStartPos;

    private void Awake()
    {
        stickStartPos = stickTransform.anchoredPosition;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {

        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {

        stickTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;

        Vector2 direccion = stickTransform.anchoredPosition - stickStartPos;
        if(direccion.magnitude > radio)
        {
            stickTransform.anchoredPosition = stickStartPos + direccion.normalized * radio;
        }

        horizontal = Math.Clamp(direccion.x / radio, -1,1);
        vertical = Math.Clamp(direccion.y / radio, -1,1);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // Permitir que el objeto vuelva a ser detectado por raycast
        canvasGroup.blocksRaycasts = true;
        stickTransform.anchoredPosition = stickStartPos;

        horizontal = 0;
        vertical = 0;
    }

}
