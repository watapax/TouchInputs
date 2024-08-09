using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RodarConAcelerometro : MonoBehaviour
{

    public Rigidbody rb;
    public float fuerza;
    Vector3 aceleracion;

    private void Update()
    {
        aceleracion = Input.acceleration;
    }

    private void FixedUpdate()
    {
        Vector3 torque = new Vector3(aceleracion.y, 0, -aceleracion.x);
        rb.AddTorque(torque * fuerza);
    }
}
