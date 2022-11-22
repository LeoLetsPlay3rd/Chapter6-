using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    public float OnscreenDelay = 3f; // Adds a float variable to store how long we want the Bullet Prefabs to remain in the scene after they are instantiated.

    void Start()
    {
        Destroy(this.gameObject, OnscreenDelay); // We use the Destroy() method to delete the GameObject. Destroy() always needs an object as a parameter. In this case, we use the this keyword to specify the object that the script is attached to. Destroy() can optionally take an additional float parameter as a delay, which we use to keep the bullets on screen for a short amount of time.
    }


    void Update()
    {
        
    }
}
