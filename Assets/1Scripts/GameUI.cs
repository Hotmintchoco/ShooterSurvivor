using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    public Image fadePlane;
    public GameObject gameOverUI;
    public RectTransform healthBar;
    public RectTransform expBar;
    public TextMeshProUGUI timer;

    Player player;

    void Start()
    {
        player = FindAnyObjectByType<Player>();
        FindAnyObjectByType<Player>().OnDeath += OnGameOver;
    }

    void Update()
    {
        float healthPercent = 0;
        float ExpPercent = 0;
        if (player != null)
        {
            var instance = GameManager.instance;
            healthPercent = player.health / player.startingHealth;
            ExpPercent = Mathf.Min((float)instance.exp / instance.nextExp[instance.level], 1);
        }
        healthBar.localScale = new Vector3(healthPercent, 1, 1);
        expBar.localScale = new Vector3(1, ExpPercent, 1);

        float nowTime = GameManager.instance.gameTime;
        int min = Mathf.FloorToInt(nowTime / 60);
        int sec = Mathf.FloorToInt(nowTime % 60);
        timer.text = min.ToString("D2") + ":" + sec.ToString("D2"); 
    }

    void OnGameOver()
    {
        StartCoroutine(Fade(Color.clear, Color.black, 1));
        gameOverUI.SetActive(true); 
    }

    IEnumerator Fade(Color from, Color to, float time)
    {
        float speed = 1 / time;
        float percent = 0;

        while (percent < 1)
        {
            percent += Time.deltaTime * speed;
            fadePlane.color = Color.Lerp(from, to, percent);
            yield return null;
        }
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("Game");
    }
}
