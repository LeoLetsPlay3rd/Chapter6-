using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    void OnTriggerEnter(Collider other) // This method runs whenever an object enters the Enemy Sphere Collider radius. Other is a type of Collider not Collision
    {
        if(other.name == "Player") // other is used to access the name of the colliding GameObject and check if it is the player with an if-statement.
        {
            Debug.Log("Player detected - attack!");
        }
    }

    void OnTriggerExit(Collider other) // This method runs when an object leaves the Enemy Sphere Collider radius.
    {
        if(other.name == "Player") // other is used to access the name of the colliding GameObject and check if it is the player with an if-statement.
        {
            Debug.Log("Player out of range, resume patrol");
        }
    }
}
