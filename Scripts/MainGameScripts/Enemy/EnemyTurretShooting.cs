using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTurretShooting : MonoBehaviour
{
    public GameObject arrowPrefab;

    public AudioSource shootingSound;

    public float shootForce;

    public Transform spawnRotation;

    public Transform spawnPosition;

    public void shootArrow()
    {
        GameObject arrow = Instantiate(arrowPrefab,
                spawnPosition.position,
                spawnRotation.rotation);

        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        shootForce = 20 + (-spawnRotation.localRotation.y * 20);

        rb.velocity = spawnRotation.forward * shootForce;
    }

}
