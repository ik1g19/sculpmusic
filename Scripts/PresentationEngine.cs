using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

[Serializable]
public class Vector3Event : UnityEvent <Vector3> { }

[Serializable]
public class FloatEvent : UnityEvent <float,float> { }

public class PresentationEngine : MonoBehaviour
{
    public bool textOnHover;

    StoryCollection sCollection;

    public float RADIUS;

    public Text text;

    public Vector3Event cameraPan;
    public FloatEvent cameraZoom;



    // Start is called before the first frame update
    void Start()
    {
        sCollection = new StoryCollection(GameObject.FindGameObjectsWithTag("Storylet"));
        cameraZoom.Invoke(1,250);
    }



    public void storyletHoverEnter(Storylet storylet) {
        //string elements = "\nFeatures:\n";
        //foreach (Flags f in storylet.effects.toAdd) {elements += FlagHandling.labelMaker(f);}
        if (textOnHover) text.text = storylet.description;
        //text.text += elements;
        storylet.gameObject.GetComponent<Animation>().smoothScale(107f, 0.5f);
    }



    public void storyletHoverExit(Storylet storylet) {
        if (textOnHover) text.text = "";
        storylet.gameObject.GetComponent<Animation>().smoothScale(97f, 0.5f);
    }



    public void storyletClicked(Storylet storylet) {
        Storylet selected = StoryEngine.selectedStorylet;

        if (selected != null) {
            if (selected.available) selected.spriteToAvailable();
            else selected.spriteToDefault();
        }

        storylet.spriteToSelected();
    }



    public void updatePresentation() {
        tapeEndUpdateInterfaceCircle();

        cameraPan.Invoke(StoryEngine.currentStorylet.transform.position);
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
