"""Minimal task-generation example with NVIDIA Nemotron research model.

This script asks the model to generate terminal tasks in a structured JSON format.
It is intentionally small so you can adapt it into a larger data-generation pipeline.
"""

from __future__ import annotations

import json
from typing import Any

import torch
from transformers import AutoModelForCausalLM, AutoTokenizer

MODEL_ID = "nvidia/Nemotron-Research-Reasoning-Qwen-1.5B"


def build_prompt() -> str:
    return (
        "You are a dataset generator for terminal-based coding agents.\\n"
        "Generate exactly 3 tasks as valid JSON (no markdown).\\n"
        "Schema: {\\\"tasks\\\": [{\\\"title\\\": str, \\\"goal\\\": str, "
        "\\\"difficulty\\\": \\\"easy|medium|hard\\\", \\\"checks\\\": [str]}]}\\n"
        "Requirements:\\n"
        "- Tasks must be Linux terminal executable.\\n"
        "- Include objective checks that can be automatically verified.\\n"
        "- At least one task must involve debugging a failed command.\\n"
    )


def load_model() -> tuple[Any, Any]:
    tokenizer = AutoTokenizer.from_pretrained(MODEL_ID)
    model = AutoModelForCausalLM.from_pretrained(
        MODEL_ID,
        torch_dtype=torch.float16 if torch.cuda.is_available() else torch.float32,
        device_map="auto",
    )
    return tokenizer, model


def generate_tasks(tokenizer: Any, model: Any) -> dict[str, Any]:
    prompt = build_prompt()
    inputs = tokenizer(prompt, return_tensors="pt").to(model.device)

    with torch.no_grad():
        output_ids = model.generate(
            **inputs,
            max_new_tokens=500,
            do_sample=True,
            temperature=0.7,
            top_p=0.9,
            eos_token_id=tokenizer.eos_token_id,
        )

    generated = tokenizer.decode(output_ids[0], skip_special_tokens=True)
    raw_json = generated[len(prompt) :].strip()

    return json.loads(raw_json)


def main() -> None:
    tokenizer, model = load_model()
    tasks = generate_tasks(tokenizer, model)
    print(json.dumps(tasks, indent=2, ensure_ascii=False))


if __name__ == "__main__":
    main()
