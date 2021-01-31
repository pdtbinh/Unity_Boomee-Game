using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreAndTime : MonoBehaviour
{
    public Text scoreDisplay;

    public int score;

    public TextMeshProUGUI timeDisplay;

    public int startSeconds;

    public int seconds;

    public GameObject manageCountdown;

    public AudioSource ticktackSound;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;

        StartCoroutine(countdown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator countdown()
    {
        while (true)
        {
            if (manageCountdown.GetComponent<CountDownToStartScripts>().countDownCompleted)
            {
                if (seconds >= 0)
                {
                    timeDisplay.text = "00:" + seconds.ToString();

                    if (seconds <= 10)
                    {
                        ticktackSound.Play();
                    }
                }
                else
                {
                    timeDisplay.text = "00:00";

                    Invoke("stopGame", 2f);
                }
                seconds -= 1;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    private void stopGame()
    {
        SceneManager.LoadScene(0);
    } 
}
