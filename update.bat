set t=%date%_%time%
set d=%t:~10,4%%t:~7,2%%t:~4,2%_%t:~15,2%%t:~18,2%%t:~21,2%
set d=%d: =0%

git pull
git add .
git commit -m "Update @ %d%"
git push
