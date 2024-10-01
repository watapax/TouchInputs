using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalancearAcelerometter : MonoBehaviour
{
    public float multiplicadorGravedad = 1;
    public Rigidbody pelota;
    public float sensitivity = 10f;  // Sensibilidad de la rotaci�n
    public Vector2 rotationLimitsX = new Vector2(-30f, 30f);
    public Vector2 rotationLimitsZ = new Vector2(-30f, 30f);
    public float smoothFactor = 0.1f;  // Factor de suavizado

    // Para almacenar el valor suavizado del aceler�metro
    private Vector3 smoothedAcceleration;

    // Para almacenar la calibraci�n
    private Vector3 calibrationOffset = Vector3.zero;
    Vector3 pelotaStartPos;
    Vector3 startGravity;
    void Start()
    {
        startGravity = Physics.gravity;
        smoothedAcceleration = Input.acceleration;
        pelotaStartPos = pelota.transform.position;

    }

    void Update()
    {
        Physics.gravity = startGravity * multiplicadorGravedad;
        // Leer la entrada del aceler�metro
        Vector3 rawAcceleration = Input.acceleration;

        // Suavizar el valor del aceler�metro
        smoothedAcceleration = Vector3.Lerp(smoothedAcceleration, rawAcceleration, smoothFactor);

        // Ajustar el aceler�metro seg�n la calibraci�n
        Vector3 adjustedAcceleration = smoothedAcceleration - calibrationOffset;

        // Calcular el �ngulo de inclinaci�n en el eje X y Z
        float tiltX = adjustedAcceleration.x * sensitivity;
        float tiltZ = adjustedAcceleration.y * sensitivity;

        // Aplicar los l�mites a la rotaci�n
        tiltX = Mathf.Clamp(tiltX, rotationLimitsX.x, rotationLimitsX.y);
        tiltZ = Mathf.Clamp(tiltZ, rotationLimitsZ.x, rotationLimitsZ.y);

        // Aplicar la rotaci�n al transform
        transform.rotation = Quaternion.Euler(tiltZ, 0, -tiltX);
    }

    // Funci�n que se llama al presionar el bot�n de calibraci�n
    public void Calibrate()
    {
        // Guardar la posici�n actual del aceler�metro como "origen"
        calibrationOffset = smoothedAcceleration;
        Debug.Log("Calibrado: " + calibrationOffset);
        pelota.position = pelotaStartPos;
        pelota.velocity = Vector3.zero;
        pelota.angularVelocity = Vector3.zero;
        
    }
}
