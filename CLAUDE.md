# Archer Rush — 프로젝트 컨텍스트

## 한 줄 요약
광고에 나오는 미니게임을 실제로 만드는 프로젝트. 궁수 기반 2.5D 레인 슈터 + 게이트 러너 하이브리드. MVP 1차 완성 상태.

## 스택
- Unity 6 LTS (6000.4.2f1) + URP 3D
- C# MonoBehaviour, Input System (new), UGUI (Legacy Text)
- Claude Code × unity-mcp (Coplay, HTTP transport, localhost:8080)

## 프로젝트 구조
```
minigame-proto/
├── unity/archer-rush/          # Unity 프로젝트 (본 개발)
│   ├── Assets/Scripts/         # 9개 C# 파일
│   ├── Assets/Prefabs/         # Arrow, Enemy, Gate
│   ├── Assets/Materials/       # URP 머티리얼 5종
│   ├── Assets/Scenes/Game.unity # 메인 씬 (Build Settings 0번)
│   └── README.md               # 실행 가이드 + 스크립트 설명
├── poc-web/                    # 웹 Canvas POC (아카이브, 터치하지 말 것)
├── docs/                       # 기획 문서 (project-overview, theme-direction)
├── .mcp.json                   # unity-mcp 서버 등록
└── CLAUDE.md                   # 이 파일
```

## unity-mcp 세팅 (새 머신에서)
1. Unity Hub에서 `unity/archer-rush/` 프로젝트 열기
2. `Window > Package Manager > + > Add package from git URL`:
   ```
   https://github.com/CoplayDev/unity-mcp.git?path=/MCPForUnity#main
   ```
3. `Window > MCP for Unity` 열고 Start Server 클릭 → localhost:8080 서버 가동
4. Claude Code 세션 시작 → `.mcp.json` 승인 프롬프트 → 승인
5. `mcp__unityMCP__manage_scene` 등 툴이 뜨면 연결 성공

## 개발 패턴 / 주의사항 (실전에서 배운 것)
- **Kinematic Rigidbody + transform.position**: OnTriggerEnter 안 터짐. `Rigidbody.linearVelocity` 설정해서 물리 엔진이 이동하게 해야 함.
- **GameManager singleton**: static은 Unity Editor의 "No Domain Reload" Play Mode에서 stale 걸림. `FindAnyObjectByType` lazy getter 패턴 사용 중.
- **save_as_prefab**: 씬에 원본 인스턴스도 남김 — 반드시 수동 삭제. 안 하면 y=-100 좌표의 유령 오브젝트가 proximity 체크에 걸림.
- **create_script validator**: private 메서드 정의+호출 패턴에서 "duplicate method" false positive 발생. Update() 안에 인라인으로 작성해서 우회.
- **component_properties**: `components_to_add`에 명시한 컴포넌트에만 적용. Cube 기본 BoxCollider.isTrigger 같은 건 `manage_prefabs modify_contents`로 별도 수정.
- **execute_code**: codedom 기본(C# 6). `?.` 연산자, local function 불가. `System.Func<>` 람다 사용.
- **manage_components add**: 타입 lookup 캐시 이슈로 간헐적 실패 → `execute_code`로 `AddComponent` 직접 호출이 더 안정적.
- **MCP transport**: stdio 모드는 Unity 브릿지 discovery 실패 — HTTP 모드(`localhost:8080/mcp`) 사용.
- **Gate 충돌 감지**: Collider 기반 OnTriggerEnter 미사용. `Update()` 내 수동 AABB 체크(`triggerXHalf`, `triggerZHalf`)로 구현 — Gate에 Rigidbody 없으므로 의도적 선택.
- **Enemy.archerTransform**: `GameObject.Find("Archer")` 이름 하드코딩. 플레이어 오브젝트 이름 변경 시 접촉 데미지 미발동. 리팩터 대상.
- **Archer 타겟팅**: `FindObjectsByType<Enemy>()` 매 프레임 호출 (캐싱 없음). 적이 수십 마리 이상이면 성능 이슈 예상 — 최적화 전에 프로파일러 확인.

## 현재 상태
MVP 루프 완성: 드래그 이동 → 자동 사격 → 적 웨이브 → 게이트 2택 버프 → HP → 게임 오버 → 재시작.
프리미티브 비주얼(큐브/캡슐)만 사용. 아트/사운드/애드몹/세이브 없음.

## 다음 할 일 (우선순위 순)
1. 플레이 테스트 → 감도/밸런스 수치 튜닝
2. 카메라 추적 여부 결정
3. 적 다양성 (빠른 적, 탱커)
4. 게이트 버프 확장 (멀티샷, 관통)
5. 테마 + 아트 방향 확정 → 저폴리 모델 교체
6. 사운드 기본
7. AdMob 통합 + 모바일 빌드
