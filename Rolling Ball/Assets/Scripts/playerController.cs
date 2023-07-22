using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float moveSpeed;
    private GameObject focalPoint;
    private Transform playerTr;
    public bool hasPower = false;
    private float powerStrength = 20.0f;
    public GameObject PowerUpIndicator;

    // Start is called before the first frame update
    void Start()
    {
        playerTr = GetComponent<Transform>();
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
    }

    // FixedUpdate is used for physics-related tasks
    void FixedUpdate()
    {
        MoveBall(); // Move the ball using physics in FixedUpdate
    }

    void Update()
    {
        PowerUpIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0); // Move the PowerUpIndicator with the ball
    }

    // Function to move the ball based on player input
    void MoveBall()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the move direction in the camera's local space (relative to the focal point)
        Vector3 moveDirection = (horizontalInput * focalPoint.transform.right + verticalInput * focalPoint.transform.forward).normalized;

        // Move the ball in the calculated direction
        if (moveDirection != Vector3.zero)
        {
            // Use ForceMode.VelocityChange to ensure consistent movement speed regardless of framerate
            playerRb.AddForce(moveDirection * moveSpeed * Time.fixedDeltaTime, ForceMode.VelocityChange);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPower = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            PowerUpIndicator.gameObject.SetActive(true);
        }
    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPower = false;
        PowerUpIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPower)
        {
            // increase player size
            Vector3 increasedScale = transform.localScale + new Vector3(0.1f, 0.1f, 0.1f);
            transform.localScale = increasedScale;

            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            // Apply a force to the enemy in the opposite direction from the player's position
            enemyRigidbody.AddForce(awayFromPlayer * powerStrength, ForceMode.Impulse);
        }
    }
}

