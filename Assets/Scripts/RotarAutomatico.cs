using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarAutomatico : MonoBehaviour
{
    public Vector3 direccionRotacion;
    public float velocidadRotacion;


    void Update()
    {
        transform.Rotate(direccionRotacion * velocidadRotacion * Time.deltaTime);
    }
}
