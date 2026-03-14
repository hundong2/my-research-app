#!/bin/bash

# Conceptual gstack workflow demonstration script
# This script illustrates how gstack skills are invoked within a Claude Code session

# 1. SETUP: Install gstack to the Claude skills directory
# git clone https://github.com/garrytan/gstack.git ~/.claude/skills/gstack
# cd ~/.claude/skills/gstack && ./setup

echo "--- STARTING GSTACK WORKFLOW DEMO ---"

# Step 1: Founder Vision Planning
# User: "I want to add a real-time notification system."
# Claude (via /plan-ceo-review): 
# "Let's think about the 10-star version. Instead of just popups, 
# how about a subtle ambient pulse on the edge of the screen 
# that shows user intent before the action is completed?"

echo "[CEO Mode]: Refining product vision for the '10-star version'..."

# Step 2: Architecture Design
# User: "/plan-eng-review"
# Claude: "Analyzing system architecture. We need a WebSocket layer 
# and a Redis backend for state. Here is the Mermaid diagram..."

echo "[Eng Lead Mode]: Generating technical architecture and data flow diagrams..."

# Step 3: Implementation
# (Claude implements the code using standard tool calls)

echo "[Implementing]: Standard code generation phase..."

# Step 4: Rigorous Audit
# User: "/review"
# Claude: "Found a potential race condition in the WebSocket handshake 
# and an N+1 query when fetching initial notification state. Fixing..."

echo "[Staff Eng Mode]: Performing structural audit and security check..."

# Step 5: Functional Verification
# User: "/browse"
# Claude: "Navigating to /dashboard. Taking screenshot. 
# Confirmed: Notification pulse is visible and correctly positioned."

echo "[QA Mode]: Verifying UI with headless browser automation..."

# Step 6: Automated Shipping
# User: "/ship"
# Claude: "Running tests... All passed. Syncing main. 
# Updating changelog. Pushing branch and opening PR #42."

echo "[Release Mode]: Pushing changes and opening Pull Request..."

echo "--- GSTACK WORKFLOW COMPLETE ---"
