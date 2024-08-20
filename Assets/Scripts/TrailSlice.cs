using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailSlice : MonoBehaviour
{

    public TrailRenderer tr;



    void Update()
    {
        if(Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0); // Obtén el primer toque

            float distanceFromCamera = Vector3.Distance(transform.position, Camera.main.transform.position);

            // Convierte la posición del toque de screen position a world position considerando la distancia del objeto
            Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, distanceFromCamera));


            // Asigna la posición mundial al Transform
            transform.position = new Vector3(touchPosition.x, touchPosition.y, transform.position.z);
        }
    }
}
