using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storylet : MonoBehaviour
{
    public SpriteRenderer sprtRenderer;
    public Sprite defaultSprt;
    public Sprite availableSprt;
    public Sprite currentSprt;
    public Sprite selectedSprt;
    private Text text;

    public bool available {get; set;}
    public bool startingStorylet;

    private TapePlayer tapePlayer;
    public AudioClip tape;

    public StoryletGuard guard {get; set;}

    public delegate void ClickedStorylet(Storylet storylet);
    public static event ClickedStorylet OnClick;



    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        
        text = gameObject.GetComponentInChildren<Text>();
        //text.text = condition.ToString();
        available = false;

        guard = gameObject.GetComponent<StoryletGuard>();

        if (startingStorylet) {
            StoryEngine.currentStorylet = this;
            if (OnClick != null) OnClick(this);
        }
    }



    // void Update()
    // {
    //     available = false;
    //     sprtRenderer.sprite = defaultSprt;
    // }



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
}
