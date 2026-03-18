-- SQL Server Initialization Script - TaskManager
-- À exécuter lors du démarrage du conteneur

WAITFOR DELAY '00:00:05';

-- Créer la database TaskManager
CREATE DATABASE TaskManagerDB;

-- Utiliser la database
USE TaskManagerDB;

-- Les migrations EF Core s'occupent de la structure des tables

-- Afficher un message de confirmation
PRINT 'TaskManagerDB created successfully';
