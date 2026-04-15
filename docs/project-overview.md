# Project Overview

## Vision
광고 속에서 자주 보이는 미니게임을 실제 플레이 가능한 게임으로 구현해서 배포한다.

## Product thesis
- 짧은 세션에서 바로 이해되는 캐주얼 게임이 유리하다.
- 빠르게 만들고, 지표가 안 나오면 빨리 접는다.
- 수익화는 광고 중심, IAP는 광고 제거 정도로 단순화한다.

## Platform
- 모바일 앱 우선
- **Unity 6 LTS + URP 3D** (Claude Code × unity-mcp 워크플로우)
- iOS / Android 동시 전개
- 웹은 필요 시 프로토타입 또는 랜딩 용도로만 검토 (기존 HTML5 POC는 `poc-web/`)

## Monetization
- 주 수익: AdMob 보상형 비디오
- 보조 수익: 광고 제거 IAP
- 이후 필요하면 mediation 확장 검토

## Current Game 1
레인 슈터 + 게이트 러너 하이브리드

### Core loop
- 좌우 이동
- 자동 직선 사격
- 적 웨이브 처치
- 게이트 선택으로 화력 또는 병력 변화
- 보스 클리어 후 보상

## Tech stack
- Unity 6 LTS (6000.4.2f1)
- URP (Universal Render Pipeline) — 모바일 2.5D
- C# + MonoBehaviour
- Input System (new, Pointer 기반)
- UGUI (Legacy UI.Text + Canvas) — 런타임 빌드
- Google Mobile Ads Unity plugin (AdMob) — Phase 이후
- Unity IAP — Phase 이후
- Unity Analytics / Firebase Analytics — Phase 이후

**개발 도구**
- Claude Code × unity-mcp (Coplay, HTTP transport, localhost:8080)
- MCP 툴로 스크립트 생성·컴파일 체크·씬/프리팹 조작·Play 모드 검증 자동화

## Immediate next topics
1. 테마 확정
2. 블루프린트 작성
3. 프로젝트 초기 세팅
