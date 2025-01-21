using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Script_GameOver : MonoBehaviour
{
    public string SceneName;
    public float waitTime;

    private void Start()
    {
        StartCoroutine(loadToScene());
    }
    IEnumerator loadToScene()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene(SceneName);
    }
}
