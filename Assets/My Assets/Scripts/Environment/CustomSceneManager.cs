using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomSceneManager : MonoBehaviour
{
    CustomSceneManager instance;
    public float transitionTimeInSecond = 0f;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CanvasFade.instance.SwitchFadeState(false);
    }

    public void ChangeScene(int value)
    {
        CanvasFade.instance.SwitchFadeState(true);
        GameManager.ResetGameValues();
        StartCoroutine(ChangeSceneAfterSeconds(transitionTimeInSecond, value));
    }

    public void NextScene()
    {
        int activeScene = SceneManager.GetActiveScene().buildIndex;
        if (SceneManager.sceneCountInBuildSettings > activeScene + 1)
        {
            GameManager.ResetGameValues();
            ChangeScene(activeScene + 1);
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReloadScene()
    {
        GameManager.ResetGameValues();
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    IEnumerator ChangeSceneAfterSeconds(float sec, int sceneValue)
    {
        yield return new WaitForSeconds(sec);
        SceneManager.LoadScene(sceneValue);
    }
}
