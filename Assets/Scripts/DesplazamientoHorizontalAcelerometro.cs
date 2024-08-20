using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazamientoHorizontalAcelerometro : MonoBehaviour
{

    public float speed;
    Vector3 aceleracion;

    private void Update()
    {
        aceleracion = Input.acceleration;
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector3.right * aceleracion.x * speed * Time.fixedDeltaTime,Space.Self);
    }
}
