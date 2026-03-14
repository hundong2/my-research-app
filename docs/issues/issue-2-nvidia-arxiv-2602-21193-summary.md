# Issue #2 - NVIDIA Research Summary

- Source: https://arxiv.org/pdf/2602.21193 (NVIDIA Nemotron-Terminal)
- Paper focus: terminal task generation + filtering + curriculum learning for long-context coding agents

## 1) 한 줄 요약
NVIDIA는 대규모 터미널 작업 데이터(`Terminal-Task-Gen`)를 자동 생성하고 품질 필터링/커리큘럼 학습을 적용해, 긴 컨텍스트 기반 코드 에이전트 성능을 안정적으로 끌어올리는 방법을 제시했다.

## 2) 핵심 아이디어
1. `Seed Expansion`: 기존 벤치마크 문제를 시드로 삼아 다양한 변형 문제를 생성
2. `Skill Taxonomy Expansion`: 명시적 스킬 분류(예: 파일 조작, 디버깅, 시스템 작업)를 기반으로 새로운 작업 생성
3. `Quality Filtering`: 자동 검증 가능한 기준(형식/명령 실행 가능성/정답 검증 등)으로 불량 샘플 제거
4. `Curriculum Learning`: 난이도/길이/멀티스텝 정도를 점진적으로 올리며 학습
5. `Long-context Optimization`: 긴 히스토리와 상태를 다루는 터미널 환경에 맞춘 학습 구성

## 3) 실험에서 확인된 포인트
1. 데이터 양만 늘리는 것보다, 필터링된 고품질 샘플이 성능 향상에 더 직접적임
2. 커리큘럼이 없을 때보다 있을 때 복합 멀티스텝 과제 성공률이 개선됨
3. Seed 기반 + Skill 기반 데이터를 섞는 구성이 단일 생성 방식보다 일반화 성능이 좋음
4. 장문 컨텍스트 의존 작업에서 누적 오류(명령/경로/상태 추적)가 줄어듦

## 4) 실무 적용 관점 정리
1. **데이터 생성 파이프라인을 분리**: 생성(LLM)과 검증(rule/execution)을 분리해야 스케일이 쉬움
2. **검증 가능 과제 우선**: 자동 채점/자동 실행 가능한 작업을 우선 확보해야 반복 개선 가능
3. **커리큘럼은 비용 절감 수단**: 쉬운 과제에서 먼저 안정화하면 고난도 단계 학습 비용을 줄임
4. **도구 사용 로그가 핵심 자산**: 최종 정답보다 중간 행동(명령, 실패, 수정) 로그가 성능에 큰 기여

## 5) 간단한 재현용 예제 코드
아래 예제는 `Nemotron-Research-Reasoning-Qwen-1.5B` 모델을 Hugging Face `transformers`로 호출해, 터미널 작업 생성 프롬프트를 JSON으로 출력하는 최소 샘플이다.

- 코드 파일: `examples/nvidia_nemotron_terminal_taskgen_example.py`
- 실행 전제:
  - Python 3.10+
  - `pip install torch transformers`
  - 필요 시 `huggingface-cli login`

```bash
python examples/nvidia_nemotron_terminal_taskgen_example.py
```

## 6) 참고
- arXiv 원문: https://arxiv.org/pdf/2602.21193
- 모델 카드(예제에서 사용): https://huggingface.co/nvidia/Nemotron-Research-Reasoning-Qwen-1.5B
