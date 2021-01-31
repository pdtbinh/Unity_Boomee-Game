using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


//Belongs to joystick, control player movement and player shooting

public class UserMovement : MonoBehaviour
{

    // UI:

    public GameObject countManagement;

    public GameObject revivePanel;

    public TextMeshProUGUI yourScoreText;

    public Transform playerSpawnPosition;

    private bool reviveCalled;

    public GameObject backgroundMusic;

    public GameObject scoreAndTime;

    public GameObject continueButton;

    public bool gameOver;

    public GameObject watchAdToReviveButton;


    // PLAYER COMPONENTS:

    //public Transform transform;

    Rigidbody playerSRigidBody;

    public Transform imagePartOfPlayer;

    public float speed = 0f; //Player's movement speed

    Animator animator;

    private bool exploded = false;

    public AudioSource boomSound;



    // JOYSTICK COMPONENTS:

    public Transform outerCircle;

    private RectTransform rtOfOuterCircle;

    public Transform innerCircle;

    private RectTransform rtOfInnerCircle;

    private bool touchStart = false;

    private float maximumDistanceAllowed; //To check if touch is closer enough, movement will be started

    private float maximumDragDistance; //To avoid player's changing direction while moving and shooting



    private void Start()
    {
        //PLAYER COMPONENTS:

        playerSRigidBody = GetComponent<Rigidbody>();

        animator = imagePartOfPlayer.GetComponent<Animator>();

        gameOver = false;


        //JOYSTICK COMPONENTS:

        rtOfInnerCircle = innerCircle.GetComponent<RectTransform>();

        rtOfOuterCircle = outerCircle.GetComponent<RectTransform>();

        maximumDistanceAllowed = 1.8f;

        maximumDragDistance = 2.5f;
    }




    // Update is called once per frame
    void Update()
    {
        if (!exploded)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); //Keep player stable

