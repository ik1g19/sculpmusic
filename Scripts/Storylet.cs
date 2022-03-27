using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storylet : MonoBehaviour
{
    public int condition;
    
    public SpriteRenderer spriteRenderer;
    public Sprite availableSprite;
    private Text text;

    public bool available {get; set;}

    private TapePlayer tapePlayer;
    public AudioClip tape;



    // Start is called before the first frame update
    void Start()
    {
        tapePlayer = GameObject.FindWithTag("TapePlayer").GetComponent<TapePlayer>();
        
        text = gameObject.GetComponentInChildren<Text>();
        text.text = condition.ToString();
        available = false;
    }



    // Update is called once per frame
    void Update()
    {
        available = false;
    }



    public void changeSprite() {
        spriteRenderer.sprite = availableSprite;
    }



    public AudioClip getTape() {return tape;}
}
