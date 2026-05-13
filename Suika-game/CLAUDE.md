# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

**수박 게임(Suika Game) 클론** — 과일을 떨어뜨려 합치는 2D 물리 퍼즐 게임.
- Unity 버전: **6000.3.15f1** (Unity 6.3)
- 템플릿: Universal 2D (URP)
- 렌더 파이프라인: Universal Render Pipeline (URP) 17.3.0
- 메인 씬: `Assets/Scenes/SampleScene.unity`

## Code Formatting

C# 파일을 저장하면 **CSharpier**가 자동으로 포맷팅됩니다 (PostToolUse 훅).

수동으로 포맷팅하려면:
```powershell
dotnet csharpier format <파일경로>
dotnet csharpier format Assets/Scripts/  # 폴더 전체
```

CSharpier 버전 관리는 `dotnet-tools.json`에 있으며, 미설치 시:
```powershell
dotnet tool restore
```

## Scene Editing

**`.unity` 씬 파일을 직접 편집하지 말 것.** 씬 오브젝트 추가/수정/삭제는 반드시 **Unity MCP**를 통해서만 수행한다.

## Build & Verification

Unity 에디터에서 검증:
- **플레이 테스트**: 에디터 Play 버튼
- **빌드**: `File > Build Profiles` → WebGL 또는 대상 플랫폼 선택 후 빌드

## File Permissions

다음 경로는 읽기/쓰기가 제한됩니다:
- `Library/**` — Unity 캐시 (읽기 불가)
- `Temp/**`, `Logs/**`, `obj/**` — 빌드 산출물 (읽기 불가)
- `**/*.meta` — Unity 메타 파일 (쓰기 불가)
- `**/*.unity` — 씬 파일 (쓰기/편집 불가, Unity MCP 사용)
- `**/packages-lock.json` — 편집 불가

## Project Structure

```
Assets/
  Scripts/       # C# 게임 스크립트
  Scenes/        # Unity 씬 파일
  Settings/      # URP 렌더러 설정
Packages/
  manifest.json  # 패키지 의존성
ProjectSettings/ # Unity 프로젝트 설정
.claude/
  settings.json  # Claude Code 훅 및 권한 설정
```

## Key Packages

- `com.unity.2d.*` — 2D 스프라이트, 애니메이션, 타일맵 도구
- `com.unity.inputsystem` 1.19.0 — 입력 처리
- `com.unity.ai.assistant` 2.7.0 — Unity AI 어시스턴트 (에디터 내 AI 기능)
- `com.unity.render-pipelines.universal` 17.3.0 — URP 렌더링
