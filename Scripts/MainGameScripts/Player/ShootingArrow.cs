using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingArrow : MonoBehaviour
{
    public GameObject arrowPrefab;

    public float shootForce = 1f;

    public AudioSource shootingSound;

    private bool shootable;

    public Transform timerRecharge;

    public RectTransform rotatorImg;

    private bool timerTriggered;

    public Button fireButton;

    private ColorBlock colorOfFireButton;
    
    public Transform spawnRotation;

    public Transform spawnPosition;

    public Transform enemyTurret;

    public GameObject countManagement;

    // Start is called before the first frame update
    void Start()
    {
        colorOfFireButton = fireButton.colors;

        GettingReadyToShoot();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            shootArrow();
        }

        if (timerTriggered)
        {
            timerRecharging();
        }
    }

    public void shootArrow()
    {
        if (shootable
            && !PauseButton.paused
            && countManagement.GetComponent<CountDownToStartScripts>().countDownCompleted) 
        {
            GameObject arrow = Instantiate(arrowPrefab,
                spawnPosition.position,
                spawnRotation.rotation);

            Rigidbody rb = arrow.GetComponent<Rigidbody>();

            shootForce = 20 + (-spawnRotation.localRotation.y * 20);

            rb.velocity = spawnRotation.forward * shootForce;

            timerTriggered = true;

            shootable = false;

            Invoke("GettingReadyToShoot", 6.5f);

            enemyTurret.GetComponent<EnemyTurretShooting>().shootArrow();

            timerRecharge.localScale = new Vector3(1, 1, 1);

            colorOfFireButton.colorMultiplier = 1f;

            fireButton.colors = colorOfFireButton;
        } 
    }

    private void timerRecharging()
    {
        rotatorImg.localEulerAngles = new Vector3
            (0, 0,
            rotatorImg.localEulerAngles.z - 360/6.5f * Time.deltaTime);
    }

    private void GettingReadyToShoot()
    {
        rotatorImg.localEulerAngles = Vector3.zero;

        shootable = true;

        timerTriggered = false;

        timerRecharge.localScale = Vector3.zero;

        colorOfFireButton.colorMultiplier = 2f;

        fireButton.colors = colorOfFireButton;
    }

}
