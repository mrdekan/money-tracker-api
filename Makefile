.PHONY: publish

PUBLISH_DIR = publish

publish:
	dotnet publish -c Release -o ./$(PUBLISH_DIR) -p:EnvironmentName=Production
