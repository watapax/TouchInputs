using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class MoverATarget : MonoBehaviour
{
    public UnityEvent onTimeLimit;
    bool moverse;
    public float velocidad, velocidadRotacion, tiempoDeVida;
    public bool homing = true;
    public bool aceleran = true;
    Transform targetTransform;
    float spawnTime;
    public void SetDireccion(Vector3 direccion, Transform _targetTransform)
    {
        transform.right = direccion;
        targetTransform = _targetTransform;
        moverse = true;
        spawnTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > spawnTime + tiempoDeVida)
        {
            onTimeLimit.Invoke();
        }
        if (!moverse) return;

        if(homing)
        {
            Vector3 nuevaDireccion = (targetTransform.position - transform.position).normalized;
            transform.right = Vector3.Lerp(transform.right, nuevaDireccion, velocidadRotacion * Time.deltaTime);
        }


        
        transform.Translate(transform.right * velocidad * Time.deltaTime, Space.World);
        if(aceleran)velocidad += 0.001f;
    }
}
