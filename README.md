# TokidosDemo

## Overview
TokidosDemo is a Unity project demonstrating interactive main and sub cubes with flashing and rotation features. The project uses C# scripts to manage cube behaviors, including color flashing, rotation, and editor tools for snapping cubes.

## Features
- **MainCubeFlash**: Controls the main cube's color flashing and rotation.
- **SubCubeFlash**: Allows sub cubes to trigger main cube flashing in its color.
- **CubeSnapper (Editor Tool)**: Snap and unsnap cubes in the scene, set sub cube colors, and enable/disable mouse interactions.

## Usage
1. **Main Cube**: Right-click to toggle rotation and flash sub cubes. Left-click to flash the main cube.
2. **Sub Cubes**: Left-click to flash the main cube to the sub cube's color.
3. **CubeSnapper Tool**: Access via `Tools > CubeSnapper` in the Unity Editor to snap/unsnap cubes and manage colors/interactions.

## Setup
- Assign `MainCubeFlash` and `SubCubeFlash` scripts to the respective GameObjects.
- Main cube takes 4 sub cube flashes objects.

## Requirements
- Unity (recommended version: 2020.3 or newer)
- .NET Framework 4.7.1

## Logic to Flashing
 - Enforced there is only one color can flash at a time on the main cube
 - Added the Grid on main, but didn't implement the flashing logic because it's a different way to flash 
