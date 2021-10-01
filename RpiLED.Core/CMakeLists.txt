project(RpiLED.Core VERSION 1.0.0.0 LANGUAGES CSharp)
include(CSharpUtilities)
# Create assembly info with current version.
CONFIGURE_FILE("Properties/AssemblyInfo.cs.template" "Properties/AssemblyInfo.cs")
