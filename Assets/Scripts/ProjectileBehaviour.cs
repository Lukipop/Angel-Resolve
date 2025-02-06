using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [SerializeField] private float destructionPoint;
    private PlayerController playercontroller;
    private GameManager gamemanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playercontroller = FindAnyObjectByType<PlayerController>();
        gamemanager = FindAnyObjectByType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position.y >= destructionPoint || gamemanager.isGameReset)
        {
            playercontroller.isProjectileActive = false;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wrath") || collision.gameObject.CompareTag("Gluttony") || collision.gameObject.CompareTag("Lust"))
        {
            playercontroller.isProjectileActive = false;
            Destroy(gameObject);
        }
    }
}
