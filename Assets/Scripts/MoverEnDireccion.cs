using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverEnDireccion : MonoBehaviour
{
    public bool desactivar;
    public Transform elTransform;
    public Vector3 direccion;
    public float velocidad;

    void Update()
    {
        if (desactivar) return;

        elTransform.Translate(direccion * velocidad * Time.deltaTime);
    }
}
