using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryEngine : MonoBehaviour
{
    public GameObject prefab;
    public GameObject[] storyCollection;

    // Start is called before the first frame update
    void Start()
    {
        storyCollection = GameObject.FindGameObjectsWithTag("Storylet");

        Debug.Log(storyCollection.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
}