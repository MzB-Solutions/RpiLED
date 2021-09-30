#!/usr/bin/env bash
# -*- coding: utf-8 -*-
AUTOCLEAN=0
fileName=$(basename "$0")
function printHelp() {
    printf "%s " "$fileName"
    printf "%s\n" "Usage help:"
    echo "##################"
    printf "%s\n" "Defaults "
    echo "-a ${AUTOCLEAN} "
    echo "=================="
    echo "Parameter help:"
    printf "%s\n" "-a[utomatic-cleanup] (either 0 or 1) 0 doesn't try to clean the Project/Submodule Directories"
    printf "%s\n" "-v[erbose] be verbose in output"
    printf "%s\n" "-h[elp] see this text"
    exit 0
}
function isTrue() {
    if [[ "${*^^}" =~ ^(TRUE|OUI|Y|O$|ON$|[1-9]) ]]; then return 0;fi
    return 1
}
while getopts ":h:a:v:" opt; do
    case $opt in
        h) PRINTHELP=1;;
        a) AUTOCLEAN="$OPTARG";;
        v) VERBOSE="-v";;
        \?) echo "Invalid option -$OPTARG" >&2;;
    esac
done
if isTrue "$PRINTHELP"; then
    printHelp;
fi
git fetch $VERBOSE
echo "git fetch result: $? #####"
git status $VERBOSE
echo "git status result: $? #####"
if [[ "$AUTOCLEAN" -eq 1 ]]; then
     rm -rf ./vendor/ConsoLovers/
     rm -rf ./output/
fi
git reset --hard $VERBOSE
echo "git reset result: $? #####"
git submodule init $VERBOSE
echo "git submodule init result: $? #####"
git submodule update $VERBOSE
echo "git submodule update result: $? #####"