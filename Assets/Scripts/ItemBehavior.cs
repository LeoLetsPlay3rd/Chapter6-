using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBehavior : MonoBehaviour
{
    public GameBehavior GameManager; // Adds a new variable to store a reference to the attached script.

    void Start() // Uses the start method to initialize GameManager by looking it up in the scene with Find() and adding a call to GetComponent().
    {
        GameManager = GameObject.Find("Game_Manager").GetComponent<GameBehavior>();
    }
    
    void OnCollisionEnter(Collision collision) // This method runs when another object runs into the Item Prefab.
    {
        if(collision.gameObject.name == "Player") // This holds a reference to the colliding GameObject's Collider.
        {
            Destroy(this.transform.gameObject); // This method runs if the colliding object is the player. The item will be removed from the scene.
            Debug.Log("Item collected!");

            GameManager.Items += 1; // Increments the Items property in the GameManger class in OnCollisionEnter() after the Item Prefab is destroyed.
        }
    }
}
