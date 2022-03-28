using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StoryCollection
{
    List<GameObject> storylets;



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



    // public StoryCollection availableStorylets(StoryState state) {
    //     StoryCollection available = new StoryCollection();

    //     foreach(GameObject s in storylets) {
    //         Storylet storylet = s.GetComponent<Storylet>();
    //         if (storylet.condition <= state.state) available.add(s);
    //     }
    //     //Debug.Log(available.storylets.Count);
    //     return available;
    // }



    public List<GameObject> availableStorylets(StoryState state) {
        return storylets.Select(s =>  new {sGObj = s, sObj = s.GetComponent<Storylet>()}  ).AsEnumerable()

                        .Where(  sPair => checkAvailable(sPair.sObj.guard, state)  ).ToList()

                        .Select(sPair => sPair.sGObj).ToList();
    }



    private bool checkAvailable(StoryletGuard storyletGuard, StoryState storyState) {
        return true;
    }



    public List<GameObject> list() {return storylets;}
}