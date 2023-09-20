using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        // Move the bullet forward
        transform.Translate(0,0, speed * Time.deltaTime);


    }


}
