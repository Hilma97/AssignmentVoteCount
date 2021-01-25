# Rösträknaren

Kompletteringsuppgift för kursen Programmeringsteknik C#.

## Inlämning

Inlämning av denna uppgift får ej ske via pull request. Lösningens innehåll skall redovisas via skärmdelning på Microsoft Teams och lämnas in via Pingpong till Sven-Erik Jonsson.

## Bakgrund

Ett företag som utvecklar rösträkningsmjukvara i Venezuela har kontaktat dig och vill ha ett program som summerar röster.

## Kravspecifikation

Programmet `VoteCount` behöver kompletteras med logik för att räkna röster i en förformatterad fil.
Mjukvaran tar emot en parameter: Sökväg till fil. Denna är förifylld med korrekt sökväg redan.

- Enbart röster som lades från `2020-11-08T00:00:00Z` till `2020-11-09T00:00:00Z` är giltiga _(notera tidszonen UTC)_.
- Programmet skall summera alla giltiga röster som lades i valet.
- Programmet skall summera alla giltiga röster som lades på partiet `GOP`.
- Programmet skall summera alla giltiga röster som lades på partiet `DNC`.
- Programmet skall summera alla giltiga röster som lades i staten `GA`.