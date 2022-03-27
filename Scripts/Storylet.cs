using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storylet : MonoBehaviour
{
    public int condition;
    public MIDIPlayer tape;
    public SpriteRenderer spriteRenderer;
    public Sprite availableSprite;
    private Text text;
    public bool available {get; set;}
    public TapePlayer tapePlayer;



    // Start is called before the first frame update
    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        tape = gameObject.GetComponent<MIDIPlayer>();
        text = gameObject.GetComponentInChildren<Text>();
        text.text = condition.ToString();
        available = false;
    }



    // Update is called once per frame
    void Update()
    {
        available = false;
    }
    


    public void play() {
        tape.play();
    }



    public void changeSprite() {
        spriteRenderer.sprite = availableSprite;
    }



    public void OnMouseDown() {
        // //tapePlayer.insert(tape);
        // if (available) play();

        // Create the synthesizer.
        // var sampleRate = 44100;
        // var synthesizer = new Synthesizer("TimGM6mb.sf2", sampleRate);

        // // Read the MIDI file.
        // var midiFile = new MidiFile("flourish.mid");
        // var sequencer = new MidiFileSequencer(synthesizer);
        // sequencer.Play(midiFile, false);

        // // The output buffer.
        // var left = new float[(int)(sampleRate * midiFile.Length.TotalSeconds)];
        // var right = new float[(int)(sampleRate * midiFile.Length.TotalSeconds)];

        // // Render the waveform.
        // sequencer.Render(left, right);
    }
}
