using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class DobleTap : MonoBehaviour
{
    public bool enableDobleTap;
    public UnityEvent onDobleTap;

    float dobleTapTime = 0.5f;
    float timeFirstTap;
    Touch touch;

    private void Update()
    {
        if (!enableDobleTap) return;

        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Began)
            {
                if(Time.time < timeFirstTap + dobleTapTime)
                {
                    print("doble tap");
                    timeFirstTap = 0;
                    onDobleTap.Invoke();
                }
                else
                {
                    timeFirstTap = Time.time;
                }
            }
        }
    }
}
