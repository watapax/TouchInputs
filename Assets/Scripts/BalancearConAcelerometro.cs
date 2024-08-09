using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancearConAcelerometro : MonoBehaviour
{
    public float maxRotation = 40.0f;
    public float smoothTime = 0.05f; 
    public float maxTiltAngle = 45.0f; 
    public float smoothingFactor = 0.1f;

    Vector3 aceleracion;
    Vector3 aceleracionSuave;
    Quaternion targetRotation;
    Vector3 angulos;

    void Start()
    {
        aceleracionSuave = Vector3.zero;
        targetRotation = transform.rotation;
    }

    void Update()
    {
        aceleracion = Input.acceleration;
    }

    private void FixedUpdate()
    {
        aceleracionSuave = Vector3.Lerp(aceleracionSuave, aceleracion, smoothingFactor);


        angulos.z = Mathf.Clamp(aceleracionSuave.x * maxRotation, -maxTiltAngle, maxTiltAngle) * -1;
        angulos.x = Mathf.Clamp(aceleracionSuave.y * maxRotation, -maxTiltAngle, maxTiltAngle);

        targetRotation = Quaternion.Euler(angulos);

        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.fixedDeltaTime / smoothTime);
    }
}
