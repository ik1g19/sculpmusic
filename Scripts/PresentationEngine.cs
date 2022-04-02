using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
//using Mathf;

public class PresentationEngine : MonoBehaviour
{
    StoryCollection sCollection;

    public static bool circleInterface;
    public bool setCircleInterface;

    public static bool initial = true;

    public float RADIUS;

    public delegate void MoveCamera();
    public static event MoveCamera PanCamera;

    public delegate void Timer();
    public static event Timer StartTimer;



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
        circleInterface = setCircleInterface;
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
        Debug.Log(StoryEngine.currentStorylet.text);
        if (  circleInterface && (changeStorylet || initial)  ) tapeEndUpdateInterfaceCircle();

        else if (changeStorylet || initial) tapeEndUpdateInterface();

        if (initial) initial = false;
    }



    private void tapeEndUpdateInterface() {

        List<GameObject> available = sCollection.getAvailableStorylets();

        foreach (GameObject s in sCollection.list()) s.GetComponent<LineRenderer>().enabled = false;
        drawLines(available, StoryEngine.currentStorylet.gameObject);

        sCollection.scriptList().ForEach(  s => s.spriteToDefault()  );

        available.ForEach(  s => s.GetComponent<Storylet>().spriteToAvailable()  );

        StoryEngine.currentStorylet.spriteToCurrent();

    }



    private void tapeEndUpdateInterfaceCircle() {

        List<GameObject> available = sCollection.getAvailableStorylets();

        arrangeArround(available, StoryEngine.currentStorylet.gameObject, RADIUS);

        foreach (GameObject s in sCollection.list()) s.GetComponent<LineRenderer>().enabled = false;
        drawLines(available, StoryEngine.currentStorylet.gameObject);
    

        sCollection.list().ForEach(  s => s.SetActive(false)  );

        available.ForEach(  s => {s.SetActive(true); s.GetComponent<Storylet>().spriteToAvailable();}  );

        StoryEngine.currentStorylet.spriteToCurrent();

        if (PanCamera != null && !initial) PanCamera();

        if (StartTimer != null && !initial) StartTimer();

    }



    private void drawLines(List<GameObject> collection, GameObject centre) {
        List<GameObject> withoutCentre = collection.Where(  s => !s.Equals(centre)  ).ToList();

        foreach (GameObject s in withoutCentre) {
            LineRenderer lr = s.GetComponent<LineRenderer>();
            lr.enabled = true;
            lr.SetPosition(0, s.transform.position);
            lr.SetPosition(1, StoryEngine.currentStorylet.gameObject.transform.position);
        }
    }



    private void arrangeArround(List<GameObject> available, GameObject centre, float radius) {
        List<GameObject> withoutCentre = available.Where(  s => !s.Equals(centre)  ).ToList();
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
}
