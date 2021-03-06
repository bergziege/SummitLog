[![Join the chat at https://gitter.im/bergziege/SummitLog](https://badges.gitter.im/bergziege/SummitLog.svg)](https://gitter.im/bergziege/SummitLog?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)
[![Build status](https://ci.appveyor.com/api/projects/status/v69dhslduqxy8g4j?svg=true)](https://ci.appveyor.com/project/bergziege/summitlog)
[![Documentation Status](https://readthedocs.org/projects/summitlogdoc/badge/?version=latest)](http://summitlogdoc.readthedocs.org/de/latest/?badge=latest)

# SummitLog #

SummitLog ist ein Hobbyprojekt zur Erfassung von Wander und Kletterrouten sowie deren Begehungen.

## Features ##

- Gruppierungen: Länder, Gebiete, Gipfelgruppen, Gipfel
- Wege auf jede der Gruppierungen
- Varianten von Wegen mit unterschiedlichen Schwierigkeitsstufen
- Einträge für Begehungen der Varianten
- Verwaltung der Schwierigkeitsstufen unterschiedlicher Skalen mit Punktesystem zum Skalenbübergreifenden Vergleich

## Benötigte Infrastruktur ##

- Neo4j auf localhost:7474 mit Nutzer "neo4j" und Passwort "extra"
- .net 4.5

## Geplante Features nach Basisfunktionen ##

- Ändern der DB Einstellungen zur Laufzeit
- Suche
- Reporting
- Alle aktuell noch einzelnen Fenster in Hauptfenster aufnehmen.
