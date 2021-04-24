using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Chronos;

public class Bullet : ChronosBehaviour
{
    public float speed;

    void Start()
    {
        if (time.timeScale > 0) // Move only when time is going forward
        {
            time.rigidbody.velocity = transform.forward * speed;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * speed * Time.deltaTime;
    }


    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
