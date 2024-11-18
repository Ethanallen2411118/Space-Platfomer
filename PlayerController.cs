using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public float jumpSpeed = 8f;
    private float direction = 0f;
    private Rigidbody2D player;

    public Transform groundCheck;
    public float groundCheckRadius = 0.2f; // Example default value
    public LayerMask groundLayer;
    private bool isTouchingGround;

    private Vector3 respawnPoint;
    public GameObject fallDetector;

    

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        respawnPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
        direction = Input.GetAxis("Horizontal");
        Debug.Log("Direction: " + direction);
        Debug.Log("IsTouchingGround: " + isTouchingGround);

        if (direction > 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(0.2469058f, 0.2469058f); // Ensure the scale is positive when moving right
        }
        else if (direction < 0f)
        {
            player.velocity = new Vector2(direction * speed, player.velocity.y);
            transform.localScale = new Vector2(-0.2469058f, 0.2469058f); // Flip the scale when moving left
        }
        else
        {
            player.velocity = new Vector2(0, player.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && isTouchingGround)
        {
            player.velocity = new Vector2(player.velocity.x, jumpSpeed);
        }

        fallDetector.transform.position = new Vector2(transform.position.x, fallDetector.transform.position.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("FallDetector"))
        {
            transform.position = respawnPoint;
        }
        else if (collision.CompareTag("Checkpoint"))
        {
            // Update the respawn point to the checkpoint's position
            respawnPoint = transform.position;


        }
        else if (collision.tag == "Crystal") 
        {
            Scoring.totalScore += 1;
            Debug.Log(Scoring.totalScore);
            collision.gameObject.SetActive(false);
        }   
    }
    }