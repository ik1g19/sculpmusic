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
    public SpriteRenderer sprtRenderer;
    public Sprite defaultSprt;
    public Sprite availableSprt;
    public Sprite currentSprt;
    public Sprite selectedSprt;
    public string text;
    public int level;
    public List<int> permittedLevles;
    public bool useLevels = true;

    public bool available {get; set;}
    public bool startingStorylet;

    private TapePlayer tapePlayer;
    public AudioClip tape;

    private PresentationEngine presentationEngine;

    private InteractionEngine interactionEngine;

    public bool orGuard2;
    public List<Flags> guard;
    public List<Flags> secondaryGuard;
    public List<Flags> toAdd;
    public List<Flags> toRemove;

    public StoryletEvent onStoryletClick;
    public StoryletEvent onHoverEnter;
    public UnityEvent onHoverExit;


    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        presentationEngine = GameObject.FindWithTag("PresentationEngine").GetComponent<PresentationEngine>();
        interactionEngine = GameObject.FindWithTag("InteractionEngine").GetComponent<InteractionEngine>();

        onStoryletClick.AddListener(interactionEngine.updateSelectedStorylet);
        onStoryletClick.AddListener(presentationEngine.storyletClicked);

        onHoverEnter.AddListener(presentationEngine.storyletHoverEnter);
        onHoverEnter.AddListener(tapePlayer.storyletHoverEnter);

        onHoverExit.AddListener(presentationEngine.storyletHoverExit);
        onHoverExit.AddListener(tapePlayer.storyletHoverExit);

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
        bool guard1 =  guard.Select(  f => state.Contains(f)  ).ToList()

                            .Aggregate(  true, (fold, next) => fold && next  );

        if (useLevels) guard1 = guard1 && permittedLevles.Contains(level);
        
        if (orGuard2) {
            bool guard2 =  secondaryGuard.Select(  f => state.Contains(f)  ).ToList()

                                         .Aggregate(  true, (fold, next) => fold && next  );

            if (useLevels) guard2 = guard2 && permittedLevles.Contains(level);

            return guard1 || guard2;
        }

        return guard1;
    }
}
