# 🚗 Groceries-in-Car — Unity ML-Agents

[![Unity](https://img.shields.io/badge/Unity-2023-000000.svg?logo=unity)](https://unity.com/)
[![ML-Agents](https://img.shields.io/badge/ML--Agents-Toolkit-blueviolet)](https://github.com/Unity-Technologies/ml-agents)
[![License: MIT](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

> A reinforcement-learning agent trained in Unity ML-Agents to **load a car trunk with grocery blocks for two destinations** in the most efficient way.

<p align="center">
  <img src="https://raw.githubusercontent.com/IsabelleDesle/GroceriesInCar_Unity_MlAgent/main/loading03.gif" width="260" />
  <img src="https://raw.githubusercontent.com/IsabelleDesle/GroceriesInCar_Unity_MlAgent/main/loading01.gif" width="260" />
  <img src="https://raw.githubusercontent.com/IsabelleDesle/GroceriesInCar_Unity_MlAgent/main/loading02.gif" width="260" />
</p>

---

## 📖 Overview

The agent learns to pack 1–4 grocery blocks into a car trunk that is split into **two destination zones** (blue and gray). Each block must end up in the zone matching its color. The number of groceries can be adjusted from the Agent inspector via a slider.

A supermarket environment surrounds the car for visual context, and a free-fly camera lets you observe the agent at work.

---

## ✨ Highlights

- 🎯 **Reinforcement learning** with Unity ML-Agents Toolkit
- 🎚️ **Configurable difficulty** — 1 to 4 groceries per episode
- 🎨 **Color-matched destinations** — blue blocks → blue zone, gray → gray
- 🛒 **Realistic supermarket scene** built with free Asset Store packages
- 📹 **Demo videos & GIFs** showing the trained agent in action

---

## 🚀 Getting Started

### Prerequisites

- **Unity 2023** (LTS recommended)
- A local installation of [Unity ML-Agents](https://github.com/Unity-Technologies/ml-agents)

### Run

1. Clone this repo and open the project in Unity 2023.
2. Open **`Scene2`**.
3. Press Play, or train/inference using the ML-Agents CLI as usual.

---

## 🎨 Credits

Source frameworks and assets used:

- [Unity ML-Agents Toolkit](https://github.com/Unity-Technologies/ml-agents)
- [SimplePoly City – Low Poly Assets](https://assetstore.unity.com/packages/3d/environments/simplepoly-city-low-poly-assets-58899)
- [Free Fly Camera](https://assetstore.unity.com/packages/tools/camera/free-fly-camera-140739)

Only free Asset Store assets were used in this project.

---

## 👤 Author

**Isabelle Deslé** — Bachelor AI & Creative Technologies @ Howest
[GitHub](https://github.com/IsabelleDesle)

---

## 📄 License

This project is licensed under the MIT License — see the [LICENSE](LICENSE) file for details.
