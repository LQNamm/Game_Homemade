using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Game State")]
    public bool isGameStarted { get; private set; } = false;
    public bool isGameOver { get; private set; } = false;

    [Header("Game Objects")]
    public GameObject gameOverImage;
    public PipeSpawner pipeSpawner;

    [Header("Score Display")]
    public Sprite[] numberSprites;     // Sprite số từ 0 đến 9
    public Image[] scoreDigits;        // UI Image cho từng chữ số

    [Header("Audio Clips")]
    public AudioClip wingSound;
    public AudioClip swooshSound;
    public AudioClip pointSound;
    public AudioClip hitSound;
    public AudioClip dieSound;

    private AudioSource audioSource;

    private int score = 0;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        // Singleton
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        Time.timeScale = 1f;
    }

    private void Start()
    {
        gameOverImage.SetActive(false);
        UpdateScoreDisplay();
    }

    private void Update()
    {
        if (!isGameStarted && !isGameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                PlaySound(wingSound);
                StartGame();
            }
        }
        else if (isGameOver)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                PlaySound(swooshSound);
                RestartGame();
            }
        }
    }


    public void PlaySound(AudioClip clip)
    {
        if (clip != null)
            audioSource.PlayOneShot(clip);
    }

    public void StartGame()
    {
        isGameStarted = true;

        Bird bird = FindObjectOfType<Bird>();
        if (bird != null) bird.StartFlying();

        if (pipeSpawner != null) pipeSpawner.StartSpawning();
    }

    public void GameOver()
    {
        isGameOver = true;

        PlaySound(hitSound);     // Phát âm khi va chạm
        PlaySound(dieSound);     // Phát âm khi chết     

        gameOverImage.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    public void AddScore(int amount = 1)
    {
        if (isGameOver) return;
        PlaySound(pointSound);
        score += amount;
        UpdateScoreDisplay();
    }

    private void UpdateScoreDisplay()
    {
        string scoreStr = score.ToString().PadLeft(scoreDigits.Length, '0');

        for (int i = 0; i < scoreDigits.Length; i++)
        {
            int digit = scoreStr[i] - '0';
            scoreDigits[i].sprite = numberSprites[digit];
        }
    }
}
