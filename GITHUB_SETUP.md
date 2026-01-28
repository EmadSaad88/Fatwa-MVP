# Pushing to GitHub

Your local repository is ready! Follow these steps to push it to your GitHub account.

## 📋 Step 1: Create a New Repository on GitHub

1. Go to [GitHub.com](https://github.com)
2. Click **"+"** icon in the top-right corner
3. Select **"New repository"**
4. Configure the repository:
   - **Repository name**: `Fatwa-MVP` (or your preferred name)
   - **Description**: "ASP.NET Core Fatwa Q&A System"
   - **Visibility**: Choose **Public** or **Private**
   - **Do NOT** add README, .gitignore, or license (we already have these)
5. Click **"Create repository"**

## 📌 Step 2: Connect Your Local Repository to GitHub

After creating the repository, GitHub will show you commands. Use these commands:

```bash
# Set the remote URL (replace YOUR_USERNAME and REPO_NAME)
git remote add origin https://github.com/YOUR_USERNAME/Fatwa-MVP.git

# Or if using SSH (requires SSH key setup):
# git remote add origin git@github.com:YOUR_USERNAME/Fatwa-MVP.git

# Rename the branch to main (if needed)
git branch -M main

# Push to GitHub
git push -u origin main
```

## 🔑 Step 3a: Using HTTPS (Easier for First Time)

If using HTTPS, GitHub will prompt for a Personal Access Token:

1. Go to [GitHub Settings → Developer settings → Personal access tokens](https://github.com/settings/tokens)
2. Click **"Generate new token"** → **"Generate new token (classic)"**
3. Give it a name like "fatwa-mv-push"
4. Select scopes: `repo` (full control of private repositories)
5. Click **"Generate token"** and copy it
6. When prompted during `git push`, use the token as the password

## 🔑 Step 3b: Using SSH (More Secure, Recommended)

1. **Generate SSH key** (if you don't have one):
   ```bash
   ssh-keygen -t ed25519 -C "your.email@example.com"
   ```

2. **Add SSH key to GitHub**:
   - Go to [GitHub Settings → SSH and GPG keys](https://github.com/settings/keys)
   - Click **"New SSH key"**
   - Paste your public key (from `~/.ssh/id_ed25519.pub`)

3. **Use SSH URL** when connecting:
   ```bash
   git remote add origin git@github.com:YOUR_USERNAME/Fatwa-MVP.git
   ```

## 🚀 Step 4: Push to GitHub

```bash
# Push the code
git push -u origin main
```

Or if your default branch is `master`:
```bash
git push -u origin master
```

## ✅ Verification

After pushing, verify on GitHub:
1. Go to your repository on GitHub
2. You should see all files listed
3. Check the commit history (click "Commits")
4. Verify the README is displayed

## 📝 Complete Commands Summary

### Quick Start (HTTPS)
```bash
# From your Fatwa_MVP directory
git remote add origin https://github.com/YOUR_USERNAME/Fatwa-MVP.git
git branch -M main
git push -u origin main
```

### Quick Start (SSH)
```bash
# From your Fatwa_MVP directory
git remote add origin git@github.com:YOUR_USERNAME/Fatwa-MVP.git
git branch -M main
git push -u origin main
```

## 🐛 Troubleshooting

### "fatal: remote origin already exists"
```bash
# Remove the existing remote and try again
git remote remove origin
git remote add origin https://github.com/YOUR_USERNAME/Fatwa-MVP.git
git push -u origin main
```

### "fatal: Authentication failed"
- **HTTPS**: Make sure you're using a Personal Access Token, not your password
- **SSH**: Verify your SSH key is added to GitHub and your ssh-agent is running

### "rejected: updates were rejected because the tip of your current branch is behind"
```bash
# Pull the remote changes first
git pull origin main

# Then push
git push origin main
```

## 📚 After Pushing

Once pushed, you can:

1. **Add collaborators**: Settings → Collaborators
2. **Set up GitHub Pages**: Settings → Pages (to host documentation)
3. **Create releases**: Go to Releases and create version tags
4. **Add a license**: Create a LICENSE file on GitHub
5. **Enable GitHub Actions**: For CI/CD pipelines
6. **Create issues/discussions**: For tracking features and bugs

## 🔄 Future Updates

After the initial push, updating is simple:

```bash
# Make your changes, then:
git add .
git commit -m "Your message"
git push
```

No need for the `-u` flag after the first push!

---

**Good luck pushing to GitHub!** 🚀
