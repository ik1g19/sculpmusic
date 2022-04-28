using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Events;
using System;

[Serializable]
public class StoryletEvent : UnityEvent <Storylet> { }

public class Storylet : MonoBehaviour
{
    public bool textOnHover;
    public bool demoOnHover;

    public SpriteRenderer sprtRenderer;
    public Sprite defaultSprt;
    public Sprite availableSprt;
    public Sprite currentSprt;
    public Sprite selectedSprt;
    public string text;

    public bool available {get; set;}
    public bool startingStorylet;

    private TapePlayer tapePlayer;
    public AudioClip tape;

    private PresentationEngine presentationEngine;

    private InteractionEngine interactionEngine;

    
    public int level;
    public List<int> permittedLevels;

    public List<Guard> guards;
    public Effects effects;

    [HideInInspector]
    public StoryletEvent onStoryletClick;
    [HideInInspector]
    public StoryletEvent onHoverEnter;
    [HideInInspector]
    public UnityEvent onHoverExit;


    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        presentationEngine = GameObject.FindWithTag("PresentationEngine").GetComponent<PresentationEngine>();
        interactionEngine = GameObject.FindWithTag("InteractionEngine").GetComponent<InteractionEngine>();

        onStoryletClick.AddListener(interactionEngine.updateSelectedStorylet);
        onStoryletClick.AddListener(presentationEngine.storyletClicked);

        if (textOnHover) onHoverEnter.AddListener(presentationEngine.storyletHoverEnter);
        if (demoOnHover) onHoverEnter.AddListener(tapePlayer.storyletHoverEnter);

        if (textOnHover) onHoverExit.AddListener(presentationEngine.storyletHoverExit);
        if (demoOnHover) onHoverExit.AddListener(tapePlayer.storyletHoverExit);

        if (startingStorylet) {
            StoryEngine.selectedStorylet = this;
            StoryEngine.currentStorylet = this;
            onStoryletClick.Invoke(this);
        }
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
        if (available) onHoverExit.Invoke();
    }



    public bool checkAvailable(int level, List<Flags> state) {
        if (guards.Count == 0) return permittedLevels.Contains(level);

        bool avlbl = false;

        foreach (Guard guard in guards) {
            bool satisfied =  guard.isSatisfied(state);

            //Debug.Log(satisfied);
            satisfied = satisfied && permittedLevels.Contains(level);
            //Debug.Log(permittedLevels.Contains(level));

            avlbl = avlbl || satisfied;
        }

        return avlbl;
    }
}
