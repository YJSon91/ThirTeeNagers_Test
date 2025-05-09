using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }        //싱글톤 인스턴스 변수

    //아직 미정
    //[SerializeField] private Player _player;       //게임오버 및 대미지 처리를 위한 player 가져옴



    [SerializeField] private float _stageTimer = 0;    //시간 변수
    [SerializeField] private bool _isGameOver = false;     //게임오버 확인 불리언
    [SerializeField] private bool _isStageClear = false;   //스테이지 클리어 확인 불리언
    [SerializeField] private int _score = 0;    //점수 변수
    [SerializeField] private int _currentStage = 1;        //스테이지 번호
    [SerializeField] private float _requiredSurvivalTime = 30f;   //버텨야 하는 시간 default
    [SerializeField] private float _increaseDuration = 10f;  //스테이지 거듭할 수록 늘어날 시간 증가값

    [SerializeField] private int _fakeHP = 3; // 디버그용 체력


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
    }

    private void Update()
    {
        //게임오버나 스테이지 클리어가 true면 아무것도 하지 않고 리턴
        if (_isGameOver || _isStageClear)
        {
            return;
        }
        _stageTimer += Time.deltaTime;      //스테이지가 지속될때 deltatime으로 더해줌

        //Debug.Log(_time);

        //스테이지타이머가 버텨야하는 시간보다 높아지면 스테이지 클리어
        if (_stageTimer >= _requiredSurvivalTime)  
        {
            StageClear();
        }

        //아직 미정
        //게임오버 로직
        if (_fakeHP <= 0)
        {
            GameOver();
        }

    }

    //스테이지 시작시 초기화
    public void StageStart()
    {
        _isStageClear = false;
        _isGameOver = false;
        _stageTimer = 0;
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
        _currentStage += 1;
        _requiredSurvivalTime += _increaseDuration;
    }


    //점수 추가 메서드
    public void AddScore(int score)
    {
        _score += score;
    }

    //로비 -> 게임으로 들어갈때 완전 초기화 메서드
    public void InitGame()
    {
        _isStageClear = false;
        _isGameOver = false;
        _stageTimer = 0f;
        _currentStage = 0;
        _requiredSurvivalTime = 0f;
        _score = 0;
    }



    //아직 미정
    //대미지 처리 메서드
    //public void Damage()
    //{
    //    _player.currentHP -= 1;
    //}



    //TODO:게임오버 조건 만들기 , UI만들고 연결하기(UI매니저로 실시)
    //player스크립트 연결 후([serializeField]로 변수를 만든 후 인스펙터에서 직접 연결
   
}
