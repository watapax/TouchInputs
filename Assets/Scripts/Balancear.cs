using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balancear : MonoBehaviour
{
    private Quaternion initialRotation;  // Almacena la rotaci�n inicial para la calibraci�n
    private bool isCalibrated = false;
    private bool gyroEnabled = false;

    public float rotationSpeed = 100.0f; // Sensibilidad de rotaci�n

    // L�mites de rotaci�n para cada eje
    public Vector2 xRotationLimits = new Vector2(-30f, 30f);
    public Vector2 yRotationLimits = new Vector2(-45f, 45f);
    public Vector2 zRotationLimits = new Vector2(-20f, 20f);

    void Start()
    {
        if (SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled = true;  // Habilitar el giroscopio
            gyroEnabled = true;
        }
        else
        {
            gyroEnabled = false;
            Debug.LogWarning("El dispositivo no soporta giroscopio.");
        }
    }

    void Update()
    {
        if (gyroEnabled && isCalibrated)
        {
            // Obtener la rotaci�n actual del giroscopio
            Quaternion currentRotation = Input.gyro.attitude;

            // Ajustar la rotaci�n a las coordenadas de Unity (intercambiando ejes Y y Z)
            currentRotation = new Quaternion(currentRotation.x, -currentRotation.z, currentRotation.y, currentRotation.w);

            // Aplicar la diferencia de rotaci�n con respecto a la rotaci�n inicial (calibrada)
            Quaternion calibratedRotation = currentRotation * Quaternion.Inverse(initialRotation);

            // Convertir la rotaci�n a �ngulos de Euler para manipular cada eje individualmente
            Vector3 eulerAngles = calibratedRotation.eulerAngles;

            // Limitar la rotaci�n en el eje X
            eulerAngles.x = ClampAngle(eulerAngles.x, xRotationLimits.x, xRotationLimits.y);

            // Limitar la rotaci�n en el eje Y
            //eulerAngles.y = ClampAngle(eulerAngles.y, yRotationLimits.x, yRotationLimits.y);
            eulerAngles.y = 0;

            // Limitar la rotaci�n en el eje Z
            eulerAngles.z = ClampAngle(eulerAngles.z, zRotationLimits.x, zRotationLimits.y);

            // Aplicar la rotaci�n limitada al objeto
            transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(eulerAngles), Time.deltaTime * rotationSpeed);
        }
    }

    // M�todo para calibrar el giroscopio
    public void CalibrateGyro()
    {
        if (gyroEnabled)
        {
            initialRotation = Input.gyro.attitude;
            initialRotation = new Quaternion(initialRotation.x, -initialRotation.z, initialRotation.y, initialRotation.w);
            isCalibrated = true;
            Debug.Log("Giroscopio calibrado.");
        }
    }

    // Funci�n para limitar un �ngulo dentro de un rango espec�fico
    private float ClampAngle(float angle, float min, float max)
    {
        if (angle > 180f) angle -= 360f;  // Convertir el rango de 0-360 a -180 a 180
        return Mathf.Clamp(angle, min, max);
    }
}
