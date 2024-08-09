using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    public bool enableRunner;
    public float distanciaEntreCarril;          // cuanto hay que desplazarse horizontalmente hacia el siguiente carril
    public int cantidadCarriles;                // se leen de izquierda a derecha (1 , 2 , 3 , 4 , etc)
    public int carrilInicial;                   // en cual carril comienza el player
    public float velocidadDesplazamientoCarril;
    public float lerpTimeDoblar;
    public int carrilActual;

    public Animator anim;

    Vector3 nextPosition;


    private void Start()
    {
        carrilActual = carrilInicial;
        nextPosition = transform.localPosition;
    }

    public void Desactivar()
    {
        enableRunner = false;
    }

    public void MoverDerecha()
    {
        if (!enableRunner) return;
        if (carrilActual == cantidadCarriles) return;

        nextPosition.x += distanciaEntreCarril;
        carrilActual++;

        StartCoroutine(Doblar("derecha"));
    }

    public void MoverIzquierda()
    {
        if (!enableRunner) return;
        if (carrilActual == 1) return;

        nextPosition.x -= distanciaEntreCarril;
        carrilActual--;

        StartCoroutine(Doblar("izquierda"));
    }

    private void FixedUpdate()
    {
        if (!enableRunner) return;

        transform.localPosition = Vector3.MoveTowards(transform.localPosition, nextPosition, velocidadDesplazamientoCarril * Time.fixedDeltaTime);
    }



    IEnumerator Doblar(string _animacion)
    {
        // Esta challa es para hacer que la mona mira pal lado donde esta doblando
        // Esto es porque no pille ninguna animación de girar corriendo y arme una animacion mula nomas
        // pueden eskipear si quieren

        anim.Play(_animacion, 1);
        float t = 0;

        // mira rapido para donde va a doblar
        while(t< (lerpTimeDoblar/2))
        {
            t += Time.unscaledDeltaTime;
            float p = t / lerpTimeDoblar;
            anim.SetLayerWeight(1, p);
            yield return null;
        }
        anim.SetLayerWeight(1, 1);
        t = lerpTimeDoblar;

        // vuelve a mirar para adelante
        while(t > 0)
        {
            t -= Time.unscaledDeltaTime;
            float p = t / lerpTimeDoblar;
            anim.SetLayerWeight(1, p);
            yield return null;
        }
        anim.SetLayerWeight(1, 0);
    }
}
