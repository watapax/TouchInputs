using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElRagdoll : MonoBehaviour
{
    Rigidbody[] rbs;
    Collider[] colls;

    public Animator anim;
    public Transform transformRootRagdoll;

    List<Vector3> posicionesJoints = new List<Vector3>();
    List<Quaternion> rotacionesJoints = new List<Quaternion>();

    private void Start()
    {
        rbs = transformRootRagdoll.GetComponentsInChildren<Rigidbody>();
        colls = transformRootRagdoll.GetComponentsInChildren<Collider>();

        DesactivarRagdoll();
    }


    void GuardarPose()
    {
        posicionesJoints.Clear();
        rotacionesJoints.Clear();
        foreach (Transform child in transformRootRagdoll)
        {
            posicionesJoints.Add(child.position);
            rotacionesJoints.Add(child.rotation);
        }
    }

    void SetearPose()
    {
        int i = 0;
        foreach (Transform child in transformRootRagdoll)
        {
            child.position = posicionesJoints[i];
            child.rotation = rotacionesJoints[i];
            i++;
        }
    }

    [ContextMenu("ActivarRagdoll")]
    public void ActivarRagdoll()
    {

        GuardarPose();

        anim.enabled = false;

        SetearPose();

        foreach(Collider c in colls)
        {
            c.enabled = true;
        }

        foreach(Rigidbody r in rbs)
        {
            r.isKinematic = false;
        }

    }

    public void DesactivarRagdoll()
    {
        anim.enabled = true;
        foreach (Collider c in colls)
        {
            c.enabled = false;
        }

        foreach (Rigidbody r in rbs)
        {
            r.isKinematic = true;
        }

    }


}
