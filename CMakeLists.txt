cmake_minimum_required(VERSION 3.18.0)
project(ShiftRegister VERSION 0.1.2 LANGUAGES CXX)

# Add The CPM Package Manager
if(NOT EXISTS "${CMAKE_HOME_DIRECTORY}/cmake/CPM.cmake")
  file(DOWNLOAD https://raw.githubusercontent.com/TheLartians/CPM.cmake/master/cmake/CPM.cmake "${CMAKE_HOME_DIRECTORY}/cmake/CPM.cmake")
endif()

include(cmake/CPM.cmake)

# Add The console-component
CPMAddPackage(
  NAME console
  GITHUB_REPOSITORY edenreich/console-component
  VERSION 1.0.6
  OPTIONS
    "WITH_TESTS Off"
)

#include(CTest)
#enable_testing()

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS ON)

# Move The Default Directory Of The Executable To bin Directory
set(CMAKE_RUNTIME_OUTPUT_DIRECTORY ${CMAKE_CURRENT_SOURCE_DIR}/bin)

file(TO_CMAKE_PATH "C:/Users/smzb_code/AppData/Local/Microsoft/Linux/HeaderCache/1.0/-114093420/usr/include" WININCLUDES)
file(TO_CMAKE_PATH "/usr/include" NIXINCLUDES)
# Locate libraries and headers
find_library(WIRINGPI_LIBRARIES
    NAMES wiringPi
    HINTS "${WININCLUDES}/" "${NIXINCLUDES}/"
    )

set(SOURCES_CXX_FILES
    "src/main.cpp"
    # add your implemention files here..
)
set(HEADER "include/main.hpp")

add_executable(${PROJECT_NAME} ${SOURCES_CXX_FILES} ${HEADER})
include_directories(${CMAKE_BINARY_DIR}/dist/include)
target_link_libraries(${PROJECT_NAME} ${WIRINGPI_LIBRARIES})
target_link_libraries(${PROJECT_NAME} console)

set(CPACK_PROJECT_NAME ${PROJECT_NAME})
set(CPACK_PROJECT_VERSION ${PROJECT_VERSION})
include(CPack)
