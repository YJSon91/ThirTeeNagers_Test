using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }        //싱글톤 인스턴스 변수


    //인스펙터에서 가져올 수 있도록 시리얼라이즈필드 설정
    [SerializeField] private PlayerState _player;       //게임오버를 위한 player 가져옴
    [SerializeField] private Slider survivalTimeSlider;          //살아남는 시간 표현 슬라이더
    [SerializeField] private TextMeshProUGUI scoreTxt;     //점수 텍스트
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private TextMeshProUGUI bestScoreTxt;
    [SerializeField] private TextMeshProUGUI clearBestScoreTxt;
    [SerializeField] private TextMeshProUGUI clearScoreTxt;
    [SerializeField] private GameObject stageClearPanel;



    [SerializeField] private bool _isGameOver = false;     //게임오버 확인 불리언
    [SerializeField] private bool _isStageClear = false;   //스테이지 클리어 확인 불리언
    [SerializeField] private bool _isPause = false;         //일시정지 확인 불리언
    [SerializeField] private int _score = 0;    //점수 변수
    private int bestScore;

    //점수 프로퍼티
    public int score
    {
        get { return _score; }
        set { _score = value; }
    }

    [SerializeField] private int _currentStage = 1;        //스테이지 번호
    //스테이지 번호 프로퍼티
    public int currentStage { get { return _currentStage; } }

    [SerializeField] private float _requiredSurvivalTime = 0f;  //실제 버텨야하는 시간


    private const string BestScoreKey = "BestScore";


    //게임 매니저 싱글톤 패턴
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //
        bestScore = PlayerPrefs.GetInt(BestScoreKey,0);
        bestScoreTxt.text = bestScore.ToString();


        //시작할때 슬라이더 밸류값 설정
        survivalTimeSlider.minValue = 0;
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;

    }
    private void Start()
    {   //스테이지 선택 메뉴에서 할당받았는지 여부 확인
        if(StageDataHolder.Instance.selectedStage != null)
        {
            StageManager.instance.LoadStage(StageDataHolder.Instance.selectedStage);            //선택된 스테이지의 값을 로드함
        }   
        else
        {
            Debug.LogWarning("[GameManager] 선택된 스테이지가 없습니다.");
        }
    }

    private void Update()
    {
        //PlayerSpeed = _player.PlayerSpeed;
        UpdateHighScore(score);

        //디버그용 스테이지 스타트 
        if (_isStageClear && Input.GetKeyDown(KeyCode.A))
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
            UpdateHighScore(score);
            StageClear();
            return;
        }

        //게임오버 로직
        if (_player.CurrentHealth <= 0)
        {
            UpdateHighScore(score);
            GameOver();
            return;
        }
    }

    //스테이지 시작시 초기화
    public void StageStart()
    {

        // 시간이 0이면 잘못된 초기화로 보고, 최소 생존 시간을 지정
        if (_requiredSurvivalTime <= 0f)
        {
            Debug.LogWarning("StageStart called with _requiredSurvivalTime <= 0. Forcing default value.");
            _requiredSurvivalTime = 10f; // 또는 _baseSurvivalTime 등 적절한 기본값
        }
        //_requiredSurvivalTime = _baseSurvivalTime + (_increaseDuration * (_currentStage - 1));      //스테이지마다 버텨야하는 시간값을 갱신

        //시작할때 슬라이더 밸류값 설정
        survivalTimeSlider.minValue = 0;
        survivalTimeSlider.maxValue = _requiredSurvivalTime;
        survivalTimeSlider.value = _requiredSurvivalTime;

        //불값 초기화
        _isStageClear = false;
        _isGameOver = false;
    }

    //스테이지 데이터 값을 받아오기 위한 메서드
    public void SetSurvivalTime(float survivalTime)
    {
        _requiredSurvivalTime = survivalTime;
    }


    //게임오버 메드
    //TODO:게임오버 씬 혹은 UI를 만들고 켜주기
    public void GameOver()
    {
        _isGameOver = true;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        SoundManager.instance.PlayDeath();
        
    }


    //스테이지 클리어 메서드
    //TODO:스테이지 클리어 UI를 만들고(다음 스테이지로 갈지 스타트씬?으로 갈지 결정) 켜주기
    public void StageClear()
    {
        stageClearPanel.SetActive(true);
        UpdateHighScore(score);
        clearBestScoreTxt.text = bestScore.ToString();
        clearScoreTxt.text = score.ToString();
        _isStageClear = true;
        Debug.Log("StageClear");
        _currentStage += 1;             //스테이지 ++

        //_player.PlayerSpeed += _increaseSpeed * _currentStage;              //플레이어 속도는 증가값 * 스테이지(추후에 변경해야 될 사항)

        StageUnlockManager.UnlockNextStage(_currentStage - 1);

        Time.timeScale = 0f;


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
        return _player.PlayerSpeed; // 현재는 임시값, 추후 게임 진행에 따라 증가하도록 변경 가능
    }

    //일시정지 메서드
    public void Pause()
    {
        _isPause = true;
        Time.timeScale = 0f;
    }

    //재개 메서드
    public void Resume()
    {
        _isPause = false;
        Time.timeScale = 1f;

    }

    //최고점수와 현재 점수를 비교하고 최고점수를 갱신함
    public void UpdateHighScore(int score)
    {
        if(bestScore < score)
        {
            bestScore = score;
            PlayerPrefs.SetInt(BestScoreKey, bestScore);
        }
        bestScoreTxt.text = bestScore.ToString();
    }

    //스테이지 클리어 패널을 닫아주는 함수
    public void CloseStageClearPanel()
    {
        stageClearPanel.SetActive(false);
        _isStageClear = false;
    }
}
