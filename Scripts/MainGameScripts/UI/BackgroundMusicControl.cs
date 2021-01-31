using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusicControl : MonoBehaviour
{
    public AudioSource backgroundAudio;

    private bool played;

    private bool called;

    // Start is called before the first frame update
    void Start()
    {
        played = false;

        called = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!called)
        {
            Invoke("playAudio", 3f);
            called = true;
        }
    }

    private void playAudio()
    {
        if (!played)
        {
            backgroundAudio.Play();
            backgroundAudio.loop = true;
            played = true;
        }
        
    }
}
