using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoPorInput : MonoBehaviour
{
    public JoystickTouch joystickTouch;
    public float velocidad;


    void Update()
    {
        Vector2 input = new Vector2(joystickTouch.horizontal, joystickTouch.vertical);

        transform.Translate(input * velocidad * Time.deltaTime);
    }
}
