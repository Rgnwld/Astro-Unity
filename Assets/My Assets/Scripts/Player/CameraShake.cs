using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public GameObject camHolder;

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = camHolder.transform.position;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude + originalPosition.x;
            float y = Random.Range(-1f, 1f) * magnitude + originalPosition.y;

            camHolder.transform.position = new Vector3(x, y, originalPosition.z);
            elapsed += Time.deltaTime;
            yield return 0;
        }
        camHolder.transform.position = originalPosition;
    }
}