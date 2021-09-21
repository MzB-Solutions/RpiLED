#!/usr/bin/env bash
##
## Verbosity Levels:
## q[uiet]
## m[inimal]
## n[ormal]
## d[etailed]
## diag[nostic]
VERBOSITY=n
CURRENT_DIR=$(pwd)
OUTPUT_DIR="${CURRENT_DIR}/output/"
CONFIG="Release"
LOGO="--nologo"
echo "building a ${CONFIG}-build into ${OUTPUT_DIR}, verbosity (${VERBOSITY}) {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]}"
printf "Press [ENTER] to Run the build or 'q' to exit\n  => "
read -s -N 1 key
if [[ $key == $'\x71' ]];        # if input == q key
then
    printf "Aborted!\n   exiting .."
    exit 255
elif [[ $key == $'\x0a' ]];        # if input == ENTER key
then
    dotnet clean -v $VERBOSITY $LOGO -c $CONFIG
    cleanState=$?
    dotnet restore -v $VERBOSITY --force --force-evaluate
    restoreState=$?
    dotnet build -v $VERBOSITY --force --no-restore $LOGO -c $CONFIG -o "${OUTPUT_DIR}"
    buildState=$?
    echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) | Build (${buildState})"
    printf "All Tasks completed!\n   exiting .."
    exit 0
fi
