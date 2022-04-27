using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class TapePlayer : MonoBehaviour
{
    private AudioSource audioSrc;
    private float srcVolumeStart;

    private AudioSource demoAudioSrc;
    private float demoVolumeStart;

    private float timeHovered;
    private float timeNotHovered;
    public float fadeDuration = 3.0f;
    private Storylet lastHovered;

    public UnityEvent onTapeChange;

    private Coroutine running;
    private float time;

    private Coroutine fadeDemoIn;
    private Coroutine fadeToReset;



    void Start() {
        AudioSource[] srcs;
        srcs = gameObject.GetComponents<AudioSource>();
        audioSrc = srcs[0];
        demoAudioSrc = srcs[1];
        running = StartCoroutine(playTapes());
        srcVolumeStart = audioSrc.volume;
        StartCoroutine(fadeSrcIn());
    }



    private IEnumerator playTapes() {
        while (true) {
            if (!StoryEngine.currentStorylet.Equals(StoryEngine.selectedStorylet)) onTapeChange.Invoke();
            else if (PresentationEngine.initial) onTapeChange.Invoke();

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
        if (fadeToReset != null) StopCoroutine(fadeToReset);

        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeHovered = 0.0f;

        play(demoAudioSrc, storylet.tape);
        demoAudioSrc.time = audioSrc.time;

        fadeDemoIn = StartCoroutine(fadeDemo());

        lastHovered = storylet;
    }



    IEnumerator fadeDemo() {
        while (timeHovered < fadeDuration) {
            float t = timeHovered / fadeDuration;

            t = t * t * (3f - 2f * t);
            demoAudioSrc.volume = Mathf.Lerp(demoVolumeStart, 1, t);
            audioSrc.volume = 1.0f - demoAudioSrc.volume;

            timeHovered += Time.deltaTime;

            yield return null;
        }

        demoAudioSrc.volume = 1.0f;
        audioSrc.volume = 0.0f;
    }



    public void storyletHoverExit() {
        StopCoroutine(fadeDemoIn);

        demoVolumeStart = demoAudioSrc.volume;
        srcVolumeStart = audioSrc.volume;

        timeNotHovered = 0.0f;


        fadeToReset = StartCoroutine(fadeReset());
    }



    IEnumerator fadeReset() {
        while (timeNotHovered < fadeDuration) {
            float t = timeNotHovered / fadeDuration;

            t = t * t * (3f - 2f * t);
            demoAudioSrc.volume = Mathf.Lerp(demoVolumeStart, 0, t);
            audioSrc.volume = 1.0f - demoAudioSrc.volume;

            timeNotHovered += Time.deltaTime;

            yield return null;
        }

        demoAudioSrc.volume = 0.0f;
        audioSrc.volume = 1.0f;
    }



    IEnumerator fadeSrcIn() {
        float timeElapsed = 0f;

        while (timeElapsed < fadeDuration) {
            float t = timeElapsed / fadeDuration;

            t = t * t * (3f - 2f * t);
            audioSrc.volume = Mathf.Lerp(srcVolumeStart, 1, t);

            timeElapsed += Time.deltaTime;

            yield return null;
        }

        audioSrc.volume = 1.0f;
    }
}
