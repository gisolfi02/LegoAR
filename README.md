# 📌 Lego AR
<p align="center">
  <img src="https://github.com/gisolfi02/LegoAR/blob/master/Assets/Images/Logo.png" style="width:300px">
</p>

**LegoAR** is an application developed in **Unity** with the support of **AR Foundation**, designed to bring the LEGO brick experience into augmented reality (AR). The app allows users to place digital LEGO models in the real world, interact with them, and visualize them in a three-dimensional space using compatible devices.

This experience combines the creativity of the LEGO world with the potential of AR technology, offering an innovative way to build and visualize LEGO structures without the need for physical pieces.

---

## 🛠 Technologies Used
- **Unity** (`2022.3.51f1 LTS`)
- **AR Foundation** (`5.1.5`)
- **ARCore XR Plugin** (`5.1.5`)
- **XR Interaction Toolkit** (`3.0.4`)
- **URP (Universal Render Pipeline)** (`14.0.11`)

---

## 💻 System Requirements
- **Supported Mobile Devices**:
  - Android (`Android 8.0+`, with support for **ARCore** and **ARCore Depth API**)

---

## 🚀 Project Setup
### 1️⃣ Clone the Repository
```bash
git clone https://github.com/gisolfi02/LegoAR.git
```
### 2️⃣ Open the Project in Unity
1. Open Unity Hub
2. Select **Open Project**
3. Navigate to the project folder and open it

### 3️⃣ Install Required Packages
- **AR Foundation**
- **ARCore XR Plugin**
- **XR Interaction Toolkit**
- **URP (Universal Render Pipeline)**

### 4️⃣ Configure Player Settings
- Go to **Edit > Project Settings > XR Plug-in Management**
- Enable **ARCore** for Android

---

## 📂 Folder Structure
```
📁 Assets/
  📁 Anatra FBX/           # 3D models in FBX format related to the duck
  📁 Anatra Info/          # Information about the duck's steps
  📁 Animations/           # Animations used in the project
  📁 Bricks/               # LEGO brick-related models and assets
  📁 Cavalluccio FBX/      # 3D models in FBX format related to the seahorse
  📁 Cavalluccio Info/     # Information about the seahorse's steps
  📁 Images/               # Images used for the UI
  📁 Material/             # Materials and shaders used in the project
  📁 ProjectFiles/         # Additional project files
  📁 Resources/            # General resources accessible at runtime
  📁 Scenes/               # Unity project scenes
  📁 Script/               # C# scripts for application logic
  📁 Shadow/               # Resources related to shadows or visual effects
  📁 Unicorno FBX/         # 3D models in FBX format related to the unicorn
  📁 Unicorno Info/        # Information about the unicorn's steps
  📁 Violet Theme UI/      # Resources for the UI theme
📁 Packages/             # Unity packages
📁 ProjectSettings/      # Project configurations
```

---

## 🔹 Main Features
- 🌐 **Surface Detection and Tracking:** The app uses *AR Foundation* technology to detect flat surfaces where users can visualize the model construction process;
- 🧩 **Interactive Guide:** For each step, various pieces are displayed in AR, virtually positioning them correctly;
- ➡️ **Step Navigation:** Users can navigate through the steps using buttons to view previous or next steps;
- 📱 **Intuitive User Interface:** The interface includes a panel showing step descriptions, indicating the number and type of required pieces. Additionally, a progress bar monitors the assembly progress.

---

## 📦 Build and Deploy
### 📱 Android
1. **Enable Developer Mode** on your device
2. **Connect the device via USB**
3. **In Unity**, go to **File > Build Settings**
4. Select **Android**
5. Click **Build & Run**

## ⚠️ Compatibility
The app currently supports **only** the LEGO® **31140** [*Magical Unicorn*](https://www.lego.com/it-it/product/magical-unicorn-31140) set. Ensure you have this set before using the app.

---

> **Disclaimer:**  
> This project is not affiliated, sponsored, or officially approved by LEGO®.  
> LEGO® is a registered trademark owned by the LEGO Group.  
> **LegoAR** is an independent project created to enhance the assembly experience of LEGO® sets.  
> All trademarks and names are the property of their respective owners.

---
