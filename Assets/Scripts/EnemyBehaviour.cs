using System.Collections;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private float moveDistance;
    [SerializeField] private float minShootInterval;
    [SerializeField] private float maxShootInterval;
    public GameObject WrathProjectile;
    public GameObject LustProjectile;
    public GameObject GluttonyProjectile;
    private SpawnManager spawnManager;
    private GameManager gameManager;
    private Vector3 startPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnManager = FindAnyObjectByType<SpawnManager>();
        gameManager = FindAnyObjectByType<GameManager>();
        InvokeRepeating("MoveDown", 0f, spawnManager.moveInterval);
        startPos = transform.position;

        StartCoroutine(ShootRandomly());
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            gameManager.GainScore(10);
            Destroy(gameObject);
            
        }
    }

    /// <summary>
    /// makes the enemy constantly move downwards
    /// </summary>
    private void MoveDown()
    {
        transform.position += Vector3.down * moveDistance;
    }

    private void OnDestroy()
    {
        if (spawnManager != null)
        {
            spawnManager.RemoveEnemyFromList(gameObject);
        }
        
    }

    /// <summary>
    /// Resets the enemy's position to it's original starting position
    /// </summary>
    public void ResetPosition()
    {
        transform.position = startPos;
    }

    /// <summary>
    /// A courotine for allowing the enemy to shoot towards the player at random intervals
    /// </summary>
    /// <returns></returns>
    private IEnumerator ShootRandomly()
    {
        while (true)
        {
            float randomInterval = Random.Range(minShootInterval, maxShootInterval);
            yield return new WaitForSeconds(randomInterval);

            if (CompareTag("Wrath"))
            {
                ShootProjectile(WrathProjectile);
            }
            else if (CompareTag("Gluttony"))
            {
                ShootProjectile(GluttonyProjectile);
                
            }
            else if (CompareTag("Lust"))
            {
                ShootProjectile(LustProjectile);
               
            }
            
        }
    }

    /// <summary>
    /// Shoots a projectile from the enemy's position
    /// </summary>
    private void ShootProjectile(GameObject projectile)
    {
        if (projectile != null)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
        }
    }

}
