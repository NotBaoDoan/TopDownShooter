using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    //Variable declaration
    Rigidbody2D rb;
    Vector2 moveInput;
    Vector2 screenBoundary;
    [SerializeField] int playerHealth = 5;
    [SerializeField] float invincibleTime = 3f;
    [SerializeField] float moveSpeed = 3;
    [SerializeField] float rotationSpeed = 700f;
    [SerializeField] float bulletSpeed = 7f;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject gun;
    bool invincible;
    float targetAngle;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        screenBoundary = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

    //Other functions and methods
    private void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnAttack()
    {
        Rigidbody2D bulletRB = Instantiate(bullet, transform.position, transform.rotation).GetComponent<Rigidbody2D>();
        bulletRB.AddForce(transform.up * bulletSpeed, ForceMode2D.Impulse);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        if (moveInput != Vector2.zero)
        {
            targetAngle = Mathf.Atan2(moveInput.y, moveInput.x) * Mathf.Rad2Deg;
        }

        transform.position = new Vector2(Mathf.Clamp(transform.position.x, -screenBoundary.x, screenBoundary.x),
                                         Mathf.Clamp(transform.position.y, -screenBoundary.y, screenBoundary.y));
    }

    void FixedUpdate()
    {
        float rotation = Mathf.MoveTowardsAngle(rb.rotation, targetAngle - 90, rotationSpeed * Time.fixedDeltaTime);
        rb.MoveRotation(rotation);
    }

    void ResetInvincibility()
    {
        invincible = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemies") && !invincible)
        {
            if (playerHealth <= 1)
            {
                Destroy(gameObject);
            }
            else
            {
                playerHealth--;
                invincible = true;
                Invoke("ResetInvincibility", invincibleTime);
                Debug.Log("Player health: " + playerHealth);
            }
        }
    }
}
