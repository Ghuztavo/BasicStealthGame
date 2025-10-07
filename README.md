# 🕵️ Basic Stealth Game

A simple **stealth-based game** made with **Unity (version 6000.0.32f1)** using the **NavMesh** package and a few **free assets** from the Unity Asset Store.

This project was created to **practice AI behavior and pathfinding** in a stealth environment, where the player must avoid detection while navigating through enemy patrols.

---

## 🎯 Objective
Reach the **safe zone** without being **caught by enemy monsters** roaming around the map.

---

## 🎮 Controls

| Action  | Key | Description |
|----------|-----|-------------|
| Move     | **W / A / S / D** | Move your character |
| Jump     | **Space** | Jump over obstacles |
| Crouch   | **Ctrl** | Move quietly and make less noise |
| Run      | **Shift** | Move faster, but make more noise |

> ⚠️ Noise levels matter — running or moving quickly can alert nearby enemies.

---

## 👾 Enemies

Each monster type behaves differently and uses unique senses to detect the player:

- **Monster 1** – Always knows the player’s position but moves very slowly.  
- **Monster 2** – The fastest monster, but only follows a fixed patrol route.  
- **Monster 3** – Patrols a set of waypoints; if it **sees the player**, it begins to chase.  
- **Monster 4** – Patrols a set of waypoints but is **blind**. It detects the player through **sound** or **close proximity**.  
  - The player makes noise while moving, and even more while running.

---

## 🧠 Features

- AI pathfinding using **Unity’s NavMesh system**
- Stealth mechanics based on **visibility and sound detection**
- Multiple enemy behavior types with unique traits
- Simple level design made with **free Unity Asset Store** resources

---

## ⚙️ Built With

- [Unity 6000.0.32f1](https://unity.com/)
- [Unity NavMesh Components](https://docs.unity3d.com/Manual/nav-BuildingNavMesh.html)
- Free a
