# 📌 Lego AR
<p align="center">
  <img src="https://github.com/gisolfi02/LegoAR/blob/master/Assets/Images/Logo.png" style="width:300px">
</p>

---

## 📝 Descrizione
**LegoAR** è un'applicazione sviluppata in **Unity** con il supporto di **AR Foundation**, progettata per portare l'esperienza dei mattoncini LEGO nella realtà aumentata (AR). L'app consente agli utenti di posizionare modelli LEGO digitali nel mondo reale, interagire con essi e visualizzarli in uno spazio tridimensionale utilizzando dispositivi compatibili.

Questa esperienza unisce la creatività del mondo LEGO con le potenzialità della tecnologia AR, offrendo un modo innovativo di costruire e visualizzare strutture LEGO senza la necessità di pezzi fisici.

---

## 🛠 Tecnologie Utilizzate
- **Unity** (`2022.3.51f1 LTS`)
- **AR Foundation** (`5.1.5`)
- **ARCore XR Plugin** (`5.1.5`)
- **XR Interaction Toolkit** (`3.0.4`)
- **URP (Universal Render Pipeline)** (`14.0.11`)

---

## 💻 Requisiti di Sistema
- **Dispositivi Mobile Supportati**:
  - Android (`Android 8.0+`, con supporto **ARCore** e **ARCore Depth API**)


---

## 🚀 Setup del Progetto
### 1️⃣ Clonare il repository
```bash
git clone https://github.com/gisolfi02/LegoAR.git
```
### 2️⃣ Aprire il progetto in Unity
1. Apri Unity Hub
2. Seleziona **Open Project**
3. Naviga alla cartella del progetto e aprilo

### 3️⃣ Installare i pacchetti richiesti
- **AR Foundation**
- **ARCore XR Plugin**
- **XR Interaction Toolkit**
- **URP (Universal Render Pipeline)**

### 4️⃣ Configurare i Player Settings
- Vai su **Edit > Project Settings > XR Plug-in Management**
- Abilita **ARCore** per Android

---

## 📂 Struttura della Cartella
```
📁 Assets/
  📁 Anatra FBX/           # Modelli 3D in formato FBX relativi all'anatra
  📁 Anatra Info/          # Informazioni sui passi dell'anatra
  📁 Animazioni/           # Animazioni utilizzate nel progetto
  📁 Bricks/               # Modelli e risorse relative ai mattoncini LEGO
  📁 Cavalluccio FBX/      # Modelli 3D in formato FBX relativi al cavalluccio
  📁 Cavalluccio Info/     # Informazioni sui passi del cavalluccio
  📁 Images/               # Immagini utilizzate per la UI
  📁 Material/             # Materiali e shader utilizzati nel progetto
  📁 ProjectFiles/         # File di progetto aggiuntivi
  📁 Resources/            # Risorse generali accessibili in fase di runtime
  📁 Scenes/               # Scene di Unity del progetto
  📁 Script/               # Script C# utilizzati per la logica dell'applicazione
  📁 Shadow/               # Risorse relative alle ombre o effetti visivi
  📁 Unicorno FBX/         # Modelli 3D in formato FBX relativi all'unicorno
  📁 Unicorno Info/        # Informazioni sui passi dell'unicorno
  📁 Violet Theme Ui/      # Risorse per il tema grafico dell'interfaccia utente
📁 Packages/             # Pacchetti Unity
📁 ProjectSettings/      # Configurazioni del progetto
```

---

## 🔹 Funzionalità Principali
- 🌐 **Rilevamento e tracciamento della superficie:** L’applicazione utilizza la tecnologia di *AR Foundation* per rilevare le superfici piane su cui sarà possibile visualizzare la costruzione del modello durante tutto il processo di montaggio;
- 🧩 **Guida interattiva:** Per ciascun passaggio, i vari pezzi vengono visualizzati in AR, posizionandoli virutalmente nella posizione corretta  
- ➡️ **Navigazione tra i passaggi:** L’utente può navigare tra i passaggi utilizzando pulsanti, per viualizzare i passi precedenti o successivi;  
- 📱 **Interfaccia utente intuitiva:** L’interfaccia include un pannello che mostra le descrizioni di ogni passaggio, indicando il numero e il tipo di pezzi richiesti. Inoltre prevede una barra di avanzamento per monitorare i progressi del montaggio; 

---

## 📦 Build e Deploy
### 📱 Android
1. **Abilita la modalità Developer** sul dispositivo
2. **Collega il dispositivo via USB**
3. **In Unity**, vai su **File > Build Settings**
4. Seleziona **Android**
5. Clicca su **Build & Run**

## ⚠️ Compatibilità
L'app attualmente supporta **esclusivamente** il set LEGO® **31140** [*Unicorno Magico*](https://www.lego.com/it-it/product/magical-unicorn-31140). Assicurati di avere questo set prima di iniziare ad usarla.

---

> **Disclaimer:**  
> Questo progetto non è affiliato, sponsorizzato o approvato ufficialmente da LEGO®.  
> LEGO® è un marchio registrato di proprietà del Gruppo LEGO.  
> **LegoAR** è un progetto indipendente creato per migliorare l'esperienza di montaggio dei set LEGO®.  
> Tutti i diritti sui marchi e sui nomi sono di proprietà dei rispettivi titolari.

---


