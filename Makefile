.PHONY: publish

# Variables
PUBLISH_DIR = publish
ZIP_FILE = publish.zip
CONFIG_FILE = $(PUBLISH_DIR)\\web.config

# Publish the .NET project, remove existing zip if it exists, and create a new zip
publish:
	dotnet publish -c Release -o ./$(PUBLISH_DIR) -p:EnvironmentName=Production
	del /f /q $(CONFIG_FILE)
	powershell Compress-Archive -Path $(PUBLISH_DIR)\* -DestinationPath $(ZIP_FILE) -Force
