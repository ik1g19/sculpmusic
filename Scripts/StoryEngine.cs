using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    private StoryState state;
    private TapePlayer tapePlayer;
    private Text stateText;



    // Start is called before the first frame update
    void Start()
    {
        state = new StoryState();
        state.state = 0;

        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));

        stateText = GameObject.FindWithTag("StateText").GetComponent<Text>();
        stateText.text = "State: " + state.state.ToString();

        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
    }



    // Update is called once per frame
    void Update()
    {
        sCollection.availableStorylets(state).Select(s => s.GetComponent<Storylet>()).ToList().
                                              ForEach(s => s.available = true);
        if (Input.GetMouseButtonDown(1)) {state.state++; stateText.text = "State: " + state.state.ToString();}
    }
}