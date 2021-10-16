# CMakeList.txt : Top-level CMake project file, do global configuration
# and include sub-projects here.
#
cmake_minimum_required (VERSION 3.8)

project ("CMakeBlink")
set(CMAKE_MODULE_PATH ${CMAKE_MODULE_PATH} cmake/)

# Include sub-projects.
add_subdirectory ("BlinkMe")