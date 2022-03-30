using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PresentationEngine : MonoBehaviour
{
    StoryCollection sCollection;



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



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));        
    }



    // // Update is called once per frame
    // void Update()
    // {
    //     // storylets.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
    //     //                 .Where(s => s.available).ToList()
    //     //                 .ForEach(s => s.setAvailable());
    //     storylets.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
    //                     .ForEach(s => {
    //                         if (s.available) s.setAvailable();
    //                         if (s.current) s.setCurrent();
    //                     });
    // }



    public void storyletClicked(Storylet storylet) {
        Storylet selected = StoryEngine.selectedStorylet;

        if (selected != null) {
            if (selected.available) selected.spriteToAvailable();
            else selected.spriteToDefault();
        }

        storylet.spriteToSelected();
    }



    public void tapeFinished() {
        sCollection.scriptList().ForEach(  s => s.spriteToDefault()  );

        sCollection.scriptList().Where(  s => s.available  ).ToList()
                                .ForEach(  s => s.spriteToAvailable()  );

        // sCollection.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
        //                   .Where(s => !s.available).ToList()
        //                   .ForEach(s => s.spriteToDefault());

        StoryEngine.currentStorylet.spriteToCurrent();
    }
}
