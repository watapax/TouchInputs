using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MapaController : MonoBehaviour
{
    public bool puedePanear, puedeRotar, puedeHacerZoom;
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTarget;

    Touch touch0, touch1;
    
    // para el paneo
    public float sensibilidadPaneo = 0.5f;
    public float sensibilidadMinimaPaneo = 0.1f;
    float porcentajeDeZoom;
    float maxSensibility;
    Vector3 newPos;

    // para la rotacion
    public float sensibilidadRotacion;
    Vector2 previousTouchPosition1;
    Vector2 previousTouchPosition2;

    // para el zoom
    Vector2 currentTouchPosition1; 
    Vector2 currentTouchPosition2;
    float initialDistance;
    float initialCameraFOV;
    public float minZoom, maxZoom;
    

    private void Start()
    {
        SetCameraTarget(cameraTarget);
        maxSensibility = sensibilidadPaneo;
        porcentajeDeZoom = (virtualCamera.m_Lens.FieldOfView - minZoom) / (maxZoom - minZoom);
    }

    public void SetCameraTarget(Transform _transformTarget)
    {
        virtualCamera.LookAt = _transformTarget;
        virtualCamera.Follow = _transformTarget;
    }

    private void Update()
    {

        if(Input.touchCount == 2)
        { 
            touch0 = Input.GetTouch(0);
            touch1 = Input.GetTouch(1);
            RotarMapa();
            PinchAndZoom();
        }
        if(Input.touchCount == 1 )
        {
            touch0 = Input.GetTouch(0);
            CameraPaning();
        }

    }

    // PANEO

    void CameraPaning()
    {
        if (!puedePanear) return;

        sensibilidadPaneo = maxSensibility * porcentajeDeZoom;
        sensibilidadPaneo = Mathf.Clamp(sensibilidadPaneo, sensibilidadMinimaPaneo, maxSensibility);

        newPos.Set(touch0.deltaPosition.x * -1, 0, touch0.deltaPosition.y * -1);
        newPos *= sensibilidadPaneo;

        if (touch0.phase == TouchPhase.Moved)
        {
            cameraTarget.Translate(newPos * Time.deltaTime, Space.Self);
        }

    }


    // ZOOM

    void PinchAndZoom()
    {
        if (!puedeHacerZoom) return;

        float currentDistance = Vector2.Distance(touch0.position, touch1.position);

        if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
        {
            initialDistance = currentDistance;
            initialCameraFOV = virtualCamera.m_Lens.FieldOfView;
        }
        else
        {
            float scaleFactor =  initialDistance / currentDistance;
            virtualCamera.m_Lens.FieldOfView = initialCameraFOV * scaleFactor;
            virtualCamera.m_Lens.FieldOfView = Mathf.Clamp(virtualCamera.m_Lens.FieldOfView, minZoom, maxZoom);
            float currentFov = virtualCamera.m_Lens.FieldOfView;
            porcentajeDeZoom = (currentFov - minZoom) / (maxZoom - minZoom);
        }

    }


    // ROTAR

    void RotarMapa()
    {
        if (!puedeRotar) return;

        currentTouchPosition1 = touch0.position;
        currentTouchPosition2 = touch1.position;

        if(touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
        {
            previousTouchPosition1 = currentTouchPosition1;
            previousTouchPosition2 = currentTouchPosition2;
        }
        else
        {
            Vector2 previousDirection = previousTouchPosition2 - previousTouchPosition1;
            Vector2 currentDirection = currentTouchPosition2 - currentTouchPosition1;

            float previousAngle = Mathf.Atan2(previousDirection.y, previousDirection.x) * Mathf.Rad2Deg;
            float currentAngle = Mathf.Atan2(currentDirection.y, currentDirection.x) * Mathf.Rad2Deg;

            float angleDelta = (currentAngle - previousAngle) * sensibilidadRotacion;

            cameraTarget.Rotate(0, angleDelta, 0);

            previousTouchPosition1 = currentTouchPosition1;
            previousTouchPosition2 = currentTouchPosition2;
        }

    }
    


}
