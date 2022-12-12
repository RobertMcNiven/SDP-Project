/// <summary>
/// CameraMotor.cs
/// 
/// This file has code needed to make the camera follow the player object.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this to a camera in Unity to make it follow an object
/// </summary>
public class CameraMotor : MonoBehaviour
{
    [SerializeField]
    private Transform lookAt;
    private float boundX = 0.4f;
    private float boundY = 0.2f;

    /// <summary>
    /// <c>LateUpdate</c>called after Update and FixedUpdate
    /// This was very early code lol.
    /// </summary>
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        // check to see if inside camera bounds on X axis
        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX || deltaX < -boundX)
        {
            if(transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - boundX;
            } 
            else
            {
                delta.x = deltaX + boundX;
            }
        }

        // check to see if inside camera bounds on Y axis
        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY || deltaY < -boundY)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }

        transform.position += new Vector3(delta.x, delta.y, 0);

    }

}
