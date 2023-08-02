using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        // Get the horizontal and vertical input values (WASD or arrow keys)
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction based on input values
        Vector3 movementDirection = new Vector3(horizontalInput, verticalInput,0f).normalized;

        // Apply movement to the GameObject
        transform.Translate(movementDirection * moveSpeed * Time.deltaTime);
        //my 'first' comment, your comment sucks
        //no it doesn't
        //comments comments everywhere!!!
        //ship it!!!
    }
}
