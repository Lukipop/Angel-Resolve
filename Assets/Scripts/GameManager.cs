using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    [SerializeField] private int pointsTowardsLife;
    private const int POINTSFORLIFE = 500;
    private const int BONUSPOINTS = 100;
    public bool isGameReset;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthText;
    private PlayerController playerController;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        playerController = FindAnyObjectByType<PlayerController>();
    }
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SetUI();
    }

    /// <summary>
    /// Sets up the UI for the score on screen
    /// </summary>
    public void SetUI()
    {
        scoreText.SetText($"Score:{score}");
        healthText.SetText($"Lives: {playerController.health}");
    }

    /// <summary>
    /// Adds a certain amount to the total score amount
    /// </summary>
    /// <param name="addedScore"></param>
    public void GainScore(int addedScore)
    {
        score += addedScore;
        pointsTowardsLife += addedScore;

        if(pointsTowardsLife == POINTSFORLIFE)
        {
            pointsTowardsLife = 0;
            if(playerController.health == 3)
            {
                score += BONUSPOINTS;
            }
            else if (playerController.health < 3)
            {
                playerController.health++;
            }
        }
    }
}
