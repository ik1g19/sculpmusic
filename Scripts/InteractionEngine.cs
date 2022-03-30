using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionEngine : MonoBehaviour
{
    private StoryCollection storylets;



    void OnEnable()
    {
        Storylet.OnClick += storyletClicked;
        TapePlayer.OnTapeEnd += tapeFinished;
    }



    void OnDisable()
    {
        Storylet.OnClick -= storyletClicked;
        TapePlayer.OnTapeEnd -= tapeFinished;
    }



    // // Update is called once per frame
    // void Update()
    // {
    //     storylets.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
    //                     .ForEach(s => {
    //                         if (s.clicked) s.setCurrent();
    //                     });
    // }



    public void storyletClicked(Storylet storylet) {
        StoryEngine.selectedStorylet = storylet;
    }



    public void tapeFinished() {
        StoryEngine.currentStorylet = StoryEngine.selectedStorylet;
    }
}
