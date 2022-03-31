using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private AudioSource audioSrc;
    private float volume = 1.0f;

    public delegate void TapeFinished(bool changeStorylet);
    public static event TapeFinished OnTapeEnd;



    void Start() {
        audioSrc = gameObject.GetComponent<AudioSource>();
        // if (OnTapeEnd != null) OnTapeEnd();
    }



    void Update() {
        if (!audioSrc.isPlaying) {
            // StartCoroutine(nearTapeEnd(StoryEngine.currentStorylet.tape.length));
            if (OnTapeEnd != null) OnTapeEnd(  !StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet)  );
            play(StoryEngine.currentStorylet.tape);
        }
    }



    public void play(AudioClip tape) {
        audioSrc.PlayOneShot(tape, volume);
    }



    public bool isPlaying() {return audioSrc.isPlaying;}
}
