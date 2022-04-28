using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public Camera camera;
    private float toWait;
    
    void Start() {toWait = camera.GetComponent<CameraMovement>().zoomDuration;}

    public void loadScene(string scene) { StartCoroutine(pauseThenLoad(scene)); }

    IEnumerator pauseThenLoad(string scene) {yield return new WaitForSeconds(toWait); SceneManager.LoadScene(scene);}
}
