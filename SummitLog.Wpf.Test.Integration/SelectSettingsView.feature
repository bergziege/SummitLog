Feature: SelectSettingsView
	Um Stammdaten zu pflegen
	Als ein Nutzer
	Muss ich die Stammdatenansicht aufrufen

@mytag
Scenario: Stammdatenansicht aufrufen
	Given Ich befinde mich im Hauptfenster
	When Ich auf Einstellungen klicke
	Then muss die Stammdatenansicht geöffnet werden
