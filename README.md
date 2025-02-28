# Simulateur de Crédit Immobilier

## Description

Ce projet est un simulateur de crédit immobilier développé en C# (.NET 9). Il permet de calculer les mensualités, les intérêts et l'assurance d'un prêt immobilier selon différents paramètres.

## Fonctionnalités

- Calcul des mensualités de remboursement
- Calcul de la cotisation d'assurance mensuelle
- Calcul du total des intérêts sur la durée du prêt
- Calcul du total de l'assurance
- Calcul du capital remboursé après X années
- Export des résultats en CSV

## Contraintes techniques

- Capital minimum : 50 000€
- Durée du prêt : entre 9 et 25 ans
- Taux nominal et taux d'assurance exprimés en pourcentage

## Structure du projet

- `CreditLib/` : Bibliothèque contenant la logique métier
- `CreditApp/` : Application console
- `CreditTests/` : Tests unitaires

## Technologies utilisées

- .NET 9
- xUnit pour les tests unitaires
- Visual Studio Code / Visual Studio 2022

## Installation et exécution

1. Cloner le repository
2. Ouvrir la solution dans Visual Studio
3. Restaurer les packages NuGet si nécessaire
4. Compiler et exécuter le projet CreditApp

## Tests

Pour exécuter les tests :

```bash
dotnet test
```

## Export CSV

Les résultats sont automatiquement exportés dans un fichier CSV à la racine du projet : `simulation_credit.csv`

---

# Réponses aux questions du TP

## Question 1 : Quel est le prix global de la mensualité ?

1210,31 €

## Question 2 : Quel est le montant de la cotisation mensuelle d'assurance ?

510,42 €

## Question 3 : Quel est le montant total des intérêts remboursés ?

34966,57 €

## Question 4 : Quel est le montant total de l'assurance ?

153125,00 €

## Question 5 : Quel montant du capital a été remboursé au bout de 10 ans ?

61691,62 € (calculé en soustrayant le capital restant au mois 120 (113308,38 €) du capital initial de 175000 €)
