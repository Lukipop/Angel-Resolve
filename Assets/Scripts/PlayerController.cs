using System.Collections;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    private float originalMoveSpeed;
    public int health;
    [SerializeField] private float fireRate;
    [SerializeField] private float cooldown;
    public GameObject projectilePerfab;
    public bool isProjectileActive;
    private bool isGluttonyCooldown;
    private bool isLustCooldown;
    private Vector3 startingPos = new Vector3(0, -6, 0);
    private Vector3 originalScale;
    private Base baseManager;


    private void Start()
    {
        originalMoveSpeed = moveSpeed;
        originalScale = transform.localScale;
        health = 3;
        transform.position = startingPos;
        baseManager = FindAnyObjectByType<Base>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            MovePlayerLeft();
            ClampPlayerPosition(-12f, 12f);
        }
        if (Input.GetKey(KeyCode.D))
        {
            MovePlayerRight();
            ClampPlayerPosition(-12f, 12f);
        }
        if (Input.GetKeyDown(KeyCode.Space) && !isProjectileActive)
        {
            ShootProjectile(18f);
        }
    }
    /// <summary>
    /// Makes the player move right
    /// </summary>
    private void MovePlayerRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// Makes the player move left
    /// </summary>
    private void MovePlayerLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
    /// <summary>
    /// Ensures the player doesn't go out of the screen on the X axis
    /// </summary>
    private void ClampPlayerPosition(float minX, float maxX)
    {
        float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
        transform.position = new Vector3(clampedX, transform.position.y, transform.position.z);
    }
    /// <summary>
    /// Creates a projectile that flies upwards from the player's position
    /// </summary>
    private void ShootProjectile(float projectileSpeed)
    {
        GameObject projectile = Instantiate(projectilePerfab, transform.position, Quaternion.identity);
        Rigidbody2D projectileRB = projectile.GetComponent<Rigidbody2D>();
        if (projectileRB != null)
        {
            projectileRB.linearVelocity = Vector2.up * projectileSpeed;
        }
        isProjectileActive = true;
    }

    /// <summary>
    /// Resets the player's position
    /// </summary>
    public void ResetPlayerPosition()
    {
        transform.position = startingPos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Destroy(collision.gameObject);
            health--;
            baseManager.ResetAllEnemies();
            StartCoroutine(baseManager.healthReductionCooldownCount());
            transform.position = startingPos;
        }
        else if (collision.gameObject.CompareTag("GluttonyProjectile"))
        {
            Destroy(collision.gameObject);
            if (!isGluttonyCooldown)
            {
                StartCoroutine(GrowPlayer());
            }
        }
        else if (collision.gameObject.CompareTag("LustProjectile"))
        {
            Destroy(collision.gameObject);
            if (!isLustCooldown)
            {
                StartCoroutine(InvertPlayerMovement());
            }
        }
        
    }

    private IEnumerator GrowPlayer()
    {
        isGluttonyCooldown = true;
        transform.localScale += new Vector3(2f, 0, 0);
        yield return new WaitForSeconds(5);
        transform.localScale = originalScale;
        isGluttonyCooldown = false;
    }

    private IEnumerator InvertPlayerMovement()
    {
        isLustCooldown = true;
        moveSpeed =-originalMoveSpeed;
        yield return new WaitForSeconds(7);
        moveSpeed = originalMoveSpeed;
        isLustCooldown = false;
    }
}
