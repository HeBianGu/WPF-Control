:: 标签操作，使用 git tag -d <tag_name> 删除本地标签，使用 git push origin --delete <tag_name> 删除远程标签
@echo off
cd /d "%~dp0"
:: 查看标签
git tag
:: 创建标签
git tag v1.0.5
:: 推送所有标签
git push origin --tags
pause

