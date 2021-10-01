SHELL=/usr/bin/env bash
CLI_PROJECT = RpiLed.Cli.csproj
CLI_PATH = ./RpiLED.Cli/
GUI_PROJECT = RpiLed.Gui.csproj
GUI_PATH = ./RpiLED.Gui/
SOLUTION = RpiLED.sln
VERBOSITY = n	## {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]}
CONTAINMENT = "--self-contained"
CONFIGURATION = Release
LOGO = --nologo
OUTPUT_DIR = ./output/
BINARY_DUMP = bin/
OBJECT_DUMP = obj/
BINARIES = BINARY_DUMP OBJECT_DUMP

all : clean restore build publish

FORCE:

--clean_sln:
	dotnet clean -v $(VERBOSITY) $(LOGO) ./$(SOLUTION)

--clean_cli:
	dotnet clean -v $(VERBOSITY) $(LOGO) $(CLI_PATH)$(CLI_PROJECT)

--clean_gui:
	dotnet clean -v $(VERBOSITY) $(LOGO) $(GUI_PATH)$(GUI_PROJECT)

--clean_disk:
	rm -rf $(OUTPUT_DIR)
	rm -rf $(CLI_PATH)$(BINARY_DUMP)
	rm -rf $(CLI_PATH)$(OBJECT_DUMP)
	rm -rf $(GUI_PATH)$(BINARY_DUMP)
	rm -rf $(GUI_PATH)$(OBJECT_DUMP)

clean:
	--clean_sln
	--clean_cli
	--clean_gui

full_clean:
	clean
	--clean_disk

--restore_cli:
	--clean_cli
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(CLI_PATH)$(CLI_PROJECT)

--restore_gui:
	--clean_gui
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(GUI_PATH)$(GUI_PROJECT)

restore:
	FORCE
	clean
	--restore_cli
	--restore_gui

--build_cli:
	--restore_cli
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CLI_PATH)$(CLI_PROJECT)

--build_gui:
	--restore_gui
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(GUI_PATH)$(GUI_PROJECT)

build:
	--build_cli
	--build_gui

--publish_cli:
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(CLI_PATH)$(CLI_PROJECT)

--publish_gui:
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(GUI_PATH)$(GUI_PROJECT)

publish:
	build
	--publish_cli
	--publish_gui

cli:
	--build_cli
	dotnet run $(CLI_PATH) -h

gui:
	--build_gui
	dotnet run $(GUI_PATH)
