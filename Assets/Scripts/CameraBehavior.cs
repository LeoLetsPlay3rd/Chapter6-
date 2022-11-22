using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehavior : MonoBehaviour
{
    public Vector3 CamOffset = new Vector3(0f, 1.2f, -2.6f); // Declares a Vector3 variable to store the distance we want between the Main Camera and the Player capsule.

    private Transform _target; // Creates a variable to hold the player capsule's Transform information.

    void Start()
    {
        _target = GameObject.Find("Player").transform; // Uses GameObject.Find to locate the capsule by name and retrieve its Transform property from the scene:
    }

    void LateUpdate() // LateUpdate is a MonoBehavior method, like Start or Update, that executes after Update.
    {
        this.transform.position = _target. 
        TransformPoint(CamOffset); // Sets the camera's position to _target.TransformPoint(CamOffset) for every frame. The TransformPoint method calculates and returns a relative position in the world space.

        this.transform.LookAt(_target); // The LookAt method updates the capsule's rotation every frame, focusing on the Transform parameter we pass in, which, in this case, is _target.
    }
}
