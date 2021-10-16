# CMakeList.txt : CMake project for CMakeRightApp, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.8)
project ("srMe")

#find_package(WiringPi REQUIRED)
FILE(TO_CMAKE_PATH "C:/Users/smzb_code/AppData/Local/Microsoft/Linux/HeaderCache/1.0/-114093420/usr/include" WIRINGPI_INCLUDE_DIRS)
FILE(TO_CMAKE_PATH "C:/Users/smzb_code/AppData/Local/Microsoft/Linux/HeaderCache/1.0/-114093420/usr/include/arm-linux-gnueabihf/bits" ARM_INCLUDE_DIRS)
include_directories(${WIRINGPI_INCLUDE_DIRS})
include_directories(${ARM_INCLUDE_DIRS})
# Add source to this project's executable.
add_executable (srMe "main.cpp" "main.h")
# TODO: Add tests and install targets if needed.
