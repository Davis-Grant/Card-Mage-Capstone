using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//implemented by robert bothne
public class shake : MonoBehaviour
{
    public AnimationCurve curve;
    public bool start = false;
    public float duration = 1f;
    void Update()
    {
        
    }
    //move camera within set position on curve.
    IEnumerator Shaking()
    {
        Vector3 startPos = transform.position;
        float elapsedT = 0f;
        while (elapsedT < duration)
        {
            elapsedT += Time.deltaTime;
            transform.position = startPos + Random.insideUnitSphere;
            yield return null;
        }
        transform.position = startPos;
    }
}
