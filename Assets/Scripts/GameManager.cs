using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] enemies; // Array con tres prefabs de enemigos
    public Transform spawnPoint;
    public float maxSpawnPointX;
    public TMP_Text scoreText;
    public GameObject menuPanel;
    public TMP_Text highScoreText;

    int score = 0;
    int highScore = 0;

    bool gameStarted = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        //else
        //{
        //    Destroy(gameObject); // Esto es opcional, pero asegura que no haya múltiples instancias.
        //}
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("highScore"))
        {
            highScore = PlayerPrefs.GetInt("highScore");
            highScoreText.text = "Last: " + highScore.ToString();
        }
    }

    void Update()
    {
        if (Input.anyKeyDown && !gameStarted)
        {
            menuPanel.SetActive(false);
            scoreText.gameObject.SetActive(true);
            StartCoroutine("SpawnEnemies");
            gameStarted = true;
        }
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.8f); // Intervalo entre spawns
            Spawn();
        }
    }

    public void Spawn()
    {
        // Seleccionar aleatoriamente uno de los tres enemigos
        int randomIndex = Random.Range(0, enemies.Length);
        GameObject selectedEnemy = enemies[randomIndex];

        // Determinar la posición de spawn
        float randomSpawnX = Random.Range(-maxSpawnPointX, maxSpawnPointX);
        Vector3 enemySpawnPos = spawnPoint.position;
        enemySpawnPos.x = randomSpawnX;

        // Instanciar el enemigo seleccionado
        Instantiate(selectedEnemy, enemySpawnPos, Quaternion.identity);
    }

    public void Restart()
    {
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("highScore", highScore);
        }
        SceneManager.LoadScene(0);
    }

    public void ScoreUp()
    {
        score++;
        scoreText.text = score.ToString();
    }
}
