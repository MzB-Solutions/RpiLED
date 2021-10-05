SHELL=/usr/bin/env bash
CLI_PROJECT=RpiLed.Cli.csproj
CLI_PATH=./RpiLED.Cli/
GUI_PROJECT=RpiLed.Gui.csproj
GUI_PATH=./RpiLED.Gui/
CORE_PROJECT=RpiLed.Core.csproj
CORE_PATH=./RpiLED.Core/
# ConsoLovers.Core
VENDOR_LIB_ConsoLovers=./vendor/ConsoLovers/src/
VENDOR_PROJECT_PATH_ConsoLoversCore=ConsoLovers.ConsoleToolkit.Core/
VENDOR_PROJECT_ConsoLoversCore=ConsoLovers.ConsoleToolkit.Core.csproj
VENDOR_PROJECT_ConsoLoversCore_file=$(VENDOR_LIB_ConsoLovers)$(VENDOR_PROJECT_PATH_ConsoLoversCore)$(VENDOR_PROJECT_ConsoLoversCore)
## all vendors ...
VENDOR1=$(VENDOR_PROJECT_ConsoLoversCore_file)
SOLUTION=RpiLED.sln

## {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]}
VERBOSITY=m
CONFIGURATION=Release
LOGO=--nologo
OUTPUT_DIR=./output/
BINARY_DUMP=bin/
OBJECT_DUMP=obj/
BINARIES=$(BINARY_DUMP) $(OBJECT_DUMP)
_CLEAN=dotnet clean -v $(VERBOSITY)
_RESTORE=dotnet restore --force --force-evaluate -v $(VERBOSITY)
_BUILD=dotnet build -v $(VERBOSITY)
_PUBLISH=dotnet publish -v $(VERBOSITY)
_RUN=dotnet run -v $(VERBOSITY)
_RMF=rm -vf
_RMD=rm -vrf

# as per https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
## This app specifically is aimed at RaspBerryPi's, so linux-arm is the only logical choice
## win-arm;win-arm64;linux-arm;linux-arm64
RUNTIME=linux-arm
CONTAINMENT=--self-contained true

####################
## Public Targets ##
####################
all: clean restore build publish

clean: --clean_all
	$(info ************  Cleaned Solution and NuGet packages  ************)

full_clean: --clean_disk clean

restore: clean --restore_cli --restore_gui
	$(info ************  Restoring NuGet packages  ************)
	touch $@

configure: restore

build: build-cli build-gui
	$(info ************  Built both RpiLED.Cli & RpiLED.Gui and their dependencies  ************)
	touch $@

build-cli: --build_cli
	$(info ************  Built RpiLED.Cli and its dependencies  ************)
	touch $@

build-gui: --build_gui
	$(info ************  Built RpiLED.Gui and its dependencies  ************)
	touch $@

publish: publish-cli publish-gui
	$(info ************  Published both RpiLED.Cli & RpiLED.Gui  ************)
	touch $@

publish-cli: --publish_cli
	$(info ************  Published RpiLED.Cli  ************)
	touch $@

publish-gui: --publish_gui
	$(info ************  Published RpiLED.Gui  ************)
	touch $@

cli: build-cli
	$(info ************  Running RpiLED.Cli  ************)
	$(_RUN) --project $(CLI_PATH)

gui: build-gui
	$(info ************  Running RpiLED.Gui  ************)
	$(_RUN) --project $(GUI_PATH)

#####################
## Private targets ##
#####################

--clean_extras:
	$(info ************  Cleaning libraries  ************)
	$(_CLEAN) $(LOGO) $(CORE_PATH)$(CORE_PROJECT)
	$(_CLEAN) $(LOGO) $(VENDOR1)

--clean_makefile_markers:
	$(info ************  Cleaning makefile markers  ************)
	$(_RMF) clean
	$(_RMF) restore
	$(_RMF) build*
	$(_RMF) publish*

--clean_cli:
	$(info ************  Cleaning RpiLED.Cli  ************)
	$(_CLEAN) $(LOGO) $(CLI_PATH)$(CLI_PROJECT)

--clean_gui:
	$(info ************  Cleaning RpiLED.Gui  ************)
	$(_CLEAN) $(LOGO) $(GUI_PATH)$(GUI_PROJECT)

--clean_output:
	$(info  ************ Cleaning ./output/ directory  ************)
	$(_RMD) $(OUTPUT_DIR)

--clean_disk: --clean_makefile_markers
	$(warning ************  This deletes all assets, obj files and build-states  ************)
	$(_RMD) $(CORE_PATH)$(BINARY_DUMP)
	$(_RMD) $(CORE_PATH)$(OBJECT_DUMP)
	$(_RMD) $(VENDOR_LIB_ConsoLovers)$(VENDOR_PROJECT_PATH_ConsoLoversCore)$(BINARY_DUMP)
	$(_RMD) $(VENDOR_LIB_ConsoLovers)$(VENDOR_PROJECT_PATH_ConsoLoversCore)$(OBJECT_DUMP)
	$(_RMD) $(CLI_PATH)$(BINARY_DUMP)
	$(_RMD) $(CLI_PATH)$(OBJECT_DUMP)
	$(_RMD) $(GUI_PATH)$(BINARY_DUMP)
	$(_RMD) $(GUI_PATH)$(OBJECT_DUMP)

--clean_sln: --clean_output --clean_cli --clean_gui --clean_extras

--clean_all: --clean_sln
	$(info ************  Cleaning Solution (.sln)  ************)
	$(_CLEAN) $(LOGO) ./$(SOLUTION)

--restore_cli: --clean_cli
	$(info ************  Restoring RpiLED.Cli  ************)
	$(_RESTORE) -r $(RUNTIME) $(CLI_PATH)$(CLI_PROJECT)

--restore_gui: --clean_gui
	$(info  ************  Restoring RpiLED.Gui  ************)
	$(_RESTORE) -r $(RUNTIME) $(GUI_PATH)$(GUI_PROJECT)

--build_cli: --restore_cli
	$(info ************  Building RpiLED.Cli  ************)
	$(_BUILD) --no-restore $(LOGO) -r $(RUNTIME) -c $(CONFIGURATION) $(CLI_PATH)$(CLI_PROJECT)

--build_gui: --restore_gui
	$(info ************  Building RpiLED.Gui  ************)
	$(_BUILD) --no-restore $(LOGO) -r $(RUNTIME) -c $(CONFIGURATION) $(GUI_PATH)$(GUI_PROJECT)

--publish_cli: --build_cli
	$(info ************  Publishing RpiLED.Cli  ************)
	$(_PUBLISH) --no-build $(LOGO) $(CONTAINMENT) -r $(RUNTIME) -c $(CONFIGURATION) -o $(OUTPUT_DIR) $(CLI_PATH)$(CLI_PROJECT)

--publish_gui: --build_gui
	$(info ************  Publishing RpiLED.Gui  ************)
	$(_PUBLISH) --no-build $(LOGO) $(CONTAINMENT) -r $(RUNTIME) -c $(CONFIGURATION) -o $(OUTPUT_DIR) $(GUI_PATH)$(GUI_PROJECT)

