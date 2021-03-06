using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text BestScoreText;
    public InputField playerNameInput;
    public GameObject GameOverText;
    
    private bool m_Started = false;
    private int m_Points;
    public int bestPoints;
    private static string playerName;
    public string bestPlayerName;
    
    private bool m_GameOver = false;

    public static MainManager Instance;
    public Color BoxColor;

    public GameObject GameOverNameEntry;

    //this is to make sure the MainManager is the only instance in the game
    

    // Start is called before the first frame update
    void Start()
    {
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
                float randomDirection = Random.Range(-1.0f, 1.0f);
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        m_GameOver = true;
        GameOverText.SetActive(true);
        GameOverNameEntry.SetActive(true);

        if (m_Points > bestPoints)
        {
            bestPoints = m_Points;
            bestPlayerName = playerName;
        }
    }

    public void UpdateHighScore()
    {
        BestScoreText.text = $"Best Score : {bestPlayerName} at {bestPoints}";
    }

    [System.Serializable]
    class SaveData
    {
        public TextMesh highScores;
    }
    
    public void SaveHighScores()
    {
        SaveData data = new SaveData();
    }

    public void LoadHighScores()
    {

    }
}
