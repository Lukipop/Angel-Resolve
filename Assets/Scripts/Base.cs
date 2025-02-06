using System.Collections;
using UnityEngine;

public class Base : MonoBehaviour
{
    private PlayerController playerController;
    private GameManager gameManager;
    private bool hasReducedHealth;
    [SerializeField] private float healthReductionCooldown;
    private void Start()
    {
        playerController = FindAnyObjectByType<PlayerController>();
        gameManager = FindAnyObjectByType<GameManager>();
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wrath") || collision.gameObject.CompareTag("Gluttony") || collision.gameObject.CompareTag("Lust") && !hasReducedHealth)
        {
            playerController.health--;
            playerController.ResetPlayerPosition();
            ResetAllEnemies();
            StartCoroutine(healthReductionCooldownCount());
        }
    }

    /// <summary>
    /// Resets all enemies to their starting position
    /// </summary>
    public void ResetAllEnemies()
    {
        EnemyBehaviour[] allEnemies = FindObjectsByType<EnemyBehaviour>(FindObjectsSortMode.None);

        foreach (EnemyBehaviour enemy in allEnemies)
        {
            enemy.ResetPosition();
        }
    }

    /// <summary>
    /// A courotine for setting up a cooldown to prevent multiple health loses
    /// </summary>
    /// <returns></returns>
    public IEnumerator healthReductionCooldownCount()
    {
        hasReducedHealth = true;
        gameManager.isGameReset = true;
        yield return new WaitForSeconds(healthReductionCooldown);
        hasReducedHealth = false;
        gameManager.isGameReset = false;
    }
}
