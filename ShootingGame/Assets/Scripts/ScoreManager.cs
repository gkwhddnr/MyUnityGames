using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // 현재 점수 UI
    public Text currentScoreUI;
    // 현재 점수
    private int currentScore;

    // 최고 점수 UI
    public Text bestScoreUI;
    // 최고 점수
    private int bestScore;

    // 싱글턴 객체
    public static ScoreManager Instance = null;

    // 싱글턴 객체에 값이 없으면 생성된 자기 자신 할당
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }    
    }

    public int Score
    {
        get
        {
            return currentScore;
        }
        set
        {
            // 씬에서 ScoreManager 클래스 속성 값 할당
            currentScore = value;

            // 화면에 현재 점수 표시
            currentScoreUI.text = "현재 점수 : " + currentScore;

            // 현재 점수가 최고 점수보다 크면
            if (currentScore > bestScore)
            {
                // 최고 점수 갱신
                bestScore = currentScore;

                // 최고 점수 UI 표시
                bestScoreUI.text = "최고 점수 : " + bestScore;

                // 최고 점수 저장
                PlayerPrefs.SetInt("Best Score", bestScore);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 최고 점수 불러오기
        bestScore = PlayerPrefs.GetInt("Best Score", 0);

        // 최고 점수 화면 표시
        bestScoreUI.text = "최고 점수 : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    // currentScore 값 넣고 화면 표시
    public void SetScore(int value)
    {
        // 씬에서 ScoreManager 클래스 속성 값 할당
        currentScore = value;

        // 화면에 현재 점수 표시
        currentScoreUI.text = "현재 점수 : " + currentScore;
       
        // 현재 점수가 최고 점수보다 크면
        if (currentScore > bestScore)
        {
            // 최고 점수 갱신
            bestScore =  currentScore;

            // 최고 점수 UI 표시
            bestScoreUI.text = "최고 점수 : " + bestScore;

            // 최고 점수 저장
            PlayerPrefs.SetInt("Best Score", bestScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
    */
}
