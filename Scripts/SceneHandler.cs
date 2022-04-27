using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{
    public void pauseThenLoadScene(float toWait, string scene) { StartCoroutine(pauseThenLoad(toWait, scene)); }

    IEnumerator pauseThenLoad(float toWait, string scene) {yield return new WaitForSeconds(toWait); SceneManager.LoadScene(scene);}
}
