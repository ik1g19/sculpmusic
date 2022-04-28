using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TapePlayer : MonoBehaviour
{
    public bool timeSensitive;

    private AudioSource audioSrc;
    private float srcVolumeStart;

    private AudioSource demoAudioSrc;
    private float demoVolumeStart;

    private float timeHovered;
    private float timeNotHovered;
    public float fadeInDuration = 5.0f;
    public float crossFadeDuration = 3.0f;
    private Storylet lastHovered;

    public UnityEvent onTapePlayerStart;
    public UnityEvent onTimeOut;
    public UnityEvent onTapeEnd;

    private Coroutine running;
    private float time;

    private Coroutine fadeDemoIn;
    private Coroutine fadeToReset;



    void Start() {
        AudioSource[] srcs;
        srcs = gameObject.GetComponents<AudioSource>();
        audioSrc = srcs[0];
        demoAudioSrc = srcs[1];

        onTapePlayerStart.Invoke();

        running = StartCoroutine(playTapes());
        srcVolumeStart = audioSrc.volume;
        StartCoroutine(fadeSrcIn());
    }



    private IEnumerator playTapes() {
        while (true) {            
            play(audioSrc, StoryEngine.currentStorylet.tape);
            if (lastHovered != null) play(demoAudioSrc, lastHovered.tape);
            
            yield return new WaitForSeconds(StoryEngine.currentStorylet.tape.length-0.05f);

            if (StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet) && timeSensitive) onTimeOut.Invoke();
            
            onTapeEnd.Invoke();

            if (audioSrc.isPlaying) audioSrc.Stop();
            if (demoAudioSrc.isPlaying) demoAudioSrc.Stop();
        }
    }



    public void play(AudioSource src, AudioClip tape) {
        src.clip = tape;
        src.Play();
        src.time = 0.0f;
    }



    public bool isPlaying() {return audioSrc.isPlaying;}



    public void storyletHoverEnter(Storylet storylet) {
        if (fadeToReset != null) StopCoroutine(fadeToReset);

        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeHovered = 0.0f;

        play(demoAudioSrc, storylet.tape);
        demoAudioSrc.time = audioSrc.time;

        fadeDemoIn = StartCoroutine(  Animation.smoothStep( (x) => {demoAudioSrc.volume = x; 
                                                                    audioSrc.volume = 1f - demoAudioSrc.volume;},
                                                            demoVolumeStart, 1, crossFadeDuration )  );

        lastHovered = storylet;
    }



    public void storyletHoverExit() {
        StopCoroutine(fadeDemoIn);

        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeNotHovered = 0.0f;


        fadeToReset = StartCoroutine(  Animation.smoothStep( (x) => {demoAudioSrc.volume = x; 
                                                                    audioSrc.volume = 1f - demoAudioSrc.volume;},
                                                             demoVolumeStart, 0, crossFadeDuration )  );
    }



    IEnumerator fadeSrcIn() {
        float timeElapsed = 0f;

        while (timeElapsed < fadeInDuration) {
            float t = timeElapsed / fadeInDuration;

            t = t * t * (3f - 2f * t);
            audioSrc.volume = Mathf.Lerp(srcVolumeStart, 1, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioSrc.volume = 1.0f;
    }
}
