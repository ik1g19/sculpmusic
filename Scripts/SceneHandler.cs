using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class SceneHandler : MonoBehaviour
{
    public CameraMovement camera;
    public TapePlayer tapePlayer;
    public Animation circle;
    private string scene;
    public GameObject menuButton;
    public GameObject timer;

    [HideInInspector]
    public UnityEvent onMenuLoad;



    void Start() {
        camera.triggerCameraZoom(1f,250f);
        if (menuButton != null) StartCoroutine(toggleMenuButton());
    }

    IEnumerator toggleMenuButton() {
        yield return new WaitForSeconds(camera.zoomDuration);
        yield return new WaitForSeconds(0.7f);
        menuButton.SetActive(true);
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

        float scaleDuration = 3f;
        circle.smoothScale(5f, scaleDuration);
        yield return new WaitForSeconds(scaleDuration);

        SceneManager.LoadScene(scene);
    }

    IEnumerator loadSequenceReverse() {
        camera.triggerCameraZoom(1f);
        onMenuLoad.Invoke();
        tapePlayer.fadeOut();

        yield return new WaitForSeconds(camera.zoomDuration);
        menuButton.SetActive(false);
        if (timer != null) timer.SetActive(false);

        Animation storyletCircle = StoryEngine.currentStorylet.gameObject.GetComponent<Animation>();
        float scaleDuration = 3f;
        storyletCircle.smoothScale(8f, 0f, scaleDuration);
        yield return new WaitForSeconds(scaleDuration);

        SceneManager.LoadScene("Menu");
    }
}
