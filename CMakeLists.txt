cmake_minimum_required(VERSION 3.18.0)
project(ShiftRegister VERSION 0.1.2 LANGUAGES CXX)

#include(CTest)
#enable_testing()

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS ON)

# Locate libraries and headers
find_library(WIRINGPI_LIBRARIES NAMES wiringPi)

set(SOURCE "src/main.cpp")
set(HEADERS "include/main.h")

add_executable(ShiftRegister ${SOURCE} ${HEADERS})
target_link_libraries(ShiftRegister ${WIRINGPI_LIBRARIES})

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
