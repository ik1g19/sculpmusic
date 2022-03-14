using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PresentationEngine : MonoBehaviour
{
    private StoryCollection storylets;



    // Start is called before the first frame update
    void Start()
    {
        storylets = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));        
    }



    // Update is called once per frame
    void Update()
    {
        storylets.list().Select(sObj => sObj.GetComponent<Storylet>()).ToList()
                        .Where(s => s.available).ToList()
                        .ForEach(s => s.changeSprite());
    }
}
