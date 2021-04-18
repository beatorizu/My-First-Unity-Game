using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUI : MonoBehaviour
{
    public GameObject startPanel, HUD, pausePanel, endPanel;
    public Button pauseBtn, playBtn, playAgainBtn, nextBtn, exitBtn;
    public Text levelTxt;
    private bool started;
    private int level;

    public static GUI Insance;

    void Awake()
    {
        if (Insance == null) Insance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(this);

        startPanel.SetActive(true);
        HUD.SetActive(false);
        pausePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        pauseBtn.onClick.AddListener(Pause);
        playBtn.onClick.AddListener(Play);
        nextBtn.onClick.AddListener(NextLevel);
        playAgainBtn.onClick.AddListener(Replay);
        exitBtn.onClick.AddListener(Quit);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started && Input.anyKey)
        {
            started = true;
            startPanel.SetActive(false);
            HUD.SetActive(true);
            int currentLevel = level + 1;
            levelTxt.text = "Level: " + currentLevel;
        }
    }

    void Play()
    {
        HUD.SetActive(true);
        pausePanel.SetActive(false);

        Time.timeScale = 1;
    }

    void Pause()
    {
        pausePanel.SetActive(true);
        HUD.SetActive(false);

        Time.timeScale = 0;
    }

    public void EndGame()
    {
        HUD.SetActive(false);
        endPanel.SetActive(true);
    }

    void NextLevel()
    {
        endPanel.SetActive(false);
        HUD.SetActive(true);

        level++;

        int currentLevel = level + 1;
        levelTxt.text = "Level: " + currentLevel;

        if (SceneManager.sceneCount <= level)
        {
            Ball.stop = false;
            SceneManager.LoadScene(level);
        }
    }

    void Replay()
    {
        endPanel.SetActive(false);
        HUD.SetActive(true);
        SceneManager.LoadScene(level);
        Ball.stop = false;
    }

    void Quit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}
