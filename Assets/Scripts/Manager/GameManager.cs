using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }        //�̱��� �ν��Ͻ� ����

    //���� ����
    [SerializeField] private PlayerState _player;       //���ӿ����� ���� player ������



    [SerializeField] private float _stageTimer = 0;    //�ð� ����
    [SerializeField] private bool _isGameOver = false;     //���ӿ��� Ȯ�� �Ҹ���
    [SerializeField] private bool _isStageClear = false;   //�������� Ŭ���� Ȯ�� �Ҹ���
    [SerializeField] private int _score = 0;    //���� ����
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }
    [SerializeField] private int _currentStage = 1;        //�������� ��ȣ
    public int currentStage { get { return _currentStage; } }
    [SerializeField] private float _requiredSurvivalTime = 30f;   //���߾� �ϴ� �ð� default
    [SerializeField] private float _increaseDuration = 10f;  //�������� �ŵ��� ���� �þ �ð� ������



    //���� �Ŵ��� �̱��� ����
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        
    }

    private void Update()
    {
        //���ӿ����� �������� Ŭ��� true�� �ƹ��͵� ���� �ʰ� ����
        if (_isGameOver || _isStageClear)
        {
            return;
        }
        _stageTimer += Time.deltaTime;      //���������� ���ӵɶ� deltatime���� ������

        //Debug.Log(_time);

        //��������Ÿ�̸Ӱ� ���߾��ϴ� �ð����� �������� �������� Ŭ����
        if (_stageTimer >= _requiredSurvivalTime)  
        {
            StageClear();
        }

        //���� ����
        //���ӿ��� ����
        if (_player.CurrentHealth <= 0)
        {
            GameOver();
        }

    }

    //�������� ���۽� �ʱ�ȭ
    public void StageStart()
    {
        _isStageClear = false;
        _isGameOver = false;
        _stageTimer = 0;
    }


    //���ӿ��� �޵�
    //TODO:���ӿ��� �� Ȥ�� UI�� ����� ���ֱ�
    public void GameOver()
    {
        _isGameOver = true;
        Debug.Log("GameOver");
    }


    //�������� Ŭ���� �޼���
    //TODO:�������� Ŭ���� UI�� �����(���� ���������� ���� ��ŸƮ��?���� ���� ����) ���ֱ�
    public void StageClear()
    {
        _isStageClear = true;
        Debug.Log("StageClear");
        _currentStage += 1;
        _requiredSurvivalTime += _increaseDuration;
    }


    //���� �߰� �޼���
    public void AddScore(int score)
    {
        _score += score;
    }

    //�κ� -> �������� ���� ���� �ʱ�ȭ �޼���
    public void InitGame()
    {
        _isStageClear = false;
        _isGameOver = false;
        _stageTimer = 0f;
        _currentStage = 0;
        _requiredSurvivalTime = 0f;
        _score = 0;
    }




    //TODO:���ӿ��� ���� ����� , UI����� �����ϱ�(UI�Ŵ����� �ǽ�)
    //player��ũ��Ʈ ���� ��([serializeField]�� ������ ���� �� �ν����Ϳ��� ���� ����
   
}
