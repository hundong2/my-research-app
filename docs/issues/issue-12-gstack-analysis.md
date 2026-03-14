# Issue #12: gstack - Opinionated Workflow Skills for Claude Code

## Overview
**gstack** is an open-source suite of "opinionated workflow skills" designed to transform **Claude Code** from a general-purpose assistant into a specialized team of virtual experts (CEO, Engineering Manager, Staff Engineer, etc.). It was created by Garry Tan (CEO of Y Combinator) and serves as an "operating system for shipping" software with high rigor and speed.

- **Repository:** [https://github.com/garrytan/gstack.git](https://github.com/garrytan/gstack.git)
- **Primary Goal:** To enable developers to switch "cognitive gears" using specific personas throughout the development lifecycle.

---

## The 8 Specialized Skills (Slash Commands)

### 1. `/plan-ceo-review` (Founder Mode)
- **Role:** Product Visionary / Founder.
- **Focus:** Product "taste," ambition, and user value.
- **Key Action:** Challenges the user to find the "10-star version" of a feature rather than just implementing a literal request.

### 2. `/plan-eng-review` (Tech Lead Mode)
- **Role:** Technical Architect.
- **Focus:** System architecture, data flow, state machines, and edge cases.
- **Key Action:** Forces Claude to generate diagrams and surface hidden technical assumptions before code is written.

### 3. `/review` (Paranoid Staff Engineer)
- **Role:** Security/Performance Auditor.
- **Focus:** Deep structural audit (N+1 queries, race conditions, security vulnerabilities, invariant violations).
- **Key Action:** Performs a more rigorous check than standard CI tools to ensure production readiness.

### 4. `/ship` (Release Engineer)
- **Role:** DevOps / Release Engineer.
- **Focus:** Automation of the "final mile."
- **Key Action:** Syncs with main, runs tests, updates changelogs, and opens/updates Pull Requests.

### 5. `/browse` (QA Engineer)
- **Role:** Visual QA / Browser Automation.
- **Focus:** End-to-end verification.
- **Key Action:** Uses a headless browser (Playwright) to navigate the app, take screenshots, and verify UI state.

### 6. `/qa` (QA Lead)
- **Role:** Systematic Tester.
- **Focus:** Regression and impact analysis.
- **Key Action:** Analyzes git diffs to identify affected routes and automatically performs targeted testing.

### 7. `/setup-browser-cookies` (Session Manager)
- **Role:** Authentication Helper.
- **Focus:** Testing authenticated sessions.
- **Key Action:** Imports cookies from local browsers (Chrome, Arc, Brave) into Claude’s environment for testing behind logins.

### 8. `/retro` (Engineering Manager)
- **Role:** Team Lead.
- **Focus:** Performance analysis and improvement.
- **Key Action:** Generates data-driven retrospectives from commit history, offering praise and growth feedback.

---

## Technical Architecture & Details

### Tech Stack
- **Language:** TypeScript (~90%+).
- **Runtime:** Bun v1.0+.
- **Browser Automation:** Playwright (Microsoft).
- **Integration:** Native support for **Greptile** (AI PR reviews).

### Key Technical Features
- **High-Performance Browser:** Uses a compiled native binary for the `/browse` skill (latencies of 100-200ms per call).
- **Isolation:** Each workspace gets a dedicated Chromium process and isolated state (stored in `.gstack/`).
- **Security:** Decrypts browser cookies via macOS Keychain (requires user permission) without exposing values in the UI.
- **Conductor Mode:** Supports up to 10 parallel Claude Code sessions on different branches.

---

## Installation and Usage

### 1. Installation
Clone the repository into the Claude skills directory and run the setup script:
```bash
git clone https://github.com/garrytan/gstack.git ~/.claude/skills/gstack
cd ~/.claude/skills/gstack && ./setup
```

### 2. Standard Workflow
1.  **Plan:** `/plan-ceo-review` (Vision) -> `/plan-eng-review` (Architecture).
2.  **Build:** Implement code using standard Claude Code commands.
3.  **Verify:** `/review` (Code Audit) -> `/qa` or `/browse` (UI/Functional Verification).
4.  **Ship:** `/ship` (Release and PR).

---

## Summary
`gstack` represents a shift from "AI as a chat tool" to "AI as a coordinated team." By providing rigid, persona-based workflows, it helps maintain high engineering standards while leveraging the speed of LLMs. It is particularly powerful for solo founders or small teams looking to maintain "Staff Engineer" level rigor across their entire codebase.
