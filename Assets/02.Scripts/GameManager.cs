using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    // singleton 패턴을 사용하여 인스턴스를 하나만 생성하고 공유하는 방식.
    public static GameManager instance;

    public Transform[] spawnPoints;
    public GameObject[] obstacles;

    public float spawnDelay; // 시간 간격
    private float spawnTimer; // 타이머
    public bool isSpawning; // 스폰을 위한 변수
    private int spawnTracker; // 어떤 장애물을 스폰할지 (0: 선인장1, 1: 선인장2, 2: 선인장3, 3: 선인장4, 4: 새)

    public int mainScore; // 메인 점수
    public TextMeshProUGUI mainScoreText; // 메인 점수 텍스트

    // Awake 메서드는 게임 시작 시 최초 한 번 호출되며, 인스턴스를 초기화하는 데 사용됩니다.
    void Awake() {
        if (instance != null && instance != this) {
            Destroy(this.gameObject);
        } else {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // ScoreUiUpdate 메서드는 점수를 업데이트하는 데 사용됩니다.
    public void ScoreUiUpdate() {
        mainScore++;
        mainScoreText.text = "스코어 : " + mainScore.ToString();
    }

    // Update is called once per frame
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

    void SpawnObstacle() {

        // 첫번째 방법
        spawnTracker = Random.Range(0, obstacles.Length);
        // Instantiate
        // Instantiate(프리팹, 위치, 회전)
        // Quaternion.identity : 회전 없음
        switch (spawnTracker) {
            case 0: // 선인장1
                Instantiate(obstacles[0], spawnPoints[0].position, Quaternion.identity);
                break;
            case 1: // 선인장2
                Instantiate(obstacles[1], spawnPoints[1].position, Quaternion.identity);
                break;
            case 2: // 선인장3
                Instantiate(obstacles[2], spawnPoints[1].position, Quaternion.identity);
                break;
            case 3: // 선인장4
                Instantiate(obstacles[3], spawnPoints[1].position, Quaternion.identity);
                break;
            case 4: // 새
                Instantiate(obstacles[4], spawnPoints[Random.Range(2, 4)].position, Quaternion.identity);
                break;
        }
        spawnTracker++;
        if (spawnTracker >= obstacles.Length) {
            spawnTracker = 0;
        }


        // 두번째 방법
        // switch (spawnTracker) {
        //     case 0: // 선인장1
        //         Instantiate(obstacles[0], spawnPoints[0].position, Quaternion.identity);
        //         break;
        //     case 1: // 선인장2
        //         Instantiate(obstacles[1], spawnPoints[1].position, Quaternion.identity);
        //         break;
        //     case 2: // 선인장3
        //         Instantiate(obstacles[2], spawnPoints[1].position, Quaternion.identity);
        //         break;
        //     case 3: // 선인장4
        //         Instantiate(obstacles[3], spawnPoints[1].position, Quaternion.identity);
        //         break;
        //     case 4: // 새
        //         Instantiate(obstacles[4], spawnPoints[Random.Range(2, 4)].position, Quaternion.identity);
        //         break;
        // }
        // spawnTracker++;
        // if (spawnTracker >= obstacles.Length) {
        //     spawnTracker = 0;
        // }
    }
}
