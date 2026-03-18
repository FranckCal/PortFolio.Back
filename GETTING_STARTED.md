# 🚀 Guide d'exécution - Portfolio + TaskManager API

## Vue d'ensemble

Ce guide vous montre comment lancer complètement votre écosystème:
- **Portfolio.API** (Port 5001) - Votre portfolio professionnel
- **Portfolio.UI** (Port 4200) - Interface Angular du portfolio
- **TaskManager API** (Port 7180 HTTPS) - Votre API CyberJet pour gérer les tâches

---

## 📋 Prérequis

✅ .NET 8 SDK installé  
✅ Node.js v18+ installé  
✅ SQL Server LocalDB configuré  
✅ VS Code avec les extensions C# et Angular  

---

## 🎯 Lancement rapide (Recommandé)

### **Depuis VS Code avec les tâches**

1. **Appuyez sur `Ctrl+Shift+D`** (Run and Debug)
2. Sélectionnez les tâches à exécuter:

| Tâche | Port | Commande alternative |
|-------|------|----------------------|
| ▶️ Run API | 5001 | `Ctrl+Shift+B` |
| 🚀 Run Angular UI | 4200 | Dans Terminal: `npm start` |
| ⚙️ Run TaskManager API | 7180 HTTPS | `Ctrl+Shift+D` |

---

## 🔧 Exécution détaillée

### **Option 1: Terminal (Recommandé)**

**Terminal 1 - Portfolio API:**
```powershell
cd e:\FranckCalderaraPortfolio\Portfolio.API
dotnet run
# Swagger disponible: https://localhost:5001/scalar
```

**Terminal 2 - TaskManager API:**
```powershell
cd E:\CyberJet\TaskManagerAPI
dotnet run
# Swagger disponible: https://localhost:7180/swagger/index.html
```

**Terminal 3 - Angular UI:**
```powershell
cd e:\FranckCalderaraPortfolio\Portfolio.UI
npm install  # Première fois seulement
npm start
# App disponible: http://localhost:4200
```

---

### **Option 2: Visual Studio (si vous le préférez)**

**Pour Portfolio:**
1. Ouvrez `FranckCalderaraPortfolio.slnx` dans Visual Studio
2. Définissez `Portfolio.API` comme projet de démarrage
3. Appuyez sur `F5`

**Pour CyberJet TaskManager:**
1. Ouvrez `E:\CyberJet\TaskManagerAPI.sln` dans Visual Studio
2. Clic-droit `TaskManagerAPI` → Set as Startup Project
3. Appuyez sur `F5`

**Pour Angular UI:**
1. Ouvrez le Terminal dans `Portfolio.UI`
2. Exécutez `npm start`

---

## 📍 URLs d'accès

| Service | URL | Notes |
|---------|-----|-------|
| **Portfolio UI** | http://localhost:4200 | Interface principale |
| **Portfolio API** | https://localhost:5001 | API du portfolio |
| **Portfolio Swagger** | https://localhost:5001/scalar | Documentation API |
| **TaskManager API** | https://localhost:7180 | API CyberJet |
| **TaskManager Swagger** | https://localhost:7180/swagger/index.html | Test des endpoints |

---

## 🗄️ Base de données

### **Initialiser les bases**

La première fois qu'une API démarre, elle crée automatiquement la base de données:

```powershell
# Si besoin, force la création:
cd Portfolio.Infrastructure
dotnet ef database update
```

**Bases créées:**
- `PortfolioDB` - Pour Portfolio.API (LocalDB)
- Automatique pour TaskManager (vérifié au démarrage)

---

## ✨ Intégration TaskManager dans le Portfolio

Le portfolio affiche désormais l'API TaskManager dans une **section "APIs & Side Projects"** avec:

✅ **Lien vers Swagger** - https://localhost:7180/swagger/index.html  
✅ **Lien vers l'API** - https://localhost:7180  
✅ **Lien GitHub** - https://github.com/FranckCal/TaskManagerAPI  
✅ **Description détaillée** - Stack tech, statut, etc.  

Accédez après avoir lancé l'Angular UI sur http://localhost:4200

---

## 🐛 Dépannage

### ❌ "Port déjà utilisé"
```powershell
# Trouvez le PID qui utilise le port:
netstat -ano | findstr :7180

# Terminez le processus:
taskkill /PID {PID} /F
```

### ❌ "Connection string error"
Vérifiez que SQL Server/LocalDB est en cours d'exécution:
```powershell
sqllocaldb info mssqllocaldb
```

### ❌ "npm install échoue"
```powershell
cd Portfolio.UI
npm cache clean --force
rm -r node_modules
npm install
```

---

## 📚 Fichiers de configuration

- **Tasks:** `.vscode/tasks.json`
- **APIs Config:** `Portfolio.UI/src/app/core/config/apis.config.ts`
- **Connection Strings:** `Portfolio.API/appsettings.json`

Modifiez directement ces fichiers pour personnaliser les ports/URLs!

---

## 🎉 Succès!

Une fois que tout est lancé:
1. Allez sur http://localhost:4200
2. Faites défiler jusqu'à "📋 APIs & Side Projects"
3. Cliquez sur **"📖 Swagger"** pour tester l'API TaskManager
4. Explorez les endpoints interactifs!

Besoin d'aide? Consultez les READMEs dans chaque dossier 📖
