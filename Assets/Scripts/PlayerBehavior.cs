using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehavior : MonoBehaviour
{
    public float MoveSpeed = 10f; // How fast we want the player to go forward and backward
    public float RotateSpeed = 75f; // How fast we want the player to rotate left and right

    private float _vInput; // Stores the vertical axis input
    private float _hInput; // Stores the horizontal axis input

    private Rigidbody _rb; // Adds a private variable of type Rigidbody that will contain a reference to the capsule's Rigidbody component.

    public float JumpVelocity = 5f; // Adds a public variable to hold the amount of applied jump force we want.
    private bool _isJumping; // Adds a private boolean to check if the player should be jumping.

    public float DistanceToGround = 0.1f; // Adds a new variable for the distance we'll check between the player Capsule Collider and any Ground layer object.
    public LayerMask GroundLayer; // Adds a LayerMask variable that we can set in the Inspector and use for the collider detection.
    private CapsuleCollider _col; // Adds a variable to store the player's Capsule Collider component.

    public GameObject Bullet; // Adds a variable to store the Bullet Prefab.
    private float BulletSpeed = 100f; // Adds a variable to hold the Bullet speed.

    private bool _isShooting; // Adds a boolean that we can use in the Update method to check if our player should be shooting.

    void Start()
    {
        _rb = GetComponent<Rigidbody>(); // The GetComponent method checks whether the component type we're looking for, in this case, Rigidbody, exists on the GameObject the script is attached to and returns it.

        _col = GetComponent<CapsuleCollider>(); // We use the GetComponent to find and return the Capsule Collider attached to the player.
    }

    void Update()
    {
        _vInput = Input.GetAxis("Vertical") * MoveSpeed; // Detects when W or S keys are pressed and multiplies value by MoveSpeed. W returns value 1 (positive direction) S returns value -1 (negative direction)
 
        _hInput = Input.GetAxis("Horizontal") * RotateSpeed; // Detects when A or D keys are pressed and multiplies value by RotateSpeed. D returns value 1 (positive direction) A returns value -1 (negative direction)

        /*
        this.transform.Translate(Vector3.forward * _vInput *
        Time.deltaTime); // Takes in a Vector3 parameter to move the transform component.Vector3.forward multiplied by _vInput and Time.deltaTime supplies the direction and speed the capsule needs to move forward or back along the z axis at the speed we've calculated. Time.deltaTime will always return the value in seconds since the last frame of the game was executed.It's commonly used to smooth values that are captured or run in the Update method instead of letting it be determined by a device's frame rate.

        this.transform.Rotate(Vector3.up * _hInput *
        Time.deltaTime); // Rotates the transform component relative to the vector we pas in as a parameter. Vector3.up multiplied by _hInput and Time.deltaTime gives us the left / right rotation axis we want. We use the this keyword and Time.deltaTime here for the same reasons.
        */

        _isJumping |= Input.GetKeyDown(KeyCode.Space); // We set the value of _isJumping to the Input.GetKeyDown() method which returns a bool value depending on whether a specific key is pressed.

        _isShooting |= Input.GetMouseButtonDown(0); // We set the value of _isShooting using the or logical operator and Input.GetMouseButtonDown(), which returns true if we're pushing the specified button, just like with Input.GetKeyDown(). GetMouseButtonDown() takes an int parameter to determine which mouse button we want to check for; 0 is the left button, 1 is the right button, and 2 is the middle button or scroll wheel.
    }

    void FixedUpdate() // Any physics- or Rigidbody-related code always goes inside the FixedUpdate method, rather than Update or the other MonoBehavior methods.FixedUpdate is frame rate independent and is used for all physics code.
    { 
        Vector3 rotation = Vector3.up * _hInput; //Creates a new Vector3 variable to store our left and right rotation

        Quaternion angleRot = Quaternion.Euler(rotation * Time.fixedDeltaTime); // Quaternion.Euler takes a Vector3 parameter and returns a rotation value in Euler angles.

        _rb.MovePosition(this.transform.position + this.transform.forward * _vInput * Time.fixedDeltaTime); // Calls MovePosition on our _rb component, which takes in a Vector3 parameter and applies force accordingly.

        _rb.MoveRotation(_rb.rotation * angleRot); // Calls the MoveRotation method on the _rb component, which also takes in a Vector3 parameter and applies the corresponding forces under the hood. angleRot already has the horizontal inputs from the keyboard, so all we need to do is multiply the current Rigidbody rotation by angleRot to get the same left and right rotation.

        if (IsGrounded() && _isJumping) // Uses an if-statement to check if _isJumping true and trigger the jump mechanic if it is.
        {
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse); // We can pass the Vector3 and ForceMode parameters to Rigidbody.AddForce() since the Rigidbody component is already stored.
            _rb.AddForce(Vector3.up * JumpVelocity, ForceMode.Impulse); // We update the if-statement to check wether IsGrounded returns true and the spacebar is pressed before executing the jump code.
        }
        
        _isJumping = false; // We reset _isJumping to false so the input check knows a complete jump and the landing cycle has been completed.

        if (_isShooting) // Then we check if our player is supposed to be shooting using the _isShooting input check variable.
        {
            GameObject newBullet = Instantiate(Bullet, this.transform.position + new Vector3(1, 0, 0), this.transform.rotation); // We create a local GameObject variable every time the left mouse button is pressed: We use the Instantiate() method to assign a GameObject to newBullet by passing in the Bullet Prefab. We also use the player capsule's position to place the new Bullet Prefab in front of the player to avoid any collisions. We append it as a GameObject to explicitly cast the returned object to the same type as newBullet, which in this case is a GameObject.

            Rigidbody BulletRB = newBullet.GetComponent<Rigidbody>(); // We call GetComponent() to return and store the Rigidbody component on newBullet.

            BulletRB.velocity = this.transform.forward * BulletSpeed; // We set the velocity property of the Rigidbody component to the player's transform.forward direction multiplied by BulletSpeed: Changing the velocity instead of using AddForce() ensures that gravity doesn't pull our bullets down in an arc when fired.
        }

        _isShooting = false; // We set the _isShooting value to false so our shooting input is reset for the next input event.
    }

    private bool IsGrounded() // We declare the IsGrounded() method with a bool return type.
    {
        Vector3 capsuleBottom = new Vector3(_col.bounds.center.x, _col.bounds.min.y, _col.bounds.center.z); // We create a local Vector3 variable to store the position at the bottom of the player's capsule Collider, which we'll use to check for collision with any objects on the Ground layer.

        bool grounded = Physics.CheckCapsule(_col.bounds.center, capsuleBottom, DistanceToGround, GroundLayer, QueryTriggerInteraction.Ignore); // We create a local bool to store the result of the CheckCapsule() method that we call from the Physics class, which takes in the following five arguments: (can be found on page 211 in book)

        return grounded; // We return the value stored in grounded at the end of the calculation.
    }
}
