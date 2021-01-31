using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButton : MonoBehaviour
{
    public static bool paused;

    public Button pauseAndResumeButton;

    public Button resumeButton;

    private Sprite resumeImage;

    public Button pauseButton;

    private Sprite pauseImage;

    public GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        paused = false;
    }

    // Update is called once per frame
    void Update()
    {
        resumeImage = resumeButton.image.sprite;

        pauseImage = pauseButton.image.sprite;
    }

    public void pauseMainGame()
    {
        Time.timeScale = 0f;
        AudioListener.pause = true;
        paused = true;
        pauseAndResumeButton.image.sprite = resumeImage;
        pausePanel.SetActive(true);
    }

    public void resumeMainGame()
    {
        Time.timeScale = 1f;
        AudioListener.pause = false;
        paused = false;
        pauseAndResumeButton.image.sprite = pauseImage;
        pausePanel.SetActive(false);
    }

    public void pauseAndResumeGame()
    {
        if (!paused)
        {
            pauseMainGame();
        }
        else
        {
            resumeMainGame();
        }
        
    }

    private void OnApplicationPause(bool pause)
    {
        pauseMainGame();
    }

}
