using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineExplode : MonoBehaviour
{
    public GameObject mineExplosion;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Character")
        {
            GameObject explosion = Instantiate(mineExplosion, transform.position, Quaternion.identity);

            GameObject playerCharacter = GameObject.FindGameObjectWithTag("Character");

            playerCharacter.GetComponent<Rigidbody>().AddExplosionForce
                (50f, transform.position - new Vector3(0f, 3f, 0f),10f, 20f, ForceMode.Impulse);

            Destroy(gameObject, 0.2f);
        }
    }
}
