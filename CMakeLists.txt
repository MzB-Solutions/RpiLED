# CMakeList.txt : CMake project for RpiLED.CMake, include source and define
# project specific logic here.
#
cmake_minimum_required (VERSION 3.18)
PROJECT(RpiLED LANGUAGES CSharp)

# Set global .NET Framework version.
# 5.0?
#SET(CMAKE_DOTNET_TARGET_FRAMEWORK_VERSION "v4.7.2")
SET(CMAKE_DOTNET_TARGET_FRAMEWORK "net5.0")

SET(CMAKE_CSharp_FLAGS "/langversion:9.0")

# Explicitly set the `PlatformTarget` for C# projects, since AnyCPU can result in
# System.BadImageFormatException throws, when trying to load C++/CLI assemblies.
IF(CMAKE_GENERATOR_PLATFORM STREQUAL "Win32")
  SET(CMAKE_CSharp_FLAGS "/platform:x86")
ELSEIF(CMAKE_GENERATOR_PLATFORM STREQUAL "ARM")
    SET(CMAKE_CSharp_FLAGS "/platform:arm")
ELSE()
  MESSAGE(WARNING "Generator platform is set to '${CMAKE_GENERATOR_PLATFORM}', which is not supported by managed projects. Defaulting to 'AnyCPU'...")
  SET(CMAKE_CSharp_FLAGS "/platform:AnyCPU")
ENDIF()

# Set the C++ standard for all projects.
SET(CMAKE_CXX_STANDARD 17)

# Define runtime intermediate build directory, so that debugging the Visual Studio solution works as expected.
SET(CMAKE_RUNTIME_OUTPUT_DIRECTORY "${CMAKE_BINARY_DIR}/binaries/")

# Set export directory for CMake config.
SET(CMAKE_INSTALL_BINARY_DIR    "bin/")
SET(CMAKE_INSTALL_LIBRARY_DIR   "bin/")
SET(CMAKE_INSTALL_INCLUDE_DIR   "include/")
SET(CMAKE_INSTALL_EXPORT_DIR    "cmake/")

# Include sub-projects.
ADD_SUBDIRECTORY(RpiLED.Core)
#ADD_SUBDIRECTORY(RpiLED.Cli)
#ADD_SUBDIRECTORY(RpiLED.Gui)
#ADD_SUBDIRECTORY(vendor/ConsoLovers)

# Add source to this project's executable.
# from original template
#add_executable (RpiLED.CMake "RpiLED.CMake.cpp" "RpiLED.CMake.h")

# TODO: Add tests and install targets if needed.
