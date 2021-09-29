#!/usr/bin/env bash
VERBOSITY=q
AUTO_MODE=true
CONTAINMENT="--self-contained"
TARGET=Cli
##
function isTrue() {
    if [[ "${@^^}" =~ ^(TRUE|OUI|Y|O$|ON$|[1-9]) ]]; then return 0;fi
    return 1
}
while getopts ":a:v:c_t:" opt; do
    case $opt in
        a) AUTO_MODE="$OPTARG"
           ;;
        v)
            VERBOSITY="$OPTARG"
            ;;
        c) 
            CONTAINMENT="$OPTARG"
            ;;
        t)
            TARGET="$OPTARG"
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
        dotnet clean -v $VERBOSITY $LOGO -c $CONFIG "./RpiLED.${TARGET}/RpiLED.${TARGET}.csproj"
        cleanState=$?
        echo "Cleaning finished ... Result: ${cleanState}"
        echo "Restoring packages and dependencies..."
        dotnet restore -v $VERBOSITY --force --force-evaluate "./RpiLED.${TARGET}/RpiLED.${TARGET}.csproj"
        restoreState=$?
        echo "Restore finished ... Result: ${restoreState}"
        echo "Build started ..."
        dotnet build -v $VERBOSITY --no-restore $LOGO -c $CONFIG "./RpiLED.${TARGET}/RpiLED.${TARGET}.csproj"
        buildState=$?
        echo "Build finished ... Result: ${buildState}"
        echo "Publishing Solutions into ${OUTPUT_DIR}"
        dotnet publish -v $VERBOSITY $CONTAINMENT --no-build -c $CONFIG $LOGO -o "${OUTPUT_DIR}" "./RpiLED.${TARGET}/RpiLED.${TARGET}.csproj"
        publishState=$?
        echo "Publishing finished as ${publishState} in ${OUTPUT_DIR}"
        echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) |  Build&Publish (${buildState}|${publishState}) | [0 = Program executed ok!] [!0 = Some error(number) occured]"
        printf "All Tasks completed!\n   exiting .."
        exit 0
    fi
else
    dotnet clean -v $VERBOSITY $LOGO -c $CONFIG
    cleanState=$?
    dotnet restore -v $VERBOSITY --force --force-evaluate
    restoreState=$?
    dotnet build -v $VERBOSITY --no-restore $LOGO -c $CONFIG
    buildState=$?
    dotnet publish -v $VERBOSITY $CONTAINMENT --no-build -c $CONFIG $LOGO -o "${OUTPUT_DIR}"
    publishState=$?
    echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) |  Build&Publish (${buildState}|${publishState}) | [0 = Program executed ok!] [!0 = Some error(number) occured]"
    printf "All Tasks completed!\n   exiting .."
    exit 0
fi