using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public IEnumerator Shake(float duratie, float magnitudine)
    {
        Vector3 pozitieInitiala = transform.localPosition;

        float elapsed = 0f;

        while (elapsed < duratie)
        {
            float x = Random.Range(-1f, 1f) * magnitudine;
            float y = Random.Range(-1f, 1f) * magnitudine;

            transform.localPosition = new Vector3(pozitieInitiala.x- x,pozitieInitiala.y- y, pozitieInitiala.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.localPosition = pozitieInitiala;
    }
}
