# DinoRun2D

Chrome 공룡 달리기 스타일의 2D 러닝 게임 (Unity 프로젝트)

## 프로젝트 정보

- **엔진**: Unity 2022.3.62f3 (LTS)
- **템플릿**: Unity 2D Template
- **언어**: C# (.NET Standard 2.1)
- **해상도**: 1920x1080

## 폴더 구조

```
Assets/
├── 01.Scenes/       # 씬 파일 (GameScene 등)
├── 02.Scripts/      # C# 스크립트
├── 03.Images/       # 2D 스프라이트/이미지 (공룡, 선인장, 땅, 구름 등)
├── 04.Sounds/       # 사운드, BGM
├── 05.Animations/   # 애니메이션 클립 & 컨트롤러
├── 06.Prefabs/      # 프리팹 (장애물 등)
└── UI Toolkit/      # UI Toolkit (PanelSettings, 테마 등)
```

## 스크립트 목록 (`Assets/02.Scripts/`)

| 파일 | 네임스페이스 | 역할 |
|------|------------|------|
| `DinoController.cs` | `DinoRun2D` | 공룡 점프·숙이기 조작, 바닥 감지, 충돌 처리 |
| `GameManager.cs` | (없음) | 장애물 스폰 관리 (타이머, 랜덤 선택) |
| `ObstacleController.cs` | (없음) | 장애물 이동 및 화면 밖 자동 삭제 |
| `Scroll.cs` | `DinoRun2D` | 배경 스크롤 (땅: 텍스처 오프셋, 구름: 오브젝트 이동) |

## 구현된 기능

- 공룡 점프 (스페이스바, Rigidbody2D velocity 방식)
- 공룡 숙이기 (아래 방향키, BoxCollider2D 크기 변경)
- 바닥 감지 (OverlapCircle)
- 달리기/점프/숙이기 애니메이션 전환 (Animator bool 파라미터)
- 배경 스크롤 (땅 텍스처 오프셋, 구름 오브젝트 루프)
- 장애물 랜덤 스폰 (선인장 4종 + 새, spawnPoints 기반)
- 장애물 자동 이동 및 화면 밖 삭제 (OnBecameInvisible)
- 충돌 감지 기반 게임 오버 / 점수 획득 트리거 (미완성)

## 코딩 컨벤션

- **네이밍**: PascalCase (클래스, 메서드, 프로퍼티), camelCase (지역변수, 파라미터), `_camelCase` (private 필드)
- **MonoBehaviour 메서드 순서**: Awake → OnEnable → Start → Update → FixedUpdate → OnDisable → OnDestroy
- **직렬화**: `[SerializeField] private` 사용 (public 필드 지양)
- **주석**: 한국어 사용 가능
- **네임스페이스**: `DinoRun2D`

> ⚠️ `GameManager.cs`와 `ObstacleController.cs`는 현재 네임스페이스 및 `[SerializeField] private` 컨벤션 미적용 상태

## 빌드

- Unity Editor에서 File > Build Settings로 빌드
- 타겟 플랫폼: Standalone (PC/Mac)

## 주의사항

- `.meta` 파일은 Unity가 자동 생성하므로 직접 수정하지 않기
- 씬 파일(`.unity`)은 YAML 형식이므로 머지 충돌에 주의
- `Library/`, `Logs/`, `UserSettings/` 폴더는 gitignore 대상
