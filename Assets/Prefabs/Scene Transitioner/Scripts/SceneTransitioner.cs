using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Kpable.Utilities;
using System;

public class SceneTransitioner : SingletonBehaviour<SceneTransitioner> {

    public Animator animator;

    private int levelToLoad;

    public void TransitionToScene(string sceneName)
    {
        var scene = SceneManager.GetSceneByName(sceneName);
        var index = scene.buildIndex;
        Debug.Log("TransitionToScene: " + sceneName + ":" + index);
        FadeToLevel(0);
    }

    public void TransitionToScene(int sceneIndex)
    {
        var scene = SceneManager.GetSceneByBuildIndex(sceneIndex);
        Debug.Log("TransitionToScene: " + scene.name + ":" + sceneIndex);
        FadeToLevel(sceneIndex);
    }

    public void RestartScene()
    {
        TransitionToScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextScene()
    {
        TransitionToScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        animator.SetTrigger("FadeOut");
    }

    void FadeFromLevel()
    {
        if(animator != null)
            animator.SetTrigger("FadeIn");
    }

    void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == levelToLoad)
            FadeFromLevel();
    }

    public void OnFadeOutComplete()
    {
        SceneManager.LoadScene(levelToLoad);
        SceneManager.sceneLoaded += SceneLoaded;
    }

    public void OnFadeInComplete()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }
}
