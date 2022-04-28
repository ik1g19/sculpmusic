using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Camera camera;

    public float panDuration = 3.0f;
    public float zoomDuration = 3.0f;



    void Start() {
        Storylet startingStorylet = StoryEngine.currentStorylet;
        if (startingStorylet != null) camera.transform.position = startingStorylet.gameObject.transform.position - new Vector3(0,0,10);
    }



    public void triggerCameraPan(Vector3 pos) {
        pos -= new Vector3(0,0,10);
        StartCoroutine(Animation.smoothStep((x) => transform.position = x, transform.position, pos, panDuration));
    }



    public void triggerCameraZoom(float size1, float size2) {
        StartCoroutine(  Animation.smoothStep((x) => camera.orthographicSize = x, size1, size2, zoomDuration)  );
    }
}
