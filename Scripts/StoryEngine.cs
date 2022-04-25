using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    [SerializeField]
    public List<Flags> state;
    public int level = 0;
    private TapePlayer tapePlayer;
    //private Text stateText;

    public static Storylet currentStorylet; 
    public static Storylet selectedStorylet;



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));

        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
    }



    public void sculpt() {
        if (  PresentationEngine.circleInterface && !PresentationEngine.initial  ) {
            StoryEngine.selectedStorylet = sCollection.randAvailableStorylet().GetComponent<Storylet>();
        }

        level = selectedStorylet.level;

        state.AddRange(selectedStorylet.toAdd);
        state = state.Distinct().ToList();

        selectedStorylet.toRemove.ForEach(f => state.Remove(f));

        sCollection.flagAvailableStorylets(level, state);

        StoryEngine.selectedStorylet.available = true;


        StoryEngine.currentStorylet = StoryEngine.selectedStorylet;
    } 

}