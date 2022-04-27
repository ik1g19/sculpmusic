using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Vector3 velocity = Vector3.zero;
    public float panDuration = 3.0f;
    public float zoomDuration = 3.0f;



    void Start() {
        Storylet startingStorylet = StoryEngine.currentStorylet;
        if (startingStorylet != null) transform.position = startingStorylet.gameObject.transform.position - new Vector3(0,0,10);
    }



    public void triggerCameraPan(Vector3 pos) {
        StartCoroutine(panCamera(pos - new Vector3(0,0,10)));
    }



    public void triggerCameraZoom(float orthoSize) {
        StartCoroutine(zoomCamera(orthoSize));
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
}
