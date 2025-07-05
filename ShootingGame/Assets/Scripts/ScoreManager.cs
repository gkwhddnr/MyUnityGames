using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    // ���� ���� UI
    public Text currentScoreUI;
    // ���� ����
    private int currentScore;

    // �ְ� ���� UI
    public Text bestScoreUI;
    // �ְ� ����
    private int bestScore;

    // �̱��� ��ü
    public static ScoreManager Instance = null;

    // �̱��� ��ü�� ���� ������ ������ �ڱ� �ڽ� �Ҵ�
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
            // ������ ScoreManager Ŭ���� �Ӽ� �� �Ҵ�
            currentScore = value;

            // ȭ�鿡 ���� ���� ǥ��
            currentScoreUI.text = "���� ���� : " + currentScore;

            // ���� ������ �ְ� �������� ũ��
            if (currentScore > bestScore)
            {
                // �ְ� ���� ����
                bestScore = currentScore;

                // �ְ� ���� UI ǥ��
                bestScoreUI.text = "�ְ� ���� : " + bestScore;

                // �ְ� ���� ����
                PlayerPrefs.SetInt("Best Score", bestScore);
            }
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // �ְ� ���� �ҷ�����
        bestScore = PlayerPrefs.GetInt("Best Score", 0);

        // �ְ� ���� ȭ�� ǥ��
        bestScoreUI.text = "�ְ� ���� : " + bestScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*
    // currentScore �� �ְ� ȭ�� ǥ��
    public void SetScore(int value)
    {
        // ������ ScoreManager Ŭ���� �Ӽ� �� �Ҵ�
        currentScore = value;

        // ȭ�鿡 ���� ���� ǥ��
        currentScoreUI.text = "���� ���� : " + currentScore;
       
        // ���� ������ �ְ� �������� ũ��
        if (currentScore > bestScore)
        {
            // �ְ� ���� ����
            bestScore =  currentScore;

            // �ְ� ���� UI ǥ��
            bestScoreUI.text = "�ְ� ���� : " + bestScore;

            // �ְ� ���� ����
            PlayerPrefs.SetInt("Best Score", bestScore);
        }
    }

    public int GetScore()
    {
        return currentScore;
    }
    */
}
