using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private AudioSource audioSrc;
    private float volume = 1.0f;

    public delegate void TapeFinished(bool changeStorylet);
    public static event TapeFinished OnTapeEnd;

    private Coroutine running;
    private float time;



    void OnEnable()
    {
        Storylet.OnHover += storyletHovered;
    }



    void OnDisable()
    {
        Storylet.OnHover -= storyletHovered;
    }



    void Start() {
        audioSrc = gameObject.GetComponent<AudioSource>();
        // if (OnTapeEnd != null) OnTapeEnd();
        running = StartCoroutine(playTapes());
    }



    void Update() {
        //DEBUG
        // if (Input.GetMouseButtonDown(1)) {
        //     audioSrc.Stop();
        //     if (OnTapeEnd != null) OnTapeEnd(  !StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet)  );
        //     play(StoryEngine.currentStorylet.tape);
        // }

        // if (!audioSrc.isPlaying) {
        //     // StartCoroutine(nearTapeEnd(StoryEngine.currentStorylet.tape.length));
        //     if (OnTapeEnd != null) OnTapeEnd(  !StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet)  );
        //     play(StoryEngine.currentStorylet.tape);
        // }
    }



    private IEnumerator playTapes() {
        while (true) {
            if (OnTapeEnd != null) OnTapeEnd(  !StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet)  );
            
            play(StoryEngine.currentStorylet.tape);
            
            yield return new WaitForSeconds(StoryEngine.currentStorylet.tape.length-0.05f);
            
        }
    }



    public void play(AudioClip tape) {
        audioSrc.PlayOneShot(tape, volume);
    }



    public bool isPlaying() {return audioSrc.isPlaying;}



    public void storyletHovered(Storylet storylet) {
        time = audioSrc.time;
        StopCoroutine(running);
        running = StartCoroutine(playTapes());
        audioSrc.time = time;
    }
}
