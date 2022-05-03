using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System;
using System.Xml;

[Serializable]
public class StoryletEvent : UnityEvent <Storylet> { }

public class Storylet : MonoBehaviour
{
    public bool demoOnHover;

    public SpriteRenderer sprtRenderer;
    public Sprite defaultSprt;
    public Sprite availableSprt;
    public Sprite currentSprt;
    public Sprite selectedSprt;

    public string description;

    public bool available {get; set;}
    public bool startingStorylet;

    private TapePlayer tapePlayer;
    public AudioClip tape;

    private PresentationEngine presentationEngine;

    private InteractionEngine interactionEngine;

    private SceneHandler sceneHandler;

    
    public int level;
    public List<int> visibleLevels;

    public List<Guard> guards;
    public Effects effects;

    public float hideLineDuration;

    [HideInInspector]
    public StoryletEvent onStoryletClick;
    [HideInInspector]
    public StoryletEvent onHoverEnter;
    [HideInInspector]
    public StoryletEvent onHoverExit;


    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        presentationEngine = GameObject.FindWithTag("PresentationEngine").GetComponent<PresentationEngine>();
        interactionEngine = GameObject.FindWithTag("InteractionEngine").GetComponent<InteractionEngine>();
        sceneHandler = GameObject.FindWithTag("SceneHandler").GetComponent<SceneHandler>();

        onStoryletClick.AddListener(interactionEngine.updateSelectedStorylet);
        onStoryletClick.AddListener(presentationEngine.storyletClicked);

        onHoverEnter.AddListener(presentationEngine.storyletHoverEnter);
        if (demoOnHover) onHoverEnter.AddListener(tapePlayer.storyletHoverEnter);

        onHoverExit.AddListener(presentationEngine.storyletHoverExit);
        if (demoOnHover) onHoverExit.AddListener(tapePlayer.storyletHoverExit);

        sceneHandler.onMenuLoad.AddListener(hide);

        if (startingStorylet) {
            StoryEngine.selectedStorylet = this;
            StoryEngine.currentStorylet = this;
            onStoryletClick.Invoke(this);
        }
        // else {
        //     string path = "StoryletDescriptions/" + gameObject.name;
        //     int guardNum = 0;
        //     foreach (Guard guard in guards) {
        //         string guardfilepath = path + "_" + guardNum;

        //         TextAsset file = (TextAsset)Resources.Load(guardfilepath);
        //         guard.actionDescription = file.text;

        //         guardNum++;
        //     }
        // }
    }



    public void spriteToCurrent() {
        sprtRenderer.sprite = currentSprt;
    }



    public void spriteToAvailable() {
        sprtRenderer.sprite = availableSprt;
    }



    public void spriteToDefault() {
        sprtRenderer.sprite = defaultSprt;
    }



    public void spriteToSelected() {
        sprtRenderer.sprite = selectedSprt;
    }



    public AudioClip getTape() {return tape;}



    private void OnMouseDown() {
        if (available) onStoryletClick.Invoke(this);
    }



    private void OnMouseEnter() {
        if (available) onHoverEnter.Invoke(this);
    }



    private void OnMouseExit() {
        if (available) onHoverExit.Invoke(this);
    }



    public bool checkAvailable(Storylet from, List<Flags> storyState) {
        if (guards.Count == 0) return from.visibleLevels.Contains(level);

        bool avlbl = false;

        //int guardNum = 0;
        foreach (Guard guard in guards) {
            bool satisfied =  guard.isSatisfied(storyState);

            //Debug.Log(satisfied);
            satisfied = satisfied && from.visibleLevels.Contains(level);
            //Debug.Log(permittedLevels.Contains(level));
            //if (gameObject.name.Equals("introdistortion")) Debug.Log(from.gameObject.name);
            //if (satisfied && description.Equals("")) description = guard.actionDescription;

            avlbl = avlbl || satisfied;

            //guardNum++;
        }

        return avlbl;
    }



    public void hide() {
        onHoverEnter.RemoveAllListeners();
        onHoverExit.RemoveAllListeners();
        onStoryletClick.RemoveAllListeners();

        if (available) {
            LineRenderer lr = GetComponent<LineRenderer>();
            StartCoroutine(Animation.smoothStep(  (x) => lr.SetPosition(1, x), lr.GetPosition(1), transform.position, hideLineDuration  ));
        }
    }
}
