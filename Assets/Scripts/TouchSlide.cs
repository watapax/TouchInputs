using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class TouchSlide : MonoBehaviour
{
    public bool enableSlide;
    public UnityEvent onRightSlide, onLeftSlide, onUpSlide, onDownSlide;

    Touch touch;
    Vector2 touchStartPos, touchLastPos;
    Vector2 screenResolution;
    float minimoPorcentajeDesplazamiento = 0.1f; // 10 porciento de ancho o alto

    public void Desactivar()
    {
        enableSlide = false;
    }

    private void Update()
    {
        if (!enableSlide) return;

        screenResolution.x = Display.main.systemWidth;
        screenResolution.y = Display.main.systemHeight;

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                touchStartPos = touch.position;
            }

            if(touch.phase == TouchPhase.Ended)
            {
                touchLastPos = touch.position;
                ChequearTipoDeTouch(touchStartPos, touchLastPos);
            }
        }

        void ChequearTipoDeTouch(Vector3 startPos, Vector3 endPos)
        {
            float deltaX = endPos.x - startPos.x;
            float deltaY = endPos.y - startPos.y;


            float porcentajeDesplazamientoX = Mathf.Abs(deltaX) / screenResolution.x;
            float porcentajeDesplazamientoY = Mathf.Abs(deltaY) / screenResolution.y;

            
            if (porcentajeDesplazamientoX < minimoPorcentajeDesplazamiento && porcentajeDesplazamientoY < minimoPorcentajeDesplazamiento)
            {
                print("se movio muy poquito");
                return;
            }
            


            if(Mathf.Abs(deltaX) > Mathf.Abs(deltaY))
            {
                if(deltaX > 0)
                {
                    print("derecha");
                    onRightSlide.Invoke();
                }
                else
                {
                    print("izquierda");
                    onLeftSlide.Invoke();
                }

            }
            else
            {
                if(deltaY > 0)
                {
                    print("arriba");
                    onUpSlide.Invoke();
                }
                else
                {
                    print("abajo");
                    onDownSlide.Invoke();
                }
            }
        }
    }

}
