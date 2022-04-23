using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private AudioSource audioSrc;
    private float srcVolumeStart;

    private AudioSource demoAudioSrc;
    private float demoVolumeStart;

    private float timeHovered;
    private float timeNotHovered;
    private bool hovering = false;
    public float fadeDuration = 3.0f;
    private Storylet lastHovered;

    public delegate void TapeFinished(bool changeStorylet);
    public static event TapeFinished OnTapeEnd;

    private Coroutine running;
    private float time;



    void OnEnable()
    {
        Storylet.HoverEnter += storyletHoverEnter;
        Storylet.HoverExit += storyletHoverExit;
    }



    void OnDisable()
    {
        Storylet.HoverEnter -= storyletHoverEnter;
        Storylet.HoverExit -= storyletHoverExit;
    }



    void Start() {
        AudioSource[] srcs;
        srcs = gameObject.GetComponents<AudioSource>();
        audioSrc = srcs[0];
        demoAudioSrc = srcs[1];
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

            if (audioSrc.isPlaying) audioSrc.Stop();
            if (demoAudioSrc.isPlaying) demoAudioSrc.Stop();
            
            play(audioSrc, StoryEngine.currentStorylet.tape);
            if (lastHovered != null) play(demoAudioSrc, lastHovered.tape);
            
            yield return new WaitForSeconds(StoryEngine.currentStorylet.tape.length-0.05f);
            
        }
    }



    public void play(AudioSource src, AudioClip tape) {
        src.clip = tape;
        src.Play();
        src.time = 0.0f;
    }



    public bool isPlaying() {return audioSrc.isPlaying;}



    public void storyletHoverEnter(Storylet storylet) {
        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeHovered = 0.0f;
        hovering = true;

        play(demoAudioSrc, storylet.tape);
        demoAudioSrc.time = audioSrc.time;

        StartCoroutine(fadeDemo());

        lastHovered = storylet;
    }



    IEnumerator fadeDemo() {
        while (timeHovered < fadeDuration) {
            if (hovering) {
                float t = timeHovered / fadeDuration;

                t = t * t * (3f - 2f * t);
                demoAudioSrc.volume = Mathf.Lerp(demoVolumeStart, 1, t);
                audioSrc.volume = 1.0f - demoAudioSrc.volume;

                timeHovered += Time.deltaTime;

                yield return null;
            }
            else yield break;
        }

        demoAudioSrc.volume = 1.0f;
        audioSrc.volume = 0.0f;
    }



    public void storyletHoverExit() {
        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeNotHovered = 0.0f;
        hovering = false;


        StartCoroutine(fadeReset());
    }



    IEnumerator fadeReset() {
        while (timeNotHovered < fadeDuration) {
            if (!hovering) {
                float t = timeNotHovered / fadeDuration;

                t = t * t * (3f - 2f * t);
                demoAudioSrc.volume = Mathf.Lerp(demoVolumeStart, 0, t);
                audioSrc.volume = 1.0f - demoAudioSrc.volume;

                timeNotHovered += Time.deltaTime;

                yield return null;
            }
            else yield break;
        }

        demoAudioSrc.volume = 0.0f;
        audioSrc.volume = 1.0f;
    }
}
