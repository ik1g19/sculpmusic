using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storylet : MonoBehaviour
{
    public int condition;
    public MIDIPlayer player;
    public SpriteRenderer spriteRenderer;
    public Sprite availableSprite;
    private Text text;
    public bool available {get; set;}



    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<MIDIPlayer>();
        text = gameObject.GetComponentInChildren<Text>();
        text.text = condition.ToString();
        available = false;
    }



    // Update is called once per frame
    void Update()
    {
        available = false;
    }
    


    public void play() {
        player.play();
    }



    public void changeSprite() {
        spriteRenderer.sprite = availableSprite;
    }
}
