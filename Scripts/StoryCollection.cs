using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryCollection
{
    private List<GameObject> storylets;



    public StoryCollection() {
        storylets = new List<GameObject>();
    }



    public StoryCollection(GameObject[] storylets) {
        this.storylets = storylets.ToList();
    }



    public StoryCollection(List<GameObject> storylets) {
        this.storylets = storylets;
    }



    public void add(GameObject storylet) { storylets.Add(storylet); }



    public GameObject randStorylet() {
        return storylets[Random.Range(0,storylets.Count)];
    }



    public GameObject randAvailableStorylet() {
        List<GameObject> available = getAvailableStorylets().Where(s => !s.GetComponent<Storylet>().Equals(StoryEngine.currentStorylet)).ToList();
        return available[Random.Range(0,available.Count)];
    }



    public void flagAvailableStorylets(int level, List<Flags> state) {
        storylets.ForEach(s => s.GetComponent<Storylet>().available = false);

        storylets.Where(s => s.GetComponent<Storylet>().checkAvailable(level, state)).ToList()
                 .ForEach(s => s.GetComponent<Storylet>().available = true);
    }



    public List<GameObject> getAvailableStorylets() {
        return storylets.Where(s => s.GetComponent<Storylet>().available).ToList();
    }



    public List<GameObject> list() {return storylets;}



    public List<Storylet> scriptList() {return storylets.Select(  s => s.GetComponent<Storylet>()  ).ToList();}
}