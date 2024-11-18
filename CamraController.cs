using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float offset;
    public float offsetSmoothing;
    private Vector3 playerPosition;

    // Update is called once per frame
    void LateUpdate()
    {
        // Follow the player on the x-axis, keep y and z positions constant
        playerPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        // Check the player's local scale to determine direction
        if (player.transform.localScale.x > 0f)
        {
            playerPosition = new Vector3(player.transform.position.x + offset, playerPosition.y, playerPosition.z);
        }
        else
        {
            playerPosition = new Vector3(player.transform.position.x - offset, playerPosition.y, playerPosition.z);
        }

        // Smoothly move the camera towards the player position
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);
    }
}
