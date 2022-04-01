using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    private List<Flags> state;
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
        state = selectedStorylet.effects;

        sCollection.scriptList().ForEach(  s => s.available = false  );

        List<GameObject> available = sCollection.availableStorylets(state);
        //Debug.Log(available.Count);
        
        available.Select(  s => s.GetComponent<Storylet>()  ).ToList().
                  ForEach(  s => s.available = true  );

        StoryEngine.selectedStorylet.available = true;

        if (OnTapeEndSE != null) OnTapeEndSE(changeStorylet);
    } 

}