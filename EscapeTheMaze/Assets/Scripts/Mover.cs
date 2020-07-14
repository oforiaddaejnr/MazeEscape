using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    public float speed = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Fire the bullet
        Rigidbody r = GetComponent<Rigidbody>();
        r.velocity = transform.forward * speed;

        //Destroy bullet after some time in case it missed player and landed elsewhere
        Destroy(gameObject, 10);

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        //Restart the game when player gets hit by bullet
        if (other.gameObject == GameObject.FindWithTag("Player"))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

    }
}
