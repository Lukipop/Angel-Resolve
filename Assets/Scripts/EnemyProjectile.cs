using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    private float destructionPoint = -7f;
   [SerializeField] private float speed;
    private GameManager gameManager;
    private Rigidbody2D rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2 (0, -speed);
        if (gameObject.transform.position.y <= destructionPoint || gameManager.isGameReset == true)
        {
            Destroy(gameObject);
        }
    }
}
