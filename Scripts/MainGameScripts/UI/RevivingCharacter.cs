using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RevivingCharacter : MonoBehaviour
{
    public GameObject player;

    public GameObject panel;

    public GameObject countdownToStart;

    public GameObject countdownManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void revive()
    {
        AudioListener.pause = false;

        player.transform.localScale = new Vector3(1, 1, 1);

        panel.SetActive(false);

        countdownToStart.SetActive(true);

        countdownManager.GetComponent<CountDownToStartScripts>().currentlyActive = true;


    }
}
