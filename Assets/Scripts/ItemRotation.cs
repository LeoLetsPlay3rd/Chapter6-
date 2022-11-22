using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRotation : MonoBehaviour
{
    public int RotationSpeed = 100;
    Transform ItemTransform;

    void Start()
    {
        ItemTransform = this.GetComponent<Transform>();
    }

    void Update()
    {
        // This Transform class method takes in three axes, one for the X, Y,
        // and Z rotations you want to execute.Since we want the item to rotate end over end,
        // we'll use the x axis and leave the others set to 0:
        ItemTransform.Rotate(RotationSpeed * Time.deltaTime, 0, 0);
    }
}
