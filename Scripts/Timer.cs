using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    private Slider timer;



    void Start() {
        timer = GetComponent<Slider>();
    }



    void OnEnable()
    {
        PresentationEngine.StartTimer += triggerTimer;
    }



    void OnDisable()
    {
        PresentationEngine.StartTimer -= triggerTimer;
    }



    public void triggerTimer() {
        float length = StoryEngine.currentStorylet.tape.length;
        StartCoroutine(countDown(length));
    }



    IEnumerator countDown(float length) {
        timer.value = 1;
        float timeElapsed = 0.0f;

        while (timeElapsed < length) {
            float t = timeElapsed / length;

            t = t * t * (3f - 2f * t);
            timer.value = 1 - Mathf.Lerp(0, 1, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        timer.value = 0;
    }

}
