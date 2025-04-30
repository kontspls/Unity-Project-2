
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEditor;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    public Text bestScore;
    public GameObject GameOverText;
    public GameObject backToMain;
    public static MainManager Instance;


    private bool m_Started = false;
    private int m_Points;
    
    public static bool m_GameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        bestScore.text = $"Highest Score: {NameSelector.Instance.bestPlayer} - {NameSelector.Instance.highScore}";
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
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
#if UNITY_EDITOR
                EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"{NameSelector.Instance.playerName}'s Score is " + m_Points;
    }

    public void GameOver()
    {
        if (m_Points >= NameSelector.Instance.highScore)
        {
            NameSelector.Instance.highScore = m_Points;
            NameSelector.Instance.SaveHighScore();
        }
        m_GameOver = true;
        GameOverText.SetActive(true);
        backToMain.SetActive(true);
    }
}
