using UnityEngine;

public class Enemy : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] float moveSpeed = 3f;
    Rigidbody2D rb;
    Vector2 direction;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerTransform = GameObject.FindWithTag("Player").transform;
    }
     
    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            direction = (playerTransform.position - transform.position).normalized;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }
}
