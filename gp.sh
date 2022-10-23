#!/bin/sh
if [ ${#1} -eq 0 ]; then
  echo "Enter commit string."
  exit 1
fi
echo "Using commit string: $1"
DATE=`date +%Y%m%d-%H%M`
echo "Adding Files."
git add . --all
echo "Committing Files."
git commit -m "$DATE $1"
echo "Pushing Files."
git push -u origin master
#git push -u origin 20221006-Updates
#git push -u origin 20221018-updates
#git push -u origin EB
