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
	$(MAKE) --clean_sln
	$(MAKE) --clean_cli
	$(MAKE) --clean_gui

full_clean:
	$(MAKE) clean
	$(MAKE) --clean_disk

--restore_cli:
	$(MAKE) --clean_cli
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(CLI_PATH)$(CLI_PROJECT)

--restore_gui:
	$(MAKE) --clean_gui
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(GUI_PATH)$(GUI_PROJECT)

restore:
	$(MAKE) FORCE
	$(MAKE) clean
	$(MAKE) --restore_cli
	$(MAKE) --restore_gui

--build_cli:
	$(MAKE) --restore_cli
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CLI_PATH)$(CLI_PROJECT)

--build_gui:
	$(MAKE) --restore_gui
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(GUI_PATH)$(GUI_PROJECT)

build:
	$(MAKE) --build_cli
	$(MAKE) --build_gui

--publish_cli:
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(CLI_PATH)$(CLI_PROJECT)

--publish_gui:
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(GUI_PATH)$(GUI_PROJECT)

publish:
	$(MAKE) build
	$(MAKE) --publish_cli
	$(MAKE) --publish_gui

cli:
	$(MAKE) --build_cli
	dotnet run $(CLI_PATH) -h

gui:
	$(MAKE) --build_gui
	dotnet run $(GUI_PATH)
