using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storylet : MonoBehaviour
{
    public int condition;



    // Start is called before the first frame update
    void Start()
    {
        
    }



    // Update is called once per frame
    void Update()
    {

    }
    


    public void play() {
        MIDIPlayer player =  gameObject.GetComponent<MIDIPlayer>();
        player.play();
    }



    void test() {
        Debug.Log("test");
    }
}
