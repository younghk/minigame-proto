# Game Sideproject

영원의 모바일/웹 게임 사이드프로젝트 작업 디렉토리.

## Current focus
- 광고에서 보이는 미니게임의 실제 버전 만들기
- 첫 게임: 궁수 기반 2.5D 레인 슈터 (게이트 러너 메커닉 유지) — MVP 1차 완성
- 스택: Unity 6 LTS + URP 3D, Claude Code × unity-mcp 워크플로우
- 다음 단계: 리뷰 → 튜닝 → 아트/사운드 → AdMob 붙이기

## MVP 상태 (2026-04-16 자동 진행)
- `unity/archer-rush/` — 플레이 가능한 수직슬라이스
- Scene: `Assets/Scenes/Game.unity`
- 루프: 드래그 이동 · 자동 사격 · 적 웨이브 · 게이트 2택 버프 · HP · 게임 오버 · 재시작
- 실행: Unity Hub → archer-rush 프로젝트 → Game 씬 열고 Play → 마우스 드래그로 좌우 이동
- 자세한 설명: `unity/archer-rush/README.md`

## Structure
- `unity/archer-rush/`: Unity 프로젝트 (본 개발)
- `poc-web/`: 초기 웹 Canvas POC (reference, 아카이브)
- `docs/project-overview.md`: 프로젝트 전체 개요
- `docs/theme-direction.md`: 게임 테마 방향 정리
- `.mcp.json`: Claude Code ↔ Unity Editor 연결 설정 (HTTP `localhost:8080`)

## Note
장기 메모와 회고는 Obsidian/memory 쪽에 남고, 실제 프로젝트 산출물은 여기서 관리한다.
