using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance { get; private set; }
    public bool useScreenShake = true;
    private void Awake()
    {
       if(Instance != null && Instance != this)
           Destroy(this);
       else
       {
           Instance = this;
       }
    }

    public void Shake(float magnitude, float duration)
    {
        if(useScreenShake)
            StartCoroutine(DoShake(magnitude, duration));
    }

    IEnumerator DoShake(float magnitude, float duration)
    {
        Vector3 initialPos = transform.localPosition;
        float time = 0f;
        while (time < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            transform.localPosition = new Vector3(x, y, initialPos.z);
            time += Time.deltaTime;
            
            yield return null;
        }

        transform.localPosition = initialPos;
    }
}
