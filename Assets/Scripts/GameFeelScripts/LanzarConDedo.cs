using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// este script es mas hueviado
// permite hacer drag del objeto usando el dedo
// y cuando se suelta el objeto saca la direccion que llevaba desde el frame actual hasta 5 frames atras
// luego usa ese vector de direccion para poder mover al objeto dentro del update
// para eso se hizo un historial de posiciones para siempre sacar la direccion  usando la primera y ultima entrada del historial

public class LanzarConDedo : MonoBehaviour
{
    public float promedioEnergia;
    Rigidbody2D rb2d;
    Vector3 screenPoint;
    Vector3 offset;

    bool seLanzo;


    HistorialPosiciones[] posiciones;
    int largoHistorial = 5;
    Vector3 velocidadFinal;

    public struct HistorialPosiciones
    {
        public Vector3 posicion;
        public float tiempo;

        public HistorialPosiciones(Vector3 _pos, float _t)
        {
            posicion = _pos;
            tiempo = _t;
        }

    }

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        rb2d.isKinematic = true;
        posiciones = new HistorialPosiciones[largoHistorial];
        lastPosition = transform.position;
    }

    void ActualizarHistorial(Vector3 _lastPos, float _lastTime)
    {
        for (int i = posiciones.Length - 1; i > 0; i--)
        {
            posiciones[i].posicion = posiciones[i - 1].posicion;
            posiciones[i].tiempo = posiciones[i - 1].tiempo;
        }

        posiciones[0].posicion = _lastPos;
        posiciones[0].tiempo = _lastTime;
    }



    private void OnMouseDown()
    {
        if (seLanzo) return;



        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    private void OnMouseDrag()
    {
        if (seLanzo) return;

        ActualizarHistorial(transform.position, Time.time);

        Vector3 posActual = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) + offset;
        transform.position = posActual;
    }

    private void OnMouseUp()
    {
        if (seLanzo) return;
        ActualizarHistorial(transform.position, Time.time);

        Vector3 prevPos = posiciones[largoHistorial - 1].posicion;
        Vector3 currentPos = posiciones[0].posicion;

        velocidadFinal = (currentPos - prevPos);

        seLanzo = true;

        rb2d.isKinematic = false;
        rb2d.AddForce(velocidadFinal, ForceMode2D.Impulse);// .velocity = velocidadFinal * Time.deltaTime * 5;
    }

    private void Update()
    {
        CalcularPromedioEnergia();
        if(seLanzo)
        {

            //transform.Translate(velocidadFinal * Time.deltaTime * 5);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("hola");
        Vector3 normal = collision.contacts[0].normal;
        velocidadFinal = normal * velocidadFinal.magnitude;
    }

    void  CalcularPromedioEnergia()
    {
        // Calcular la distancia recorrida en este frame
        float distance = Vector3.Distance(lastPosition, transform.position);

        // Calcular la velocidad (distancia / tiempo entre frames)
        currentSpeed = distance / Time.deltaTime;

        // Actualizar la última posición
        lastPosition = transform.position;

        // Verificar si está moviéndose rápido o lento
        if (currentSpeed > speedThreshold)
        {
            Debug.Log("El objeto se está moviendo rápido.");
        }
        else
        {
            Debug.Log("El objeto se está moviendo lento.");
        }
    }


    private Vector3 lastPosition;
    public float speedThreshold = 1.0f; // Velocidad en unidades por segundo
    public float currentSpeed;

}
