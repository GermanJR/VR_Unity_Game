using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator animator;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadNewScene(string newSceneName)
    {
        StartCoroutine(LoadSceneCorroutine(newSceneName));
    }

    IEnumerator LoadSceneCorroutine(string sceneName)
    {
        animator.SetTrigger("Start");
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(sceneName);
    }
}
