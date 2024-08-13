using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OffsetStartAnimator : MonoBehaviour
{
    public Animator anim;

    private void OnEnable()
    {
        float randomNormalizedTime = Random.Range(0f, 1f);
        string stateName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        anim.Play(stateName, 0, randomNormalizedTime);
    }
}
