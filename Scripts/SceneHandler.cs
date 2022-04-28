using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneHandler : MonoBehaviour
{
    public CameraMovement camera;
    public Animation circle;
    private string scene;

    public UnityEvent onMenuLoad;



    void Start() {
        camera.triggerCameraZoom(1f,250f);
    }




    public void loadMenu() {
        StartCoroutine(loadSequenceReverse());
    }

    public void loadInterface(string scene) {
        this.scene = scene;
        StartCoroutine(loadSequence());
    }

    IEnumerator loadSequence() {
        camera.triggerCameraZoom(1f);
        yield return new WaitForSeconds(camera.zoomDuration);
        circle.smoothScale(5f);
        yield return new WaitForSeconds(circle.scaleDuration);
        SceneManager.LoadScene(scene);
    }

    IEnumerator loadSequenceReverse() {
        camera.triggerCameraZoom(1f);
        onMenuLoad.Invoke();
        yield return new WaitForSeconds(camera.zoomDuration);
        Animation storyletCircle = StoryEngine.currentStorylet.gameObject.GetComponent<Animation>();
        storyletCircle.smoothScale(8f, 0f);
        yield return new WaitForSeconds(storyletCircle.scaleDuration);
        SceneManager.LoadScene("Menu");
    }
}
