using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Storylet : MonoBehaviour
{
    public SpriteRenderer sprtRenderer;
    public Sprite defaultSprt;
    public Sprite availableSprt;
    public Sprite currentSprt;
    public Sprite selectedSprt;
    public string text;
    public int level;
    public bool useLevels = true;

    public bool available {get; set;}
    public bool startingStorylet;

    private TapePlayer tapePlayer;
    public AudioClip tape;

    public bool orGuard2;
    public List<Flags> guard;
    public List<Flags> secondaryGuard;
    public List<Flags> toAdd;
    public List<Flags> toRemove;

    public delegate void ClickedStorylet(Storylet storylet);
    public static event ClickedStorylet OnClick;

    public delegate void StartHover(Storylet storylet);
    public static event StartHover HoverEnter;

    public delegate void CancelHover();
    public static event CancelHover HoverExit;


    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        
        //text = gameObject.GetComponentInChildren<Text>();
        //text.text = condition.ToString();
        //available = false;

        // guard = gameObject.GetComponent<StoryletGuard>();
        // effects = gameObject.GetComponent<StoryletEffects>();

        if (startingStorylet) {
            StoryEngine.selectedStorylet = this;
            StoryEngine.currentStorylet = this;
            if (OnClick != null) OnClick(this);
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
        if (available) {
            if (OnClick != null) OnClick(this);
        }
    }



    private void OnMouseEnter() {
        if (available) {
            if (HoverEnter != null) HoverEnter(this);
        }
    }



    private void OnMouseExit() {
        if (available) {
            if (HoverExit != null) HoverExit();
        }
    }



    public bool checkAvailable(int level, List<Flags> state) {
        bool guard1 =  guard.Select(  f => state.Contains(f)  ).ToList()

                                 .Aggregate(  true, (fold, next) => fold && next  );

        if (useLevels) guard1 = guard1 && (level + 1 == this.level);
        
        if (orGuard2) {
            bool guard2 =  secondaryGuard.Select(  f => state.Contains(f)  ).ToList()

                                        .Aggregate(  true, (fold, next) => fold && next  );

            if (useLevels) guard2 = guard2 && (level + 1 == this.level) ;

            return guard1 || guard2;
        }

        return guard1;
    }
}
