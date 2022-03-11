using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEngine : MonoBehaviour
{
    private GameObject prefab;
    private StoryCollection sCollection;
    private StoryState state;



    // Start is called before the first frame update
    void Start()
    {
        state = new StoryState();
        state.state = 0;

        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) sCollection.availableStorylets(state).randStorylet().GetComponent<Storylet>().play();
        
        if (Input.GetMouseButtonDown(1)) state.state++;
        Debug.Log(state.state);
    }
}