using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float panDuration = 3.0f;



    void Start() {
        transform.position = StoryEngine.currentStorylet.gameObject.transform.position - new Vector3(0,0,10);
    }



    void triggerCameraPan() {
        StartCoroutine(panCamera(StoryEngine.currentStorylet.gameObject.transform.position - new Vector3(0,0,10)));
    }



    IEnumerator panCamera(Vector3 destination) {
        Vector3 startPos = transform.position;
        float timeElapsed = 0.0f;

        // while (timeElapsed < panDuration) {
        // //while (!transform.position.Equals(destination)) {
        //     transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, panDuration);

        //     timeElapsed += Time.deltaTime;

        //     yield return null;
        // }

        while (timeElapsed < panDuration) {
        //while (!transform.position.Equals(destination)) {
            float t = timeElapsed / panDuration;

            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, destination, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }



    void OnEnable()
    {
        PresentationEngine.PanCamera += triggerCameraPan;
    }



    void OnDisable()
    {
        PresentationEngine.PanCamera -= triggerCameraPan;
    }
}
