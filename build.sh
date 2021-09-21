#!/usr/bin/env bash
##
## Verbosity Levels:
## q[uiet]
## m[inimal]
## n[ormal]
## d[etailed]
## diag[nostic]
VERBOSITY=m
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
    echo "Cleaning started ..."
    dotnet clean -v $VERBOSITY $LOGO -c $CONFIG
    cleanState=$?
    echo "Cleaning finished ... Result: ${cleanState}"
    echo "Restore started ..."
    dotnet restore -v $VERBOSITY --force --force-evaluate
    restoreState=$?
    echo "Restore finished ... Result: ${restoreState}"
    echo "Build started ..."
    dotnet build -v $VERBOSITY --force --no-restore $LOGO -c $CONFIG -o "${OUTPUT_DIR}"
    buildState=$?
    echo "Build finished ... Result: ${buildState}"
    echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) | Build (${buildState}) | [0 = Program executed successfully!] [!0 = Some error(number) occured]"
    printf "All Tasks completed!\n   exiting .."
    exit 0
fi
