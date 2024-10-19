using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evadir : MonoBehaviour
{
    public float velocidad;
    Vector3 screenPoint;
    Vector3 offset;
    bool startDrag;
    private void OnMouseDown()
    {

        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        startDrag = true;
    }

    private void OnMouseUp()
    {
        startDrag = false;


    }

    private void Update()
    {
        if(startDrag)
        {
            Vector3 posActual = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) + offset;
            transform.position = Vector3.Lerp(transform.position, posActual, velocidad * Time.deltaTime);
        }
    }
}
