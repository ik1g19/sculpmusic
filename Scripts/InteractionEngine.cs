using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class InteractionEngine : MonoBehaviour
{
    private StoryCollection sCollection;



    void Start() {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));
    }



    // // Update is called once per frame
    // void Update()
    // {
    //     sCollection.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
    //                     .ForEach(s => {
    //                         if (s.clicked) s.setCurrent();
    //                     });
    // }



    public void updateSelectedStorylet(Storylet storylet) {
        StoryEngine.selectedStorylet = storylet;
    }
}
