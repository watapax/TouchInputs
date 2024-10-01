using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    public CinemachineImpulseSource impulseSource;

    public void GenerarShake(Vector3 velocidad)
    {
        impulseSource.m_DefaultVelocity = velocidad;

    }
}
