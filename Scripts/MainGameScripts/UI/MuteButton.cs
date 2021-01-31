using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteButton : MonoBehaviour
{
    private bool mute;

    public Button muteAndUnmuteButton;

    public Button muteButton;

    private Sprite muteImage;

    public Button unmuteButton;

    private Sprite unmuteImage;


    public 

    // Start is called before the first frame update
    void Start()
    {
        mute = false;

        muteImage = muteButton.image.sprite;

        unmuteImage = unmuteButton.image.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void muteAndUnmute()
    {
        if (!mute)
        {
            AudioListener.volume = 0f;
            muteAndUnmuteButton.image.sprite = muteImage;
            mute = true;
        }
        else
        {
            AudioListener.volume = 1f;
            muteAndUnmuteButton.image.sprite = unmuteImage;
            mute = false;
        }
    }
}
