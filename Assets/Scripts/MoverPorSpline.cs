using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class MoverPorSpline : MonoBehaviour
{
    public bool desactivar;
    public SplineContainer spline;
    public float velocidad;

    bool detener;
    float valorNormalizado;
    float tiempoActual;
    float currentVelocity;
    float startSpeed;

    private void Start()
    {
        startSpeed = velocidad / 100;
    }

    private void Update()
    {
        if (desactivar) return;

        Mover();
    }

    public void Detener()
    {
        if (desactivar) return;
        detener = true;
        StartCoroutine(CambiarVelocidad(true));
    }

    public void Continuar()
    {
        if (desactivar) return;
        detener = false;
        StartCoroutine(CambiarVelocidad(false));
    }

    public void Resetear()
    {
        if (desactivar) return;
        tiempoActual = 0;
    }

    void Mover()
    {
        if (!detener)
        {
            currentVelocity = velocidad / 100;    // escalo este numero porque anda muy rajao en valores chicos
        }

        tiempoActual += Time.deltaTime * currentVelocity;

        valorNormalizado = tiempoActual % 1;

        Vector3 pos = spline.EvaluatePosition(valorNormalizado);
        Vector3 tangent = spline.EvaluateTangent(valorNormalizado);
        Vector3 upVector = spline.EvaluateUpVector(valorNormalizado);

        Quaternion rot = Quaternion.LookRotation(tangent, upVector);

        transform.position = pos;
        transform.rotation = rot;
    }

    IEnumerator CambiarVelocidad(bool _frenar)
    {
        float t = 0;
        float lerpTime = 0.5f;
        float from = _frenar ? currentVelocity : 0;
        float to = _frenar ? 0 : startSpeed;
        while (t < lerpTime)
        {
            t += Time.deltaTime;
            float n = t / lerpTime;
            currentVelocity = Mathf.Lerp(from, to, n);
            yield return null;
        }
    }

    public void Desactivar() => desactivar = true;
    public void Activar() => desactivar = false;


}
