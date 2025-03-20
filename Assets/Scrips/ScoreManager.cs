using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [SerializeField]
    private GameObject _floatingPointsPrefab;

    private int _currentScore = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }

    public void ClearCurrentScore()
    {
        _currentScore = 0;
    }

    public void UpdateKillScore(Vector3 pos, int score)
    {
        ChangeScore(score);
        CreateFloatingScore(score, pos);
    }

    private void ChangeScore(int scoreChange)
    {
        _currentScore += scoreChange;
        Debug.Log("Score: " + _currentScore);
        // Update UI
    }

    private void CreateFloatingScore(int score, Vector3 pos)
    {
        GameObject floatingPoints = Instantiate(_floatingPointsPrefab, pos, Quaternion.identity);
        TextMesh textMesh = floatingPoints.GetComponentInChildren<TextMesh>();
        textMesh.text = (score >= 0 ? "+" : "") + score;
        //textMesh.color = (score >= 0) ? Color.magenta : Color.red;
        Destroy(floatingPoints, 1f);
    }
}
