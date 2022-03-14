using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapePlayer : MonoBehaviour
{
    private Queue<GameObject> tapes;



    void Start() {

    }



    void Update() {
        if (getScript(tapes.Peek()).player.hasFinished()) {
            tapes.Dequeue();
            play();
        }
    }



    public void play() {
        getScript(tapes.Peek()).play();
    }



    public void insert(GameObject storylet) {
        tapes.Enqueue(storylet);
    }



    private Storylet getScript(GameObject obj) {
        return obj.GetComponent<Storylet>();
    }
}
