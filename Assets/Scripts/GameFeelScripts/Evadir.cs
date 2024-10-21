using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Evadir : MonoBehaviour
{
    public BoxCollider2D areaDeRestriccionMovimiento;
    Vector2 upRight, upLeft, downRight, downLeft;
    public float velocidad;
    Vector3 screenPoint;
    Vector3 offset;
    bool startDrag;

    void DefinirBordes()
    {
        Vector2 center = areaDeRestriccionMovimiento.bounds.center;
        Vector2 size = areaDeRestriccionMovimiento.bounds.size;

        upRight = center + new Vector2(size.x / 2, size.y / 2);
        upLeft = center + new Vector2(-size.x / 2, size.y / 2);
        downRight = center + new Vector2(size.x / 2, -size.y / 2);
        downLeft = center + new Vector2(-size.x / 2, -size.y / 2);
    }

    private void Start()
    {
        DefinirBordes();
    }
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
            posActual.x = Mathf.Clamp(posActual.x, upLeft.x, upRight.x);
            posActual.y = Mathf.Clamp(posActual.y, downLeft.y, upRight.y);
            transform.position = Vector3.Lerp(transform.position, posActual, velocidad * Time.deltaTime);
        }
    }
}
