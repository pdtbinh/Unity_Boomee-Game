using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBombExplode : MonoBehaviour
{
    public GameObject enemyExplosion;

    private float spawnTime;

    private bool hitSomeone = false;

    private float countDown;

    public float exploisionForce = 30f;

    private float exploisionRadius = 2f;

    private float upwardForce = 20f;

    private bool enemyBombSoundPlayed;

    public Transform topFire1;

    public Transform topFire2;

    public Transform topFire3;

    public Transform topFire4;

    public Transform topFire5;


    // Start is called before the first frame update
    void Start()
    {
        countDown = 2.5f;

        spawnTime = Time.time;

        enemyBombSoundPlayed = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > spawnTime + countDown || hitSomeone)
        {
            BombExplode();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character" || collision.gameObject.tag == "Enemy")
        {
            hitSomeone = true;
        }
    }

    private void BombExplode()
    {
        if (!enemyBombSoundPlayed)
        {
            GetComponent<AudioSource>().Play();

            GameObject explode = Instantiate(enemyExplosion, transform.position, Quaternion.identity);

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
                }
            }

            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;

            minimizeFire();

            enemyBombSoundPlayed = true;

        }

        Invoke("deleteObject", 0.8f);

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

}
