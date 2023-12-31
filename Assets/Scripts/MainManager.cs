using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public GameObject GameOverText;

    public TMPro.TMP_Text bestScoreText;
    private string bestScorer = "Unknown";
    private int bestScore = 0;

    private bool m_Started = false;
    private int m_Points;
    
    private bool m_GameOver = false;

    [Serializable]
    class BestScoreData
    {
        public int score;
        public string player;
    }

    // Start is called before the first frame update
    void Start()
    {
        LoadBestScore();

        bestScoreText.text = "Best Score: \"" + bestScorer + "\", " + bestScore;

        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = UnityEngine.Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        if (m_Points >= bestScore)
        {
            UpdateBestScore(NameCarrier.instance.GetPlayerName(), m_Points);
        }

        m_GameOver = true;
        GameOverText.SetActive(true);
    }

    private void UpdateBestScore(string name, int score)
    {
        bestScorer = name;
        bestScore = score;
        bestScoreText.text = "Best Score: \"" + bestScorer + "\", " + bestScore;

        SaveBestScore();
    }

    public void SaveBestScore()
    {
        if (NameCarrier.instance != null)
        {
            BestScoreData data = new BestScoreData();
            data.player = NameCarrier.instance.GetPlayerName();
            data.score = m_Points;

            string json = JsonUtility.ToJson(data);
            System.IO.File.WriteAllText(Application.persistentDataPath + "/score.json", json);
        }
        else
        {
            Debug.Log("Error while getting instance of NameCarrier!");
        }
    }

    public void LoadBestScore()
    {
        string path = Application.persistentDataPath + "/score.json";

        if(System.IO.File.Exists(path))
        {
            string json = System.IO.File.ReadAllText(path);
            BestScoreData data = JsonUtility.FromJson<BestScoreData>(json);

            bestScore = data.score;
            bestScorer = data.player;
        }
    }
}
