using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private Queue<MIDIPlayer> tapes;



    void Start() {
        tapes = new Queue<MIDIPlayer>();
    }



    void Update() {
        if (tapes.Count > 0 && tapes.Peek().hasFinished()) {
            tapes.Dequeue();
            play();
        }
    }



    public void play() {
        tapes.Peek().play();
    }



    public void insert(MIDIPlayer tape) {
        tapes.Enqueue(tape);
    }



    private Storylet getScript(GameObject obj) {
        return obj.GetComponent<Storylet>();
    }
}
