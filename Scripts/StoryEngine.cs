using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    [SerializeField]
    public int level = 0;
    private TapePlayer tapePlayer;
    //private Text stateText;

    public static List<Flags> storyState;
    public static Storylet currentStorylet; 
    public static Storylet selectedStorylet;



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));

        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();

        storyState = new List<Flags>();
    }


    public void selectNextStorylet() {StoryEngine.selectedStorylet = sCollection.randAvailableStorylet().GetComponent<Storylet>();}



    public void sculpt() {
        level = selectedStorylet.level;
        
        Effects effects = selectedStorylet.effects;

        storyState.AddRange(effects.toAdd);
        storyState = storyState.Distinct().ToList();

        effects.toRemove.ForEach(f => storyState.Remove(f));

        sCollection.flagAvailableStorylets(level, storyState);

        StoryEngine.selectedStorylet.available = true;


        StoryEngine.currentStorylet = StoryEngine.selectedStorylet;
    } 

}