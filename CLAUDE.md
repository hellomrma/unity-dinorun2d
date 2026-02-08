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
└── UI Toolkit/      # UI Toolkit (PanelSettings, 테마 등)
```

## 코딩 컨벤션

- **네이밍**: PascalCase (클래스, 메서드, 프로퍼티), camelCase (지역변수, 파라미터), _camelCase (private 필드)
- **MonoBehaviour 메서드 순서**: Awake → OnEnable → Start → Update → FixedUpdate → OnDisable → OnDestroy
- **직렬화**: `[SerializeField] private` 사용 (public 필드 지양)
- **주석**: 한국어 사용 가능
- **네임스페이스**: `DinoRun2D`

## 빌드

- Unity Editor에서 File > Build Settings로 빌드
- 타겟 플랫폼: Standalone (PC/Mac)

## 주의사항

- `.meta` 파일은 Unity가 자동 생성하므로 직접 수정하지 않기
- 씬 파일(`.unity`)은 YAML 형식이므로 머지 충돌에 주의
- `Library/`, `Logs/`, `UserSettings/` 폴더는 gitignore 대상
