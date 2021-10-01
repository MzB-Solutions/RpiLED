CLI_PROJECT = RpiLed.Cli.csproj
CLI_PATH = ./RpiLED.Cli/
GUI_PROJECT = RpiLed.Gui.csproj
GUI_PATH = ./RpiLED.Gui/
SOLUTION = RpiLED.sln
VERBOSITY = n
CONTAINMENT = "--self-contained"
CONFIGURATION = Release
LOGO = --nologo
OUTPUT_DIR = ./output/
## {q[uiet],m[inimal],n[ormal],d[etailed],diag[nostic]}

all : clean restore build publish

FORCE:

clean:
	dotnet clean -v $(VERBOSITY) $(LOGO) ./$(SOLUTION)
	dotnet clean -v $(VERBOSITY) $(LOGO) $(CLI_PATH)$(CLI_PROJECT)
	dotnet clean -v $(VERBOSITY) $(LOGO) $(GUI_PATH)$(GUI_PROJECT)

restore:
	$(MAKE) FORCE
	$(MAKE) clean
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(CLI_PATH)$(CLI_PROJECT)
	dotnet restore -v $(VERBOSITY) --force --force-evaluate $(GUI_PATH)$(GUi_PROJECT)

build:
	$(MAKE) restore
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CLI_PATH)$(CLI_PROJECT)
	dotnet build --no-restore $(LOGO) -c $(CONFIGURATION) -r linux-arm $(GUI_PATH)$(GUI_PROJECT)

publish:
	$(MAKE) build
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(CLI_PATH)$(CLI_PROJECT)
	dotnet publish --no-build $(LOGO) -c $(CONFIGURATION) -r linux-arm $(CONTAINMENT) -o $(OUTPUT_DIR) $(GUI_PATH)$(GUI_PROJECT)
#	warp-packer --arch linux-arm --input_dir $(CLI_PATH)bin/Release/net5.0/linux-arm/publish --exec rpiled.cli --output RpiLED.CLI
#	warp-packer --arch linux-arm --input_dir $(GUI_PATH)bin/Release/net5.0/linux-arm/publish --exec rpiled.gui --output RpiLED.GUI

run-cli:
	dotnet run $(CLI_PATH) -h

run-gui:
	dotnet run $(GUI_PATH)


dotnet restore -v "$VERBOSITY" --force --force-evaluate "${PROJECT_FILE}"
restoreState=$?
dotnet build -v "$VERBOSITY" --no-restore $LOGO -c $CONFIG "${PROJECT_FILE}"
buildState=$?
dotnet publish -v "$VERBOSITY" "$CONTAINMENT" --no-build -c $CONFIG $LOGO -o "${OUTPUT_DIR}" "${PROJECT_FILE}"
publishState=$?