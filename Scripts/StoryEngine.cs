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

    public delegate void TapeFinishedSE(bool changeStorylet);
    public static event TapeFinishedSE OnTapeEndSE;



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));

        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
    }



    void OnEnable()
    {
        TapePlayer.OnTapeEnd += tapeFinished;
    }



    void OnDisable()
    {
        TapePlayer.OnTapeEnd -= tapeFinished;
    }



    public void tapeFinished(bool changeStorylet) {
        if (  PresentationEngine.circleInterface && !PresentationEngine.initial && !changeStorylet  ) {
            StoryEngine.selectedStorylet = sCollection.randAvailableStorylet().GetComponent<Storylet>();
            changeStorylet = true;
        }

        // if (changeStorylet) {
        //     if (selectedStorylet.upVisit) level++;
        //     else level--;

        //     selectedStorylet.upVisit = !selectedStorylet.upVisit;
        // }
        if (changeStorylet) level = selectedStorylet.level;

        state.AddRange(selectedStorylet.toAdd);
        state = state.Distinct().ToList();

        selectedStorylet.toRemove.ForEach(f => state.Remove(f));

        sCollection.flagAvailableStorylets(level, state);

        StoryEngine.selectedStorylet.available = true;

        if (OnTapeEndSE != null) OnTapeEndSE(changeStorylet);
    } 

}