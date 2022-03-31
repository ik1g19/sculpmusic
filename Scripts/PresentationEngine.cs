using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using Mathf;

public class PresentationEngine : MonoBehaviour
{
    StoryCollection sCollection;
    public bool circleInterface;

    private bool initial = true;

    public float RADIUS;



    void OnEnable()
    {
        InteractionEngine.OnClickIE += storyletClicked;
        InteractionEngine.OnTapeEndIE += tapeFinished;
    }



    void OnDisable()
    {
        InteractionEngine.OnClickIE -= storyletClicked;
        InteractionEngine.OnTapeEndIE -= tapeFinished;
    }



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));
    }



    public void storyletClicked(Storylet storylet) {
        if (!circleInterface) {
            Storylet selected = StoryEngine.selectedStorylet;

            if (selected != null) {
                if (selected.available) selected.spriteToAvailable();
                else selected.spriteToDefault();
            }

            storylet.spriteToSelected();
        }


        else {
            //TO RPELACE
            Storylet selected = StoryEngine.selectedStorylet;

            if (selected != null) {
                if (selected.available) selected.spriteToAvailable();
                else selected.spriteToDefault();
            }

            storylet.spriteToSelected();
        }
    }



    public void tapeFinished(bool changeStorylet) {
        if (!circleInterface) {
            sCollection.scriptList().ForEach(  s => s.spriteToDefault()  );

            sCollection.scriptList().Where(  s => s.available  ).ToList()
                                    .ForEach(  s => s.spriteToAvailable()  );

            StoryEngine.currentStorylet.spriteToCurrent();
        }


        else {
            if (changeStorylet || initial) {
                arrangeArround(availableStorylets(sCollection), StoryEngine.currentStorylet.gameObject, RADIUS);
                if (initial) initial = false;
            }

            sCollection.list().ForEach(  s => s.SetActive(false)  );

            sCollection.scriptList().Where(  s => s.available  ).ToList()
                                    .ForEach(  s => {s.gameObject.SetActive(true); s.spriteToAvailable();}  );

            StoryEngine.currentStorylet.spriteToCurrent();
        }
    }



    private void arrangeArround(List<GameObject> collection, GameObject centre, float radius) {
        List<GameObject> withoutCentre = collection.Where(  s => !s.Equals(centre)  ).ToList();
        arrangeCircle(withoutCentre, centre.transform.position, radius);
    }



    public void arrangeCircle (List<GameObject> toArrange, Vector2 point, float radius){
        int num = toArrange.Count;
        int i = 0;


        foreach (GameObject s in toArrange){
            //Debug.Log("test");
            /* Distance around the circle */  
            var radians = 2 * Mathf.PI / num * i;
            
            /* Get the vector direction */ 
            var vertical = Mathf.Sin(radians);
            var horizontal = Mathf.Cos(radians); 
            
            var spawnDir = new Vector2 (horizontal, vertical);
            
            /* Get the spawn position */ 
            var spawnPos = point + spawnDir * radius; // Radius is just the distance away from the point
            
            /* Now spawn */
            //var enemy = Instantiate (enemyPefab, spawnPos, Qaurtenion.Identity) as GameObject;
            s.transform.position = spawnPos;

            i++;
        }
    }



    private List<GameObject> availableStorylets(StoryCollection sColl) {
        //Debug.Log(sColl.list().Where(  s => s.GetComponent<Storylet>().available  ).ToList().Count);
        foreach(GameObject s in sColl.list()){Debug.Log(s.GetComponent<Storylet>().available);}
        return sColl.list().Where(  s => s.GetComponent<Storylet>().available  ).ToList();
    }
}
