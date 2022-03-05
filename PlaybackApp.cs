using System;
using System.Linq;
using System.Threading;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Multimedia;
using UnityEngine;


class PlaybackApp : MonoBehaviour
{
    private static Playback _playback;



    // Start is called before the first frame update
    void Start()
    {
        var midiFile = MidiFile.Read("Assets\\A.mid");

        var outputDevice = OutputDevice.GetByName("Microsoft GS Wavetable Synth");

        _playback = midiFile.GetPlayback(outputDevice);
        _playback.NotesPlaybackStarted += OnNotesPlaybackStarted;
        _playback.Start();

        SpinWait.SpinUntil(() => !_playback.IsRunning);

        Console.WriteLine("Playback stopped or finished.");

        outputDevice.Dispose();
        _playback.Dispose();
    }



    // Update is called once per frame
    void Update()
    {

    }



    private static void OnNotesPlaybackStarted(object sender, NotesEventArgs e)
    {
        if (e.Notes.Any(n => n.NoteName == Melanchall.DryWetMidi.MusicTheory.NoteName.B))
            _playback.Stop();
    }
}