            callMoving();
        }

        else if (!reviveCalled)
        {
            Invoke("revivePanelPopUp", 1f);

            if (!reviveCalled)
            {
                Invoke("temporarilyDeleteCharacter", 2f);
                reviveCalled = true;
            } 
        }

        else if (exploded && reviveCalled && gameOver)
        {
            Invoke("revivePanelPopUp", 1f);
            watchAdToReviveButton.SetActive(false);
        }
    }


    public Slider loadingProgressSlider;

    public TextMeshProUGUI loadingPercent;

    public GameObject loadingScene;



    public void LoadMainMenu()
    {
        int score = scoreAndTime.GetComponent<ScoreAndTime>().score;

        if (score > PlayerPrefs.GetInt("BestScore", score))
        {
            PlayerPrefs.SetInt("BestScore", score);
        }

            Time.timeScale = 1f;
        AudioListener.pause = false;

        StartCoroutine(LoadMainMenuAsync());
    }

    IEnumerator LoadMainMenuAsync()
    {
        loadingScene.SetActive(true);

        AsyncOperation operation = SceneManager.LoadSceneAsync(0);

        while (!operation.isDone)
        {
            float loadingProgress = Mathf.Clamp01(operation.progress / 0.9f);

            loadingProgressSlider.value = loadingProgress;

            loadingPercent.text = ((int) (loadingProgress * 100)).ToString() + " %";

            yield return null;
        }
    }



    //MOVING CHARACTER SECTION:


    private void callMoving()
    {
        if (Input.GetMouseButton(0)
            && !PauseButton.paused
            && countManagement.GetComponent<CountDownToStartScripts>().countDownCompleted)
        {
            moveCharacterUsingJoystick(Input.mousePosition);
        }
        else
        {
            resetJoystick();
        }
    }


    private void moveCharacter(Vector3 direction)
    {
        playerSRigidBody.constraints = RigidbodyConstraints.None;

        if (!exploded)
        {
            playerSRigidBody.constraints =
                  RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationZ;
        }


        playerSRigidBody.velocity = direction * speed;
    }


    private void rotatePlayer(Vector2 joystickPosition)
    {
        //Cross product = length_1 x length_2 x cos(angle)

        float crossProduct = joystickPosition.x * 0 + joystickPosition.y * 1;

        float angleMoved = Mathf.Acos(crossProduct / (joystickPosition.magnitude * 1)) * 180 / Mathf.PI;

        angleMoved = (joystickPosition.x < 0) ? -angleMoved : angleMoved;

        imagePartOfPlayer.localRotation = Quaternion.Euler(0, angleMoved, 0);
    }


    private void moveCharacterUsingJoystick(Vector3 positionOfTheMouse)
    {
        Vector2 pos;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rtOfOuterCircle, positionOfTheMouse, null, out pos))
        {
            //This part is essential for main moving part: 
            float convertedX = (pos.x / rtOfOuterCircle.rect.size.x) * 2 - 1; //Convert touch x-coord to x in range [-1; 1]
            float convertedY = (pos.y / rtOfOuterCircle.rect.size.y) * 2 - 1; //Convert touch y-coord to y in range [-1; 1]

            //This part is only for first-touch check
            float distance = Mathf.Sqrt(convertedX * convertedX + convertedY * convertedY);
            if (distance <= maximumDistanceAllowed)
            {
                touchStart = true;
            }

            //Main part for moving joystick and character
            if (touchStart && distance <= maximumDragDistance)
            {
                animator.SetBool("moving", true);

                Vector2 convertedVector = new Vector2(convertedX, convertedY);
                convertedVector = (convertedVector.magnitude > 1f) ? convertedVector.normalized : convertedVector;

                rotatePlayer(convertedVector);

                //Move the joystick
                rtOfInnerCircle.anchoredPosition = new Vector3(
                    convertedVector.x * rtOfOuterCircle.rect.size.x / 3,
                    convertedVector.y * rtOfOuterCircle.rect.size.y / 3);

                //Move character accordingly
                moveCharacter(Vector3.Normalize(new Vector3(convertedX, 0, convertedY)));
            }
        }
    }


    private void resetJoystick()
    {
        touchStart = false;

        rtOfInnerCircle.anchoredPosition = Vector3.zero;

        if (!exploded)
        {
            playerSRigidBody.constraints = RigidbodyConstraints.FreezeAll;
        }

        playerSRigidBody.constraints = RigidbodyConstraints.None;

        animator.SetBool("moving", false);
    }


    private void temporarilyDeleteCharacter()
    {
        transform.localScale = Vector3.zero;

        transform.position = playerSpawnPosition.position;

        transform.rotation = Quaternion.Euler(0, 0, 0); //Keep player stable

        imagePartOfPlayer.localRotation = Quaternion.Euler(0, 0, 0);

        exploded = false;

        resetJoystick();

        animator.SetBool("die", false);

        countManagement.GetComponent<CountDownToStartScripts>().countDownCompleted = false;

        exploded = false;

        gameOver = true;

    }


    private void revivePanelPopUp()
    {
        int score = scoreAndTime.GetComponent<ScoreAndTime>().score;

        if (score > PlayerPrefs.GetInt("BestScore", score))
        {
            PlayerPrefs.SetInt("BestScore", score);

            yourScoreText.text = "New Best Score";
        }

        revivePanel.SetActive(true);
        continueButton.SetActive(false);
    }


    void OnCollisionEnter(Collision collision)
    {
        if ((collision.gameObject.tag == "Explosion" || collision.gameObject.tag == "Player Land Mine")
            && !exploded)
        {
            if (collision.gameObject.tag == "Player Land Mine") { boomSound.Play(); }
            
            transform.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;

            exploded = true;

            imagePartOfPlayer.GetComponent<Animator>().SetBool("die", true);

            backgroundMusic.GetComponent<AudioSource>().Pause();

            Invoke("pauseAudio", 2f);
        }
    }

    private void pauseAudio()
    {
        AudioListener.pause = true;
    }



}



