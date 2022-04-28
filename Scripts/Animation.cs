using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{

    public float scaleDuration;



    static public IEnumerator smoothStep(System.Action<Vector3> set, Vector3 start, Vector3 destination, float duration) {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration) {
            float t = timeElapsed / duration;

            t = t * t * (3f - 2f * t);
            set(Vector3.Lerp(start, destination, t));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        set(destination);
    }

    static public IEnumerator smoothStep(System.Action<float> set, float start, float destination, float duration) {
        float timeElapsed = 0.0f;

        while (timeElapsed < duration) {
            float t = timeElapsed / duration;

            t = t * t * (3f - 2f * t);
            set(Mathf.Lerp(start, destination, t));

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        set(destination);
    }



    public void smoothScale(float scale) {
        Vector3 scaleFrom = transform.localScale;
        Vector3 scaleTo = new Vector3(scale, scale, 1f);
        StartCoroutine(  Animation.smoothStep((x) => transform.localScale = x, scaleFrom, scaleTo, scaleDuration) );
    }



    public void smoothScale(float from, float to) {
        Vector3 scaleFrom = new Vector3(from, from, 1f);
        Vector3 scaleTo = new Vector3(to, to, 1f);
        StartCoroutine(  Animation.smoothStep((x) => transform.localScale = x, scaleFrom, scaleTo, scaleDuration) );
    }
}
