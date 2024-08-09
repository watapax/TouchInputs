using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoMove : MonoBehaviour
{
    public Vector3 direccionMovimiento;
    public float velocidadMovimiento;
    public Rigidbody rb;

    private void FixedUpdate()
    {

        rb.velocity = direccionMovimiento * velocidadMovimiento * Time.fixedDeltaTime;
    }
}
