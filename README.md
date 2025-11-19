# GitHub Push & Pull Helper 🚀

This README contains a single **bash script** with all the steps and notes you need to **clone, pull, commit, and push** code to GitHub.  
You can copy the script into a file (e.g., `git_helper.sh`), make it executable, and run it.

---

## Usage

```bash
# Save this file as git_helper.sh
chmod +x git_helper.sh
./git_helper.sh
```

#!/usr/bin/env bash
# ============================================
# GitHub Push & Pull Helper Script
# ============================================
# Notes:
# - ALWAYS pull before starting work
# - NEVER commit directly to main (use branches)
# - Workflow:
#   1. git checkout main && git pull origin main
#   2. git checkout -b yourname/feature-xyz
#   3. Make changes
#   4. git add . && git commit -m "feat: message"
#   5. git push origin yourname/feature-xyz
#   6. Open a Pull Request to merge back into main
# ============================================

set -euo pipefail

REPO_URL="https://github.com/Rejshen/IAB251-AT2.git"
DEFAULT_BRANCH="main"

echo "➡️ Repo: $REPO_URL"
echo "➡️ Default branch: $DEFAULT_BRANCH"

# Clone if not exists
if [ ! -d "IAB251-AT2/.git" ]; then
  echo "📥 Cloning repo..."
  git clone "$REPO_URL"
fi

cd IAB251-AT2

# Pull latest main
echo "🔄 Pulling latest changes..."
git checkout "$DEFAULT_BRANCH"
git pull origin "$DEFAULT_BRANCH"

# Ask for branch name
read -rp "Enter your feature branch name (e.g., ray/feat-xyz): " FEATURE_BRANCH

# Create or switch to branch
if git show-ref --verify --quiet "refs/heads/$FEATURE_BRANCH"; then
  echo "🌿 Switching to existing branch: $FEATURE_BRANCH"
  git checkout "$FEATURE_BRANCH"
else
  echo "🌿 Creating new branch: $FEATURE_BRANCH"
  git checkout -b "$FEATURE_BRANCH"
fi

# Merge latest main into branch
git merge "$DEFAULT_BRANCH" || true

# Ask if ready to commit
read -rp "Stage, commit, and push changes now? (y/N): " CONFIRM
if [[ "${CONFIRM,,}" == "y" ]]; then
  git add .
  read -rp "Enter commit message: " COMMIT_MSG
  git commit -m "${COMMIT_MSG:-update}"
  git push -u origin "$FEATURE_BRANCH"
  echo "✅ Changes pushed to branch: $FEATURE_BRANCH"
else
  echo "ℹ️ Skipped commit. Run these later:"
  echo "   git add ."
  echo "   git commit -m \"feat: message\""
  echo "   git push origin $FEATURE_BRANCH"
fi

echo "🎉 Done! Open a Pull Request on GitHub."



---

✅ This way you can copy everything above into **one `README.md` file**, and your teammates will have both **notes** and a ready-to-run **bash helper script**.  

Do you want me to also add a **visual workflow diagram** (like pull → branch → push → PR → merge) inside the README for extra clarity?
