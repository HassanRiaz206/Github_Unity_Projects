using UnityEngine;
using TMPro;

public class Meteor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private int health;

    [SerializeField] private TMP_Text textHealth;
    [SerializeField] private float jumpForce;

    protected float[] leftAndRight = new float[2] { -1f, 1f };

    [HideInInspector] public bool isResultOfFission = true;

    private bool isShowing;

    private void Start()
    {
        UpdateHealthUI();

        isShowing = true;
        rb.gravityScale = 0f;

        if (isResultOfFission)
        {
            FallDown();
        }
        else
        {
            float direction = leftAndRight[Random.Range(0, 2)];
            float screenOffset = Game.Instance.screenWidth * 1.3f;
            transform.position = new Vector2(screenOffset * direction, transform.position.y);

            rb.velocity = new Vector2(-direction, 0f);
            // Push meteor down after a few seconds
            Invoke("FallDown", Random.Range(screenOffset - 2.5f, screenOffset - 1f));
        }
    }

    private void FallDown()
    {
        isShowing = false;
        rb.gravityScale = 1f;
        rb.AddTorque(Random.Range(-20f, 20f));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("cannon"))
        {
            // Game over
            Debug.Log("Game over");
            Destroy(other.gameObject); // Destroy the Cannon game object
            Destroy(gameObject); // Destroy the Meteor game object
        }

        if (other.CompareTag("missile"))
        {
            // Take damage
            TakeDamage(1);
            // Destroy missile
            Missiles.Instance.DestroyMissile(other.gameObject);
        }

        if (!isShowing && other.CompareTag("wall"))
        {
            // Hit wall
            float posX = transform.position.x;
            if (posX > 0)
            {
                // Hit right wall
                rb.AddForce(Vector2.left * 150f);
            }
            else
            {
                // Hit left wall
                rb.AddForce(Vector2.right * 150f);
            }

            rb.AddTorque(posX * 4f);
        }

        if (other.CompareTag("ground"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            rb.AddTorque(-rb.angularVelocity * 4f);
        }
    }

    public void TakeDamage(int damage)
    {
        if (health > 1)
        {
            health -= damage;
        }
        else
        {
            Die();
        }
        UpdateHealthUI();
    }

    protected virtual void Die()
    {
        Destroy(gameObject);
    }

    private void UpdateHealthUI()
    {
        textHealth.text = health.ToString();
    }
}
