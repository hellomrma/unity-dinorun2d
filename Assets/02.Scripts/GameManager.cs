using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

/// <summary>
/// 게임 전반을 관리하는 매니저 클래스.
/// 싱글톤 패턴으로 구현되어 씬 내 어디서든 GameManager.instance로 접근 가능하다.
/// 담당 기능: 장애물 스폰, 점수 관리, 게임 오버 처리
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>씬 전체에서 공유되는 싱글톤 인스턴스</summary>
    public static GameManager instance;

    /// <summary>장애물이 생성될 위치 목록 (Inspector에서 Transform을 연결)</summary>
    public Transform[] spawnPoints;

    /// <summary>스폰 가능한 장애물 프리팹 목록 (선인장4종 + 새)</summary>
    public GameObject[] obstacles;

    /// <summary>장애물 스폰 간격 (초)</summary>
    public float spawnDelay;

    /// <summary>마지막 스폰 이후 경과된 시간을 측정하는 내부 타이머</summary>
    private float spawnTimer;

    /// <summary>true일 때 장애물 스폰을 시작한다</summary>
    public bool isSpawning;

    /// <summary>
    /// 다음에 스폰할 장애물 인덱스 추적용
    /// (0: 선인장1, 1: 선인장2, 2: 선인장3, 3: 선인장4, 4: 새)
    /// </summary>
    private int spawnTracker;

    /// <summary>현재 세션의 누적 점수</summary>
    public int mainScore;

    /// <summary>게임 화면에 실시간으로 점수를 표시하는 TMP 텍스트</summary>
    public TextMeshProUGUI mainScoreText;

    /// <summary>게임 오버 시 표시되는 패널 오브젝트</summary>
    public GameObject gameOverPanel;

    /// <summary>게임 오버 패널에 표시되는 최고 점수 TMP 텍스트</summary>
    public TextMeshProUGUI bestScoreText;

    /// <summary>게임 오버 패널에 표시되는 이번 게임 최종 점수 TMP 텍스트</summary>
    public TextMeshProUGUI endScoreText;

    public AudioClip explosionSound;
    public AudioClip pickupCoinSound;

    public AudioSource SE;

    /// <summary>
    /// [Unity 내장] 씬 로드 시 가장 먼저 호출된다. Start보다 앞서 실행된다.
    /// 싱글톤 패턴: 이미 인스턴스가 존재하면 중복 오브젝트를 파괴하고,
    /// 없으면 자신을 인스턴스로 등록한다.
    /// </summary>
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    /// <summary>
    /// [Unity 내장] 첫 프레임 직전에 한 번 호출된다.
    /// 현재는 초기화 로직 없음 (추후 게임 시작 처리 추가 예정).
    /// </summary>
    void Start()
    {
        SE = GetComponent<AudioSource>();
    }

    /// <summary>
    /// [Unity 내장] 매 프레임 호출된다.
    /// isSpawning이 true일 때 타이머를 증가시키고,
    /// spawnDelay에 도달하면 SpawnObstacle()을 호출해 장애물을 생성한다.
    /// </summary>

    // 과제
    // 장애물 1~3초 사이에서 랜덤하게 나오기
    // PLAY 시, GAMEOVER 시 BGM 나오면 좋겠고
    // Item을 먹으면 장애물에 닿아도 한번 살 수 있음 (쉴드)

    spawnDelay = Random.Range(1f, 3f);
    if (spawnDelay < 1f) {
        spawnDelay = 1f;
    }
    if (spawnDelay > 3f) {
        spawnDelay = 3f;
    }

    void Update()
    {
        if (isSpawning) {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnDelay) {
                spawnTimer = 0;
                SpawnObstacle();
            }
        }
    }

    /// <summary>
    /// 게임 오버 처리를 수행하는 사용자 정의 메서드.
    /// DinoController의 OnTriggerEnter2D에서 장애물 태그 충돌 시 호출된다.
    /// 1) Time.timeScale을 0으로 설정해 게임을 일시 정지한다.
    /// 2) 현재 점수가 최고 점수보다 높으면 PlayerPrefs에 저장한다.
    /// 3) 게임 오버 패널을 활성화하고 점수를 표시한다.
    /// </summary>
    public void GameOver() {
        SE.clip = explosionSound;
        SE.Play();
        // Time.timeScale = 0 → 모든 물리·Update 계산이 멈춘다 (UI는 정상 동작)
        Time.timeScale = 0;

        // PlayerPrefs: Unity 제공 영구 저장소. 앱을 껐다 켜도 유지된다.
        if (mainScore > PlayerPrefs.GetInt("BestScore")) {
            PlayerPrefs.SetInt("BestScore", mainScore);
        }

        bestScoreText.text = "Best Score : " + PlayerPrefs.GetInt("BestScore").ToString();
        endScoreText.text = "Score : " + mainScore.ToString();
        gameOverPanel.SetActive(true);
    }

    public void GameRestart() {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;
    }

    /// <summary>
    /// 점수를 1 증가시키고 UI 텍스트를 갱신하는 사용자 정의 메서드.
    /// DinoController의 OnTriggerEnter2D에서 Point 태그 오브젝트와 충돌 시 호출된다.
    /// </summary>
    public void ScoreUiUpdate() {
        SE.clip = pickupCoinSound;
        SE.Play();
        mainScore++;
        mainScoreText.text = "스코어 : " + mainScore.ToString("D5");
    }

    /// <summary>
    /// 랜덤으로 장애물 하나를 선택해 지정된 spawnPoint에 생성하는 사용자 정의 메서드.
    /// Update에서 spawnDelay 간격마다 호출된다.
    /// Instantiate(프리팹, 위치, 회전): 프리팹의 복사본을 씬에 생성한다.
    /// Quaternion.identity: 회전 없이 기본 방향으로 생성한다.
    /// </summary>
    void SpawnObstacle() {
        // Random.Range(min, max): min 이상 max 미만의 정수를 반환한다
        spawnTracker = Random.Range(0, obstacles.Length);

        switch (spawnTracker) {
            case 0: // 선인장1 → 지상 스폰 포인트[0]
                Instantiate(obstacles[0], spawnPoints[0].position, Quaternion.identity);
                break;
            case 1: // 선인장2 → 지상 스폰 포인트[1]
                Instantiate(obstacles[1], spawnPoints[1].position, Quaternion.identity);
                break;
            case 2: // 선인장3 → 지상 스폰 포인트[1]
                Instantiate(obstacles[2], spawnPoints[1].position, Quaternion.identity);
                break;
            case 3: // 선인장4 → 지상 스폰 포인트[1]
                Instantiate(obstacles[3], spawnPoints[1].position, Quaternion.identity);
                break;
            case 4: // 새 → 공중 스폰 포인트[2~3] 중 랜덤 높이
                Instantiate(obstacles[4], spawnPoints[Random.Range(2, 4)].position, Quaternion.identity);
                break;
        }

        // 다음 호출을 위해 인덱스를 순환시킨다 (현재는 첫 줄에서 Random.Range로 덮어쓰므로 영향 없음)
        spawnTracker++;
        if (spawnTracker >= obstacles.Length) {
            spawnTracker = 0;
        }
    }
}
