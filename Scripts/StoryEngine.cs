using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    private List<Properties> state;
    private TapePlayer tapePlayer;
    //private Text stateText;

    public static Storylet currentStorylet; 
    public static Storylet selectedStorylet;



    // Start is called before the first frame update
    void Start()
    {
        //state = gameObject.GetComponent<StoryState>();

        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));

        // stateText = GameObject.FindWithTag("StateText").GetComponent<Text>();
        // stateText.text = "State: " + state.bar.ToString();

        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
    }



    // // Update is called once per frame
    // void Update()
    // {
    //     if (!tapePlayer.isPlaying()) {
    //         // List<GameObject> available = sCollection.availableStorylets(state);

    //         // available.Select(s => s.GetComponent<Storylet>()).ToList().
    //         //           ForEach(s => s.available = true);

    //         // Storylet randomStorylet = new StoryCollection(available).randStorylet().GetComponent<Storylet>();
    //         // randomStorylet.current = true;

    //         // tapePlayer.play(randomStorylet.getTape());
    //         tapePlayer.play( currentStorylet.getTape() );
    //     }


    //     if (Input.GetMouseButtonDown(1)) {state.state++; stateText.text = "State: " + state.state.ToString();}
    // }



    void OnEnable()
    {
        TapePlayer.OnTapeEnd += tapeFinished;
    }



    void OnDisable()
    {
        TapePlayer.OnTapeEnd -= tapeFinished;
    }



    public void tapeFinished() {
        state = selectedStorylet.effects;

        sCollection.scriptList().ForEach(  s => s.available = false  );

        List<GameObject> available = sCollection.availableStorylets(state);
        
        available.Select(  s => s.GetComponent<Storylet>()  ).ToList().
                  ForEach(  s => s.available = true  );

    } 

}