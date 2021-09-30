#!/usr/bin/env bash
# -*- coding: utf-8 -*-
VERBOSITY=q
AUTO_MODE=true
CONTAINMENT="--self-contained"
TARGET=Cli
##PRINTHELP=0
fileName=$(basename "$0")

function printHelp() {
    printf "%s " "$fileName"
    printf "%s\n" "Usage help:"
    echo "##################"
    printf "%s\n" "Defaults "
    echo -n "-a ${AUTO_MODE} "
    echo -n "-v ${VERBOSITY} "
    echo -n "-c ${CONTAINMENT} "
    echo "-t ${TARGET}"
    echo "=================="
    echo "Parameter help:"
    printf "%s\n" "-a[utomatic-mode] (either 0 or 1) 0 asks for confirmation before it runs."
    printf "%s\n" "-v[verbosity] (q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic])"
    printf "%s\n" "-c[ontainment] (either '--self-contained' or '--no-self-contained')"
    printf "%s\n" "-t[arget] (either 'Cli' or 'Gui')"
    printf "%s\n" "-h[elp] see this text"
    exit 0
}

function isTrue() {
    if [[ "${*^^}" =~ ^(TRUE|OUI|Y|O$|ON$|[1-9]) ]]; then return 0;fi
    return 1
}
while getopts ":a:v:c:t:h:" opt; do
    case $opt in
        h) PRINTHELP=1;;
        a) AUTO_MODE="$OPTARG";;
        v) VERBOSITY="$OPTARG";;
        c) CONTAINMENT="$OPTARG";;
        t) TARGET="$OPTARG";;
        \?) echo "Invalid option -$OPTARG" >&2;;
    esac
done
if isTrue "$PRINTHELP"; then
    printHelp;
fi
CURRENT_DIR=$(pwd)
OUTPUT_DIR="${CURRENT_DIR}/output"
if [[ -d $OUTPUT_DIR ]];
then
    echo "Removing output directory '${OUTPUT_DIR}'"
    rm -rf "$OUTPUT_DIR"
fi
#build_dir="${OUTPUT_DIR}/build"
OUTPUT_DIR="${OUTPUT_DIR}/${TARGET}"
WORKING_DIR="${CURRENT_DIR}/RpiLED.${TARGET}"
PROJECT_FILE="RpiLed.${TARGET}.csproj"
CONFIG="Release"
LOGO="--nologo"
directory_status=pushd "$WORKING_DIR" || directory_status=1;exit 1
if ! isTrue "$AUTO_MODE"; then
    echo "building RpiLED.${TARGET} ${CONFIG}-build into ${OUTPUT_DIR}"
    echo " verbosity (${VERBOSITY}) {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]} || Directory Status (${WORKING_DIR}):${directory_status}"
    printf "Press [ENTER] to Run the build or 'q' to exit\n  => "
    read -r -s -N 1 key
    if [[ $key == $'\x71' ]];        # if input == q key
    then
        printf "Aborted!\n   exiting .."
        exit 255
    elif [[ $key == $'\x0a' ]];        # if input == ENTER key
    then
        echo "Cleaning started ..."
        dotnet clean -v "$VERBOSITY" $LOGO -c $CONFIG "${PROJECT_FILE}"
        cleanState=$?
        echo "Cleaning finished ... Result: ${cleanState}"
        echo "Restoring packages and dependencies..."
        dotnet restore -v "$VERBOSITY" --force --force-evaluate "${PROJECT_FILE}"
        restoreState=$?
        echo "Restore finished ... Result: ${restoreState}"
        echo "Build started ..."
        dotnet build -v "$VERBOSITY" --no-restore $LOGO -c $CONFIG "${PROJECT_FILE}"
        buildState=$?
        echo "Build finished ... Result: ${buildState}"
        echo "Publishing Solutions into ${OUTPUT_DIR}"
        dotnet publish -v "$VERBOSITY" "$CONTAINMENT" --no-build -c $CONFIG $LOGO -o "${OUTPUT_DIR}" "${PROJECT_FILE}"
        publishState=$?
        echo "Publishing finished as ${publishState} in ${OUTPUT_DIR}"
        echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) |  Build&Publish (${buildState}|${publishState}) | [0 = Program executed ok!] [!0 = Some error(number) occured]"
        printf "All Tasks completed!\n   exiting .."
        popd || directory_status=1
        if [[ "$restoreState" -eq 0 ]] && [[ "$buildState" -eq 0 ]] && [[ "$publishState" -eq 0 ]] && [[ "$directory_status" -eq 0 ]]; then exit 0; else exit 1; fi
    fi
else
    echo "building RpiLED.${TARGET} ${CONFIG}-build into ${OUTPUT_DIR}"
    echo " verbosity (${VERBOSITY}) {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]} || Directory Status (${WORKING_DIR}):${directory_status}"
    dotnet clean -v "$VERBOSITY" $LOGO -c $CONFIG "./RpiLED.${TARGET}/RpiLed.${TARGET}.csproj"
    cleanState=$?
    dotnet restore -v "$VERBOSITY" --force --force-evaluate "${PROJECT_FILE}"
    restoreState=$?
    dotnet build -v "$VERBOSITY" --no-restore $LOGO -c $CONFIG "${PROJECT_FILE}"
    buildState=$?
    dotnet publish -v "$VERBOSITY" "$CONTAINMENT" --no-build -c $CONFIG $LOGO -o "${OUTPUT_DIR}" "${PROJECT_FILE}"
    publishState=$?
    echo "Task completed! Results: Clean (${cleanState}) | Restore (${restoreState}) |  Build&Publish (${buildState}|${publishState}) | [0 = Program executed ok!] [!0 = Some error(number) occured]"
    printf "All Tasks completed!\n   exiting .."
    popd || directory_status=1
    if [[ "$restoreState" -eq 0 ]] && [[ "$buildState" -eq 0 ]] && [[ "$publishState" -eq 0 ]] && [[ "$directory_status" -eq 0 ]]; then exit 0; else exit 1; fi
fi