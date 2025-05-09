using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }        //싱글톤 인스턴스 변수
    

    //인스펙터에서 가져올 수 있도록 시리얼라이즈필드 설정
    [SerializeField] private PlayerState _player;       //게임오버를 위한 player 가져옴
    [SerializeField] private Slider survivalTimeSlider;          //살아남는 시간 표현 슬라이더
    [SerializeField] private Text scoreTxt;     //점수 텍스트



    [SerializeField] private bool _isGameOver = false;     //게임오버 확인 불리언
    [SerializeField] private bool _isStageClear = false;   //스테이지 클리어 확인 불리언
    [SerializeField] private int _score = 0;    //점수 변수

    //점수 프로퍼티
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private int _currentStage = 1;        //스테이지 번호
    //스테이지 번호 프로퍼티
    public int currentStage { get { return _currentStage; } }

    [SerializeField] private float _baseSurvivalTime = 30f;     //버텨야 하는 시간 default
    [SerializeField] private float _requiredSurvivalTime = 0f;  //실제 버텨야하는 시간
    [SerializeField] private float _increaseDuration = 10f;  //스테이지 거듭할 수록 늘어날 시간 증가값
    [SerializeField] private int _increaseSpeed = 1;   //스테이지 거듭할 수록 스피드 증가값



    //게임 매니저 싱글톤 패턴
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
        //시작할때 슬라이더 밸류값 설정
        survivalTimeSlider.minValue = 0;                
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;


    }

    private void Update()
    {


        //디버그용 점수 추가
        if (Input.GetKeyDown(KeyCode.A))
        {
            AddScore(1);
        }

        //디버그용 스테이지 스타트 
        if (_isStageClear && Input.GetKeyDown(KeyCode.Space))
        {
            StageStart();
        }

        //게임오버나 스테이지 클리어가 true면 아무것도 하지 않고 리턴
        if (_isGameOver || _isStageClear)
        {
            return;
        }
        _requiredSurvivalTime = Mathf.Max(0f, _requiredSurvivalTime - Time.deltaTime);     //버텨야 되는 시간을 deltaTime만큼 뺌(0보다 낮은 수가 나오지 않게 하게 최저값을 0으로 지정)
        survivalTimeSlider.value = _requiredSurvivalTime;           //슬라이더에 현재 남은 생존 시간을 시작적으로 반영


        //버텨야 하는 시간이 0이 되면 클리어
        if (_requiredSurvivalTime <= 0)  
        {
            StageClear();
            return;
        }

        //게임오버 로직
        if (_player.CurrentHealth <= 0)
        {
            GameOver();
            return;
        }
    }

    //스테이지 시작시 초기화
    private void StageStart()
    {
        _requiredSurvivalTime = _baseSurvivalTime + (_increaseDuration * (_currentStage - 1));      //스테이지마다 버텨야하는 시간값을 갱신

        //시작할때 슬라이더 밸류값 설정
        survivalTimeSlider.minValue = 0;
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;

        //불값 초기화
        _isStageClear = false;      
        _isGameOver = false;
    }


    //게임오버 메드
    //TODO:게임오버 씬 혹은 UI를 만들고 켜주기
    public void GameOver()
    {
        _isGameOver = true;
        Debug.Log("GameOver");
    }


    //스테이지 클리어 메서드
    //TODO:스테이지 클리어 UI를 만들고(다음 스테이지로 갈지 스타트씬?으로 갈지 결정) 켜주기
    public void StageClear()
    {
        _isStageClear = true;
        Debug.Log("StageClear");
        _currentStage += 1;             //스테이지 ++

        _player.PlayerSpeed += _increaseSpeed * _currentStage;              //플레이어 속도는 증가값 * 스테이지(추후에 변경해야 될 사항)


    }


    //점수 추가 메서드
    public void AddScore(int score)
    {
        _score += score;
        scoreTxt.text = _score.ToString();       //추가된 스코어 텍스트로 변환
    }

    //로비 -> 게임으로 들어갈때 완전 초기화 메서드
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
        return 3f; // 현재는 임시값, 추후 게임 진행에 따라 증가하도록 변경 가능
    }



    //TODO: UI만들고 연결하기(UI매니저로 실시)


}
