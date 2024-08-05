using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MapaController : MonoBehaviour
{
    public bool activarMapController;
    public CinemachineVirtualCamera virtualCamera;
    public Transform cameraTarget;
    public float sensibilidad = 0.03f;

    Touch touch0, touch1;
    Vector2 promedio;
    Vector3 newPos;

    private void Start()
    {
        newPos = cameraTarget.position;
        SetCameraTarget(cameraTarget);
    }

    public void SetCameraTarget(Transform _transformTarget)
    {
        virtualCamera.LookAt = _transformTarget;
        virtualCamera.Follow = _transformTarget;
    }

    private void Update()
    {
        if (!activarMapController) return;

        if(Input.touchCount == 2)
        {
            CameraPaning();
        }

        cameraTarget.position = Vector3.Lerp(cameraTarget.position, newPos, 10 * Time.deltaTime);
    }

    void CameraPaning()
    {
        touch0 = Input.GetTouch(0);
        touch1 = Input.GetTouch(1);

        promedio.x = (touch0.deltaPosition.x + touch1.deltaPosition.x) / 2;
        promedio.y = (touch0.deltaPosition.y + touch1.deltaPosition.y) / 2;

        newPos = cameraTarget.position;
        newPos.x -= promedio.x * sensibilidad;
        newPos.z -= promedio.y * sensibilidad;



    }

    void PinchAndZoom()
    {

    }
    


}
