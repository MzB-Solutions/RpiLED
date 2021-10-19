cmake_minimum_required(VERSION 3.18.0)
project(ShiftRegister VERSION 0.1.2 LANGUAGES CXX)

#include(CTest)
#enable_testing()

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS ON)

file(TO_CMAKE_PATH "C:/Users/smzb_code/AppData/Local/Microsoft/Linux/HeaderCache/1.0/-114093420/usr/include" WININCLUDES)
file(TO_CMAKE_PATH "C:/Qt/install/include" qt6INCLUDES)
file(TO_CMAKE_PATH "/usr/include" NIXINCLUDES)
# Locate libraries and headers
find_library(WIRINGPI_LIBRARIES
    NAMES wiringPi
    HINTS "${WININCLUDES}/" "${NIXINCLUDES}/"
    )

find_library(QT6_LIBRARIES,
    names Qt
    HINTS ${qt6INCLUDES}
    )

set(SOURCE "src/main.cpp")
set(HEADER "include/main.h")

add_executable(ShiftRegister ${SOURCE} ${HEADER}
target_link_libraries(ShiftRegister ${WIRINGPI_LIBRARIES})
target_link_libraries(ShiftRegister ${QT6_LIBRARIES})

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
