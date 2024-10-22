using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AtaqueConDedo : MonoBehaviour
{
    public UnityEvent onMakeDamage;
    Vector3 screenPoint;
    Vector3 offset;

    private Vector3 lastPosition;

    float currentSpeed;
    float energiaActual;
    public float gravedadEnergia;
    public float maxEnergia;
    float energiaAcumulada;

    bool isDragging;
    public SpriteRenderer[] fasesSpriteRenderer;
    public float damageBase;
    public float velocidadBase;
    public float velocidadRotacion;
   
    float totalDamage;
    bool seLanzo;

    public Transform targetTransform;
    float velocidadCambioDireccion = 20;
    float amplificadorVelocidad;
    float amplificadorRotacion;



    private void Awake()
    {
        lastPosition = transform.position;
        startPosition = transform.position;
        startRotation = transform.rotation;

        CalcularFase();
    }

    private void OnMouseDown()
    {
        if (seLanzo) return;
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        isDragging = true;

    }

    private void OnMouseDrag()
    {
        if (seLanzo) return;
        Vector3 posActual = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z)) + offset;
        transform.position = posActual;

    }

    private void OnMouseUp()
    {
        isDragging = false;
        if(transform.up.y < 0)
        {
            Vector3 newUp = transform.up;
            newUp.y *= -1;
            transform.up = newUp;
        }
        seLanzo = true;


    }

    private void Update()
    {
        CalcularPromedioEnergia();
    }

    private void FixedUpdate()
    {
        if (!seLanzo) return;

        Vector3 nuevaDireccion = (targetTransform.position - transform.position).normalized;
        transform.up = Vector3.Lerp(transform.up, nuevaDireccion, (velocidadRotacion + amplificadorRotacion) * Time.fixedDeltaTime);
        transform.Translate(transform.up * (velocidadBase + amplificadorVelocidad) * Time.deltaTime, Space.World);

    }


    void CalcularFase()
    {
        int index = 0;
        if(energiaAcumulada < 10)
        {
            index = 0;
        }
        else if(energiaAcumulada < 20)
        {
            index = 1;
        }
        else if (energiaAcumulada < 30)
        {
            index = 2;
        }
        else if (energiaAcumulada >= 30)
        {
            index = 3;
        }

        for (int i = 0; i < fasesSpriteRenderer.Length; i++)
        {
            fasesSpriteRenderer[i].enabled = i == index;
        }
        totalDamage = damageBase * (index + 1);
        amplificadorVelocidad = 4 * (index + 1);
        amplificadorRotacion = index * .5f;
    }



    void CalcularPromedioEnergia()
    {
        energiaAcumulada -=  Time.deltaTime * (isDragging?gravedadEnergia : gravedadEnergia * 4);

        float distance = Vector3.Distance(lastPosition, transform.position);
        currentSpeed = distance / Time.deltaTime;

        Vector3 direccion = (transform.position - lastPosition).normalized;
        if(distance > 0.1f && isDragging)
        {      
            transform.up = Vector3.Lerp(transform.up, direccion, velocidadCambioDireccion * Time.deltaTime);
        }


        lastPosition = transform.position;

        energiaAcumulada += currentSpeed * Time.deltaTime;
        energiaAcumulada = Mathf.Clamp(energiaAcumulada, 0, maxEnergia);
        energiaActual = energiaAcumulada;

        if(!seLanzo)
            CalcularFase();

    }

    Quaternion DireccionObjeto(Vector3 _direccion)
    {
        _direccion.z = Mathf.Abs(_direccion.y);
        return Quaternion.LookRotation(_direccion);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IDamagable>() != null)
        {
            collision.GetComponent<IDamagable>().TakeDamage((int)totalDamage);
            onMakeDamage.Invoke();
        }
    }



    public void ResetearBala()
    {
        isDragging = false;
        seLanzo = false;
        transform.position = startPosition;
        transform.rotation = startRotation;
    }

    Vector3 startPosition;
    Quaternion startRotation;


}
