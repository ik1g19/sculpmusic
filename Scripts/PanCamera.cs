using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanCamera : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float panDuration = 3.0f;
    public float zoomDuration = 3.0f;



    void Start() {
        transform.position = StoryEngine.currentStorylet.gameObject.transform.position - new Vector3(0,0,10);
    }



    void triggerCameraPan() {
        StartCoroutine(panCamera(StoryEngine.currentStorylet.gameObject.transform.position - new Vector3(0,0,10)));
    }



    void triggerCameraZoom() {
        StartCoroutine(zoomCamera(250.0f));
    }



    IEnumerator panCamera(Vector3 destination) {
        Vector3 startPos = transform.position;
        float timeElapsed = 0.0f;

        while (timeElapsed < panDuration) {
            float t = timeElapsed / panDuration;

            t = t * t * (3f - 2f * t);
            transform.position = Vector3.Lerp(startPos, destination, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = destination;
    }



    IEnumerator zoomCamera(float size) {
        float startPos = gameObject.GetComponent<Camera>().orthographicSize;
        float timeElapsed = 0.0f;

        while (timeElapsed < zoomDuration) {
            float t = timeElapsed / zoomDuration;

            t = t * t * (3f - 2f * t);
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.Lerp(startPos, size, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        gameObject.GetComponent<Camera>().orthographicSize = size;
    }



    void OnEnable()
    {
        PresentationEngine.PanCamera += triggerCameraPan;
        PresentationEngine.CameraZoom += triggerCameraZoom;
    }



    void OnDisable()
    {
        PresentationEngine.PanCamera -= triggerCameraPan;
        PresentationEngine.CameraZoom += triggerCameraZoom;
    }
}
