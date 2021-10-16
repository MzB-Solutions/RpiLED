# CMakeList.txt : CMake project for CMakeBlink, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.12)

# Add source to this project's executable.
# SOME_DIRECTORY("/usr/include/wiringPi.h")
set(USRLIBS "/usr/include")
target_include_directories(CMakeBlink INTERFACE ${USRLIBS})
set(SOURCES main.cpp main.h ${USRLIBS}/wiringPi.h ${USRLIBS}/stdio.h)
add_executable (CMakeBlink ${SOURCES})

# TODO: Add tests and install targets if needed.
