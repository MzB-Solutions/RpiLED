#!/usr/bin/env bash
VERBOSITY=q
AUTO_MODE=true
##
function isTrue() {
    if [[ "${@^^}" =~ ^(TRUE|OUI|Y|O$|ON$|[1-9]) ]]; then return 0;fi
    return 1
}
while getopts ":a:v:" opt; do
    case $opt in
        a) AUTO_MODE="$OPTARG"
           ;;
        v)
            VERBOSITY="$OPTARG"
            ;;
        \?) echo "Invalid option -$OPTARG" >&2
            ;;
    esac
done
## Verbosity Levels:
## q[uiet]
## m[inimal]
## n[ormal]
## d[etailed]
## diag[nostic]
CURRENT_DIR=$(pwd)
OUTPUT_DIR="${CURRENT_DIR}/output/"
if [[ -d $OUTPUT_DIR ]];
then
    echo "Removing output directory ${OUTPUT_DIR}"
    rm -rf "$OUTPUT_DIR"
fi
CONFIG="Release"
LOGO="--nologo"
if ! isTrue "$AUTO_MODE"; then
    echo "building a ${CONFIG}-build into ${OUTPUT_DIR}, verbosity (${VERBOSITY}) {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]}"
    printf "Press [ENTER] to Run the build or 'q' to exit\n  => "
    read -r -s -N 1 key
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
        echo "Build started ..."
        dotnet build -v $VERBOSITY --force $LOGO -c $CONFIG -o "${OUTPUT_DIR}"
        buildState=$?
        echo "Build finished ... Result: ${buildState}"
        echo "Task completed! Results: Clean (${cleanState}) |  Build (${buildState}) | [0 = Program executed successfully!] [!0 = Some error(number) occured]"
        printf "All Tasks completed!\n   exiting .."
        exit 0
    fi
else
    dotnet clean -v $VERBOSITY $LOGO -c $CONFIG
    cleanState=$?
    dotnet restore -v $VERBOSITY --force --force-evaluate
    restoreState=$?
    dotnet build -v $VERBOSITY --force --no-restore $LOGO -c $CONFIG -o "${OUTPUT_DIR}"
    buildState=$?
    echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) | Build (${buildState}) | [0 = Program executed successfully!] [!0 = Some error(number) occured]"
    printf "All Tasks completed!\n   exiting .."
    exit 0
fi