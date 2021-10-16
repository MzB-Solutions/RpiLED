# CMakeList.txt : CMake project for CMakeBlink, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.12)

# Add source to this project's executable.
# SOME_DIRECTORY("/usr/include/wiringPi.h")
set(USRLIBS "/usr/include")
add_library(stdio ${USRLIBS}/stdio.h)
add_library(wpi ${USRLIBS}/wiringPi.h)
#target_include_directories(stdio INTERFACE ${USRLIBS}/stdio.h)
#target_include_directories(wpi INTERFACE ${USRLIBS}/wiringPi.h)
set(SOURCES main.cpp main.h)
add_executable (CMakeBlink ${SOURCES})
#target_include_directories(CMakeBlink INTERFACE ${SOURCES})
target_link_libraries(CMakeBlink stdio wpi)

# TODO: Add tests and install targets if needed.
