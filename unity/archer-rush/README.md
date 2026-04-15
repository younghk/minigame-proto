# Archer Rush — MVP

Unity 6 LTS + URP 3D. Claude Code × unity-mcp로 개발.

## 아침에 돌아온 사용자 용 가이드

### 실행 방법
1. Unity Hub 열기 → Projects → `archer-rush` 클릭 (이미 열려있으면 생략)
2. Project 창에서 `Assets/Scenes/Game.unity` 더블클릭 (이미 열려있을 수 있음)
3. Play 버튼 (▶) 클릭
4. Game 창 클릭해서 포커스 → **마우스 좌우 드래그**로 궁수 이동

### 조작
- **좌우 드래그** (마우스/터치): 궁수 x축 이동
- 궁수는 자동으로 가장 가까운 적을 타겟팅해서 화살을 발사
- 빨간 적을 피하거나 먼저 죽여야 함 — 접촉하면 HP -1
- 초록 게이트(FireRate) / 주황 게이트(Damage) 중 하나를 통과해서 버프 획득
- HP 0 → GAME OVER → 화면 중앙 RESTART 버튼

### 구성
- **Scripts** (`Assets/Scripts/`)
  - `Archer.cs` — 자동 타겟팅 + 화살 인스턴스
  - `PlayerController.cs` — 드래그 입력 → X축 이동 (Input System Pointer)
  - `Arrow.cs` — 화살 이동(dynamic Rigidbody.linearVelocity) + 적 트리거
  - `Enemy.cs` — 전진 이동 + 궁수 proximity 체크 (HP 3)
  - `EnemySpawner.cs` — 1.4s마다 z=25에서 스폰
  - `Gate.cs` — 게이트 이동 + XZ proximity 체크 + 버프 적용 + 색상
  - `GateSpawner.cs` — 8s마다 좌우 쌍 게이트 스폰 (랜덤 버프 배치)
  - `GameManager.cs` — HP/Score/GameOver 상태 (lazy singleton)
  - `HudUI.cs` — 런타임 Canvas/Text/Button 구축, GameManager 이벤트 구독

- **Prefabs** (`Assets/Prefabs/`)
  - `Arrow.prefab` — 노란 cube + trigger collider + Rigidbody + Arrow script
  - `Enemy.prefab` — 빨간 cube + BoxCollider + Enemy script
  - `Gate.prefab` — 흰 cube (runtime에서 초록/주황) + BoxCollider + Gate script

- **Materials** (`Assets/Materials/`) — URP/Lit 기반 5종

### 알려진 제약 / 다음 할 일
- **시각**: 프리미티브 큐브/캡슐만 사용. 실제 궁수/몬스터 모델 필요
- **밸런스**: 스폰 간격·HP·데미지 수치 튜닝 안 됨
- **카메라**: 고정 (0, 14, -10) 55° 틸트. 플레이어 추적 안 함
- **사운드**: 없음
- **애니메이션**: 없음 (화살 회전도 없음)
- **적 다양성**: 1종만
- **보스**: 없음
- **세이브**: 없음
- **AdMob/IAP**: 미연결
- **드래그 감도**: sensitivity=0.04 (튜닝 가능). 모바일 터치에서 너무 민감/둔감할 수 있음

### MCP 워크플로우 메모
- `.mcp.json`은 프로젝트 루트에서 `http://127.0.0.1:8080/mcp`에 접속
- Claude Code 세션에서 `mcp__unityMCP__*` 도구 사용 가능
- Unity Editor에서 `Window > MCP for Unity` 창이 열려있어야 함 (서버 자동 시작됨)
- 새 Claude Code 세션 시작 시 `.mcp.json` 승인 프롬프트 → 승인
