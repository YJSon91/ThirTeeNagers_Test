using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }        //�̱��� �ν��Ͻ� ����
    

    //�ν����Ϳ��� ������ �� �ֵ��� �ø���������ʵ� ����
    [SerializeField] private PlayerState _player;       //���ӿ����� ���� player ������
    [SerializeField] private Slider survivalTimeSlider;          //��Ƴ��� �ð� ǥ�� �����̴�
    [SerializeField] private Text scoreTxt;     //���� �ؽ�Ʈ



    [SerializeField] private bool _isGameOver = false;     //���ӿ��� Ȯ�� �Ҹ���
    [SerializeField] private bool _isStageClear = false;   //�������� Ŭ���� Ȯ�� �Ҹ���
    [SerializeField] private int _score = 0;    //���� ����

    //���� ������Ƽ
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private int _currentStage = 1;        //�������� ��ȣ
    //�������� ��ȣ ������Ƽ
    public int currentStage { get { return _currentStage; } }

    [SerializeField] private float _baseSurvivalTime = 30f;     //���߾� �ϴ� �ð� default
    [SerializeField] private float _requiredSurvivalTime = 0f;  //���� ���߾��ϴ� �ð�
    [SerializeField] private float _increaseDuration = 10f;  //�������� �ŵ��� ���� �þ �ð� ������
    [SerializeField] private int _increaseSpeed = 1;   //�������� �ŵ��� ���� ���ǵ� ������



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
        //�����Ҷ� �����̴� ����� ����
        survivalTimeSlider.minValue = 0;                
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;


    }

    private void Update()
    {


        //����׿� ���� �߰�
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddScore(1);
        }

        //����׿� �������� ��ŸƮ 
        if (_isStageClear && Input.GetKeyDown(KeyCode.Space))
        {
            StageStart();
        }

        //���ӿ����� �������� Ŭ��� true�� �ƹ��͵� ���� �ʰ� ����
        if (_isGameOver || _isStageClear)
        {
            return;
        }
        _requiredSurvivalTime = Mathf.Max(0f, _requiredSurvivalTime - Time.deltaTime);     //���߾� �Ǵ� �ð��� deltaTime��ŭ ��(0���� ���� ���� ������ �ʰ� �ϰ� �������� 0���� ����)
        survivalTimeSlider.value = _requiredSurvivalTime;           //�����̴��� ���� ���� ���� �ð��� ���������� �ݿ�


        //���߾� �ϴ� �ð��� 0�� �Ǹ� Ŭ����
        if (_requiredSurvivalTime <= 0)  
        {
            StageClear();
            return;
        }

        //���ӿ��� ����
        if (_player.CurrentHealth <= 0)
        {
            GameOver();
            return;
        }
    }

    //�������� ���۽� �ʱ�ȭ
    private void StageStart()
    {
        _requiredSurvivalTime = _baseSurvivalTime + (_increaseDuration * (_currentStage - 1));      //������������ ���߾��ϴ� �ð����� ����

        //�����Ҷ� �����̴� ����� ����
        survivalTimeSlider.minValue = 0;
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;

        //�Ұ� �ʱ�ȭ
        _isStageClear = false;      
        _isGameOver = false;
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
        _currentStage += 1;             //�������� ++

        _player.PlayerSpeed += _increaseSpeed * _currentStage;              //�÷��̾� �ӵ��� ������ * ��������(���Ŀ� �����ؾ� �� ����)


    }


    //���� �߰� �޼���
    public void AddScore(int score)
    {
        _score += score;
        scoreTxt.text = _score.ToString();       //�߰��� ���ھ� �ؽ�Ʈ�� ��ȯ
    }

    //�κ� -> �������� ���� ���� �ʱ�ȭ �޼���
    public void InitGame()
    {
        _isStageClear = false;
        _isGameOver = false;
        _currentStage = 1;
        _requiredSurvivalTime = 0f;
        _score = 0;
        _player.PlayerSpeed = 3;
    }

    public float GetCurrentGameSpeed()
    {
        return 3f; // ����� �ӽð�, ���� ���� ���࿡ ���� �����ϵ��� ���� ����
    }



    //TODO: UI����� �����ϱ�(UI�Ŵ����� �ǽ�)


}
