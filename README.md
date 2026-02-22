# DinoRun2D

Chrome 공룡 달리기 스타일의 2D 러닝 게임입니다. Unity 2D로 제작됩니다.

## 요구 사항

- **Unity**: 2022.3.62f3 (LTS) 이상
- **템플릿**: Unity 2D Template 기준

## 프로젝트 구조

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

## 시작하기

1. Unity Hub에서 **Unity 2022.3.62f3 (LTS)** 설치
2. 이 저장소 클론 후 Unity Hub로 **프로젝트 열기**
3. `Assets/01.Scenes/GameScene.unity` 씬 열기
4. Play 버튼으로 실행

## 빌드

- 메뉴: **File → Build Settings**
- 타겟 플랫폼: **Standalone (PC/Mac)**
- 해상도: 1920x1080 권장

## 구현된 기능

- 공룡 점프 (스페이스바)
- 공룡 숙이기 (아래 방향키)
- 달리기 / 점프 / 숙이기 애니메이션 전환
- 배경 스크롤 (땅 텍스처 오프셋, 구름 오브젝트 루프)
- 장애물 랜덤 스폰 (선인장 4종 + 새)
- 장애물 자동 이동 및 화면 밖 삭제

## 기술 스택

| 항목     | 내용                |
|----------|---------------------|
| 엔진     | Unity 2022.3 LTS     |
| 언어     | C# (.NET Standard 2.1) |
| 해상도   | 1920x1080           |

## 라이선스

저장소 설정에 따릅니다.
