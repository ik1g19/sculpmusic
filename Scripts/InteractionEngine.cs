using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InteractionEngine : MonoBehaviour
{
    private StoryCollection storylets;

    public delegate void TapeFinishedIE(bool changeStorylet);
    public static event TapeFinishedIE OnTapeEndIE;

    public delegate void ClickedStoryletIE(Storylet storylet);
    public static event ClickedStoryletIE OnClickIE;



    void OnEnable()
    {
        Storylet.OnClick += storyletClicked;
        StoryEngine.OnTapeEndSE += tapeFinished;
    }



    void OnDisable()
    {
        Storylet.OnClick -= storyletClicked;
        StoryEngine.OnTapeEndSE -= tapeFinished;
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
        if (OnClickIE != null) OnClickIE(storylet);
    }



    public void tapeFinished(bool changeStorylet) {
        StoryEngine.currentStorylet = StoryEngine.selectedStorylet;
        if (OnTapeEndIE != null) OnTapeEndIE(changeStorylet);
    }
}
