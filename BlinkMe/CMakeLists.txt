# CMakeList.txt : CMake project for CMakeBlink, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)

# Add source to this project's executable.
# SOME_DIRECTORY("/usr/include/wiringPi.h")
SOME_DIRECTORY("/usr/include")
target_include_directories(usr_includes INTERFACE ${SOME_DIRECTORY})
set(SOURCES main.cpp main.h ${SOME_DIRECTORY}/wiringPi.h)

add_executable (CMakeBlink ${SOURCES})

# TODO: Add tests and install targets if needed.
