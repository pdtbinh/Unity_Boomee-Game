using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject explosion;

    private float spawnTime;

    private bool hitCharacter = false;

    private float countDown;

    public float exploisionForce = 30f;

    private float exploisionRadius = 2f;

    private float upwardForce = 20f;

    private bool soundPlayed;

    public Transform topFire1;

    public Transform topFire2;

    public Transform topFire3;

    public Transform topFire4;

    public Transform topFire5;

    private GameObject enemyCharacter;

    private GameObject enemyTurret;

    private float timeWaitInConsecutiveShots;

    //public GameObject aimer;

    //private GameObject aimerContainer;

    private bool aimerInstantiated;


    public GameObject textPrefab;

    private bool missedTextShowed;

    private bool explosionReachEnemy;


    public GameObject niceTextPrefab;

    private bool niceTextShowed;




    // Start is called before the first frame update
    void Start()
    {
        countDown = 3f;

        spawnTime = Time.time;

        soundPlayed = false;

        enemyTurret = GameObject.FindGameObjectWithTag("Enemy Turret");

        timeWaitInConsecutiveShots = 0.4f;

        aimerInstantiated = false;

        explosionReachEnemy = false;

        missedTextShowed = false;

        niceTextShowed = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + 1f)
        {
            if (!aimerInstantiated) {createAimer();}

            updateAimerPosition();
            
        }

        if (Time.time > spawnTime + countDown || hitCharacter)
        {
            BombExplode();
        }
    }

    private void createAimer()
    {
        //aimerContainer = Instantiate(aimer, transform.position, Quaternion.identity);
        aimerInstantiated = true;
    }

    private void updateAimerPosition()
    {
       // if (aimerContainer != null)
        {
        //    aimerContainer.transform.position = transform.position;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Enemy")
        {
            hitCharacter = true;
        }
    }

    private void BombExplode()
    {
        if (!soundPlayed)
        {
            GetComponent<AudioSource>().Play();

            GameObject explode = Instantiate(explosion, transform.position, Quaternion.identity);

            Collider[] colliders = Physics.OverlapSphere(transform.position, exploisionRadius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.constraints = RigidbodyConstraints.None;

                    rb.AddExplosionForce(exploisionForce,
                        new Vector3(transform.position.x, transform.position.y, transform.position.z),
                        exploisionRadius,
                        upwardForce,
                        ForceMode.Impulse);

                    if (rb.gameObject.tag == "Enemy")
                    {
                        explosionReachEnemy = true;
                    }
                }
            }
    
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            enemyCharacter = GameObject.FindGameObjectWithTag("Enemy");

            float distanceToEnemy = Vector3.Distance(
                transform.position,
                enemyCharacter.transform.position);

            if (!explosionReachEnemy)
            {
                GameObject floatingText = Instantiate(textPrefab, Vector3.zero, Quaternion.identity);
                floatingText.GetComponent<TextMesh>().text = "Missed\n" + (int)distanceToEnemy + "  ft";
                missedTextShowed = true;
            }

            if ((hitCharacter || explosionReachEnemy) && !niceTextShowed)
            {
                float random = Random.Range(0, 2);
                string congrats = "Nice !";
                GameObject niceText = Instantiate(niceTextPrefab, Vector3.zero, Quaternion.identity);
                if (random < 0.33)
                {
                    congrats = "Awesome !";
                }
                else if (random < 0.66)
                {
                    congrats = "Well played !";
                }
                niceText.GetComponent<TextMesh>().text = congrats;

                niceTextShowed = true;
            }

            soundPlayed = true;

           
            minimizeFire();

            if (missedTextShowed)
            {
                if (distanceToEnemy < 1f)
                {
                    Invoke("enemyTurretShoot", 1.2f);
                }
                else if (distanceToEnemy < 5f)
                {
                    Invoke("enemyTurretShootTwice", 1.2f);
                }
                else if (distanceToEnemy < 8f)
                {
                    Invoke("enemyTurretShootThrice", 1.2f);
                }
                else if (distanceToEnemy < 12f)
                {
                    Invoke("enemyTurretShootFourTimes", 1.2f);
                }
                else
                {
                    Invoke("enemyTurretShootFiveTimes", 1.2f);
                }
            }
            
        }

        Invoke("deleteObject", 5f);
        
    }

    private void minimizeFire()
    {
        topFire1.localScale = Vector3.zero;

        topFire2.localScale = Vector3.zero;

        topFire3.localScale = Vector3.zero;

        topFire4.localScale = Vector3.zero;

        topFire5.localScale = Vector3.zero;

    }

    private void deleteObject()
    {
        Destroy(gameObject);
    }



    private void enemyTurretShoot()
    {
        enemyTurret.GetComponent<EnemyTurretShooting>().shootArrow();
    }

    private void enemyTurretShootOnce()
    {
        enemyTurretShoot();

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 1);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 2);
    }

    private void enemyTurretShootTwice()
    {
        enemyTurretShoot();

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 1);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 2);
    }

    private void enemyTurretShootThrice()
    {
        enemyTurretShoot();

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 1);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 2);
    }

    private void enemyTurretShootFourTimes()
    {
        enemyTurretShoot();

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 1);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 2);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 3);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 4);
    }

    private void enemyTurretShootFiveTimes()
    {
        enemyTurretShoot();

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 1);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 2);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 3);

        Invoke("enemyTurretShoot", timeWaitInConsecutiveShots * 4);
    }
}
