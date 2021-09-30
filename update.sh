#!/usr/bin/env bash
# -*- coding: utf-8 -*-
###############################
#### VARIABLE DECLARATIONS ####
###############################
export AUTOCLEAN=0
fileName=$(basename "$0")
export fileName
branch_name="$(git rev-parse --abbrev-ref HEAD 2> /dev/null)"
export branch_name
export VERBOSE
export VERBOSITY
###############################
#### FUNCTION DECLARATIONS ####
###############################
printHelp() {
    printf "%s " "$fileName"
    printf "%s\n" "Usage help:"
    echo "##################"
    printf "%s\n" "Defaults "
    echo "-a ${AUTOCLEAN}"
    echo "-v ${VERBOSITY}"
    echo "=================="
    echo "Parameter help:"
    printf "%s\n" "-a[utomatic-cleanup] (either 0 or 1) 0 doesn't try to clean the Project/Submodule Directories"
    printf "%s\n" "-v[erbose] be verbose in output"
    printf "%s\n" "-h[elp] see this text"
}
isTrue() {
    if [[ "${*^^}" =~ ^(TRUE|OUI|Y|O$|ON$|[1-9]) ]]; then return 0;fi
    return 1
}
#################################
### MAIN routine DECLARATION ####
#################################
options=$(getopt -l "help,autoclean,verbose" -o "haV" -- "$@")
eval set -- "$options"
while true
    do
        case $1 in
            -h|--help)
                printHelp
                exit 0
                ;;
            -V|--verbose)
                VERBOSE="-v"
                VERBOSITY=true
                ;;
            -a|--autoclean)
                AUTOCLEAN=true
                ;;
            --)
                shift
                break;;
        esac
        shift
        done
##while getopts ":h:a:v:" opt; do
##    case "${opt}" in
##        h) printHelp;exit 0;;
##        a) AUTOCLEAN=true;;
##        v) VERBOSE="-v";VERBOSITY=true;;
##        \?) echo "Invalid option -$OPTARG" >&2;;
##    esac
##done
if isTrue "${VERBOSITY}"; then echo "Updating repository with increased verbosity (${VERBOSE}) || Branch Name (${branch_name})"; else echo "Updating repository quietly || Branch Name (${branch_name})"; fi
if isTrue "${AUTOCLEAN}"; then echo "This is a hard reset to the '${branch_name}' branch!!!"; fi
printf "Press [ENTER] to Run the update or 'q' to exit\n  => "
read -r -s -N 1 key
if [[ $key == $'\x71' ]];        # if input == q key
    then
        printf "Aborted!\n   exiting .."
        exit 1
    elif [[ $key == $'\x0a' ]];        # if input == ENTER key
    then
        # detached HEAD should fail us out since we dont want to pull from that
        git fetch $VERBOSE
        echo "git fetch result: $? #####"
        git status $VERBOSE
        echo "git status result: $? #####"
        if isTrue "$AUTOCLEAN"; then
            echo "Removing all untracked and ignored files AND directories"
            if isTrue "$VERBOSITY"; then git clean -xdf $VERBOSE; else clean -q -xdf $VERBOSE; fi
            echo "Resetting hard to origin ${branch_name}"
            git reset --hard $VERBOSE
        else
            echo "Pulling current state from origin ${branch_name}"
            git pull origin "$branch_name" $VERBOSE
        fi
        echo "git reset result: $? #####"
        git submodule init $VERBOSE
        echo "git submodule init result: $? #####"
        git submodule update $VERBOSE
        echo "git submodule update result: $? #####"
        exit 0
fi

