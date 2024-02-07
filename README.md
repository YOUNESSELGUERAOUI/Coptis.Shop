# Coptis.Shop

Une application de commerce électronique simplifiée réalisée avec Blazor Server sur .Net7, en suivant les principes de la clean l'architecture basique.
L'infrastructure repose sur le modèle repository et utilise Entity Framework Core avec une stratégie Code First, signifiant que la base de données est générée automatiquement au démarrage si elle n'existe pas déjà.

L'application offre trois fonctionnalités clés : une page de connexion pour l'authentification, empêchant les visiteurs non identifiés d'effectuer des actions avant de se connecter; une page « Catalogue des produits » présentant une liste de produits avec l'option de consulter les détails d'un produit et de procéder à son achat; et une troisième page répertoriant les achats réalisés par tous les utilisateurs de l'application, accessible à tout utilisateur connecté dans un souci de simplification.

# Tester l'application

En exécutant l'application localement, la base de données sera automatiquement créée et peuplée avec un ensemble de données de test, incluant une liste de produits et des utilisateurs. Il est possible de personnaliser la chaîne de connexion en modifiant le fichier appsettings.json.

Les deux utilisateurs de tests:
yelgueraoui@coptis.com/Coptis@2024
mahmoud.alabsi@coptis.com/Coptis@2025
