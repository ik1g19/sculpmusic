using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonHandler : MonoBehaviour
{
    public Camera camera;

    public GameObject sceneHandlerObj;

    public Button b_1, b_2;

    // Start is called before the first frame update
    void Start()
    {
        CameraMovement cmrMovement = camera.GetComponent<CameraMovement>();
        SceneHandler scnHandler = sceneHandlerObj.GetComponent<SceneHandler>();

        b_1.onClick.AddListener(() => cmrMovement.triggerCameraZoom(1f));
        b_1.onClick.AddListener(() => cmrMovement.triggerCameraPan(b_1.transform.position));
        b_1.onClick.AddListener(() => scnHandler.pauseThenLoadScene(cmrMovement.zoomDuration, "Interface1"));

        b_2.onClick.AddListener(() => cmrMovement.triggerCameraZoom(1f));
        b_2.onClick.AddListener(() => cmrMovement.triggerCameraPan(b_2.transform.position));
    }
}
