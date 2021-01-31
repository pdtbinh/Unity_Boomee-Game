using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CountDownToStartScripts : MonoBehaviour
{
    private int currentSecond;

    public TextMeshProUGUI count;

    public GameObject countAsGameObject;

    public bool currentlyActive;

    public AudioSource ticktack;

    public bool countDownCompleted;

    public GameObject backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        currentSecond = 4;

        currentlyActive = true;

        countDownCompleted = false;

        StartCoroutine(countDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator countDown()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            if (currentSecond > 3 && currentlyActive)
            {
                currentSecond -= 1;
                ticktack.Play();
                count.text = "3";
            }

            else if (currentSecond > 1 && currentlyActive)
            {
                currentSecond -= 1;
                ticktack.Play();
                count.text = currentSecond.ToString();
            }

            else if (currentlyActive)
            {
                ticktack.Play();
                count.text = "FIRE !";
                countDownCompleted = true;
                backgroundMusic.GetComponent<AudioSource>().Play();
                Invoke("countDisappear", 0.5f);
            }
            
        }
    }

    public void countDisappear()
    {
        countAsGameObject.SetActive(false);

        count.text = "3";

        currentSecond = 4;

        currentlyActive = false;
    }
}
