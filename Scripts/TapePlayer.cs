using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private AudioSource audioSrc;
    private float volume = 1.0f;



    void Start() {
        audioSrc = gameObject.GetComponent<AudioSource>();
    }



    void Update() {
        
    }



    public void play(AudioClip tape) {
        audioSrc.PlayOneShot(tape, volume);
    }



    public bool isPlaying() {return audioSrc.isPlaying;}
}
