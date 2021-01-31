using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyMovementNew : MonoBehaviour
{
    // MOVING PART:

    public LayerMask whatIsWall;

    private Rigidbody rigidBody;

    public Transform imagePartOfEnemy;

    public Transform wallDetector1;

    private Vector3 movingDirection;

    private Vector3 rotatingDirection;

    private bool rotating;

    public float rotatingSpeed = 1f;

    private bool moving;

    public float movingSpeed = 1f;

    private float changeDirectionTimeRecord;

    private bool exploded;

    public GameObject enemyCharacter;

    public Transform enemySpawnPosition;

    private bool respawned;

    public Transform skin;

    public GameObject timeAndScoreManager;

    private bool timeAndScoreAreReset;

    public Transform enemyTurret;

    private Animator animator;

    public AudioSource increaseScoreSound;

    public GameObject countManagement;


    // Start is called before the first frame update
    void Start()
    {
        // MOVING COMPONENTS:

        rigidBody = GetComponent<Rigidbody>();

        resetAllStates();

        animator = imagePartOfEnemy.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!exploded
            && !PauseButton.paused
            && countManagement.GetComponent<CountDownToStartScripts>().countDownCompleted)
        {
            movingRandomly();
        }

        else if (exploded)
        {
            rigidBody.constraints = RigidbodyConstraints.None;

            animator.SetBool("die", true);

            Invoke("TimeAndScoreReset", 1.5f);

            Invoke("makeTransparent", 1.8f);

            Invoke("Respawn", 2f);
          
            Invoke("DeleteEnemy", 3f);
        }
    }

    private void resetAllStates()
    {
        movingDirection = transform.forward;

        rotatingDirection = movingDirection;

        changeDirectionTimeRecord = 0f;

        exploded = false;

        rotating = false;

        moving = true;

        respawned = false;

        timeAndScoreAreReset = false;
    }

    private void TimeAndScoreReset()
    {
        if (!timeAndScoreAreReset)
        {
            increaseScoreSound.Play();


            int scoreAdded = timeAndScoreManager.GetComponent<ScoreAndTime>().seconds;

            int currentScore = timeAndScoreManager.GetComponent<ScoreAndTime>().score;

            timeAndScoreManager.GetComponent<ScoreAndTime>().score = scoreAdded + currentScore;

            timeAndScoreManager.GetComponent<ScoreAndTime>().scoreDisplay.text
                = timeAndScoreManager.GetComponent<ScoreAndTime>().score.ToString();



            int timeLeft = timeAndScoreManager.GetComponent<ScoreAndTime>().startSeconds * 98/100;

            timeAndScoreManager.GetComponent<ScoreAndTime>().startSeconds = timeLeft;

            timeAndScoreManager.GetComponent<ScoreAndTime>().seconds = timeLeft;

            timeAndScoreAreReset = true;
        } 
    }

    private void makeTransparent()
    {
        //skin.GetComponent<Renderer>().enabled = false;
    }

    
    private void Respawn()
    {
        if (!respawned)
        {
            float enemyTurretSpeed = enemyTurret.GetComponent<RotatingEnemyCannon>().rotatingSpeed;

            enemyTurret.GetComponent<RotatingEnemyCannon>().rotatingSpeed = enemyTurretSpeed * 1.05f;

            GameObject newEnemy = Instantiate(enemyCharacter,
                enemySpawnPosition.position, enemySpawnPosition.rotation);

            newEnemy.GetComponent<EnemyMovementNew>().movingSpeed = this.movingSpeed * 1.02f;

            newEnemy.GetComponent<EnemyMovementNew>().rotatingSpeed = this.rotatingSpeed * 1.02f;

            respawned = true;
        }  
    }

    private void DeleteEnemy()
    {
        Destroy(gameObject);
    }


    // MOVING METHODS:

    private void movingRandomly()
    {
        transform.rotation = Quaternion.Euler(0, 180, 0);

        if (rotating)
        {
            rigidBody.constraints = RigidbodyConstraints.None;

            rigidBody.constraints = RigidbodyConstraints.FreezePositionY
                | RigidbodyConstraints.FreezeRotationX
                | RigidbodyConstraints.FreezeRotationZ;

            rotatingDirection = Vector3.RotateTowards(rotatingDirection, movingDirection, rotatingSpeed * Time.deltaTime, 0f);
            imagePartOfEnemy.rotation = Quaternion.LookRotation(rotatingDirection);

            if (Vector3.Angle(rotatingDirection, movingDirection) < 1)
            {
                if (checkForWalls())
                {
                    movingDirection = chooseDirection();
                }
                else
                {
                    rotating = false;
                    moving = true;
                }
            }
        }
        else if (moving)
        {
            rigidBody.velocity = movingDirection * movingSpeed;
            if (checkForWalls())
            {
                rigidBody.constraints = RigidbodyConstraints.FreezeAll;
                movingDirection = chooseDirection();
                moving = false;
                rotating = true;
            }
        }
    }

    private Vector3 chooseDirection()
    {
        float x = Random.Range(-5, 5);
        x = (x == 0) ? 1 : x;

        float z = Random.Range(-5, 5);
        z = (z == 0) ? -1 : z;

        return (new Vector3(x, 0, z)).normalized;
    }

    private bool checkForWalls()
    {
        if (changeDirectionTimeRecord + 5 < Time.time)
        {
            changeDirectionTimeRecord = Time.time;
        }
        return Physics.Raycast(wallDetector1.position, movingDirection, 3f, whatIsWall) || changeDirectionTimeRecord == Time.time; ;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Explosion")
        {
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            exploded = true;
        }
    }

}
