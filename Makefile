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
VERBOSITY=n
CONFIGURATION=Release
LOGO=--nologo
OUTPUT_DIR=./output/
BINARY_DUMP=bin/
OBJECT_DUMP=obj/
BINARIES=BINARY_DUMP OBJECT_DUMP

# as per https://docs.microsoft.com/en-us/dotnet/core/rid-catalog
RUNTIME=linux-arm

.PHONY: clean restore build publish

FORCE:

--clean_extras:
	dotnet clean -v $(VERBOSITY) $(LOGO) $(CORE_PATH)$(CORE_PROJECT)
	dotnet clean -v $(VERBOSITY) $(LOGO) $(VENDOR1)

--clean_makefile_markers:
	rm -f build
	rm -f publish

--clean_cli:
	dotnet clean -v $(VERBOSITY) $(LOGO) $(CLI_PATH)$(CLI_PROJECT)

--clean_gui:
	dotnet clean -v $(VERBOSITY) $(LOGO) $(GUI_PATH)$(GUI_PROJECT)

--clean_disk: --clean_makefile_markers
	rm -rf $(OUTPUT_DIR)
	rm -rf $(CORE_PATH)$(BINARY_DUMP)
	rm -rf $(CORE_PATH)$(OBJECT_DUMP)
	rm -rf $(VENDOR_LIB_ConsoLovers)$(VENDOR_PROJECT_PATH_ConsoLoversCore)$(BINARY_DUMP)
	rm -rf $(VENDOR_LIB_ConsoLovers)$(VENDOR_PROJECT_PATH_ConsoLoversCore)$(OBJECT_DUMP)
	rm -rf $(CLI_PATH)$(BINARY_DUMP)
	rm -rf $(CLI_PATH)$(OBJECT_DUMP)
	rm -rf $(GUI_PATH)$(BINARY_DUMP)
	rm -rf $(GUI_PATH)$(OBJECT_DUMP)

--clean_sln: --clean_cli --clean_gui --clean_extras

--clean_all: --clean_sln
	dotnet clean -v $(VERBOSITY) $(LOGO) ./$(SOLUTION)

clean: --clean_sln

full_clean: --clean_disk clean

--restore_cli: --clean_cli
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(CLI_PATH)$(CLI_PROJECT)

--restore_gui: --clean_gui
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(GUI_PATH)$(GUI_PROJECT)

restore: FORCE clean --restore_cli --restore_gui

--build_cli: --restore_cli
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) $(CLI_PATH)$(CLI_PROJECT)

--build_gui: --restore_gui
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) $(GUI_PATH)$(GUI_PROJECT)

build: --build_cli --build_gui
	touch $@

--publish_cli: --build_cli
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -o $(OUTPUT_DIR) $(CLI_PATH)$(CLI_PROJECT)

--publish_gui: --build_gui
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -o $(OUTPUT_DIR) $(GUI_PATH)$(GUI_PROJECT)

publish: build --publish_cli --publish_gui
	touch $@

cli: --build_cli
	dotnet run --project $(CLI_PATH) -- -h

gui: --build_gui
	dotnet run --project $(GUI_PATH)
