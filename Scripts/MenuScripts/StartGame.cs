using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class StartGame : MonoBehaviour
{

    public Slider loadingProgressSlider;

    public TextMeshProUGUI loadingPercent;

    public GameObject loadingScene;

    public void LoadMainGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;

        StartCoroutine(LoadMainGameAsync());
    }

    IEnumerator LoadMainGameAsync()
    {
        loadingScene.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(1);

        while (!operation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingProgressSlider.value = loadingProgress;

            loadingPercent.text = ((int) (loadingProgress * 100)).ToString() + " %";

            yield return null;
        }
    }
}
