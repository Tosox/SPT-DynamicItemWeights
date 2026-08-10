# Dynamic Item Weights

[![Total Downloads](https://img.shields.io/github/downloads/Tosox/SPT-DynamicItemWeights/total.svg?label=Downloads%20(All%20Time))](https://github.com/Tosox/SPT-DynamicItemWeights/releases) [![Latest Release Downloads](https://img.shields.io/github/downloads/Tosox/SPT-DynamicItemWeights/latest/total.svg?label=Downloads%20(Latest%20Release))](https://github.com/Tosox/SPT-DynamicItemWeights/releases/latest)

## 📜 Description

**Dynamic Item Weights** is a mod for *Single Player Tarkov* that adjusts the weight of items based on their current usage.

## ✨ Features

* Adjusts weight for items dynamically based on their usage
* In-game configuration settings for fine-tuning
* Tare weights fully configurable via `tareweights.jsonc`

## 📁 Installation

* Download the latest release
* Copy the `BepInEx` folder into your SPT folder
* Start the game

## 🛠️ Configuration

### In-game menu

* **Enabled**: Toggle dynamic item weights on/off
* **Default Tare Fraction**: Fallback tare weight as a fraction of the full item weight
* **Verbose Logging**: Log every single weight adjustment (troubleshooting only)

### tareweights.jsonc

A JSONC file mapping the item template ID to the tare weight in kilograms, that is, what the
item still weighs once it is fully used up
```jsonc
{
    "57513f07245977207e26a311": 0.030, // Pack of apple juice
    "5d1b36a186f7742523398433": 2.500  // Metal fuel tank
}
```
> If an item is missing, the plugin uses `Default Tare Fraction` of the original weight

## 📝 Changelog

You can check out the latest changes in [`CHANGELOG.md`](CHANGELOG.md).

## 📷 Preview

<img src="readme-res/fuel_full.png" alt="fuel_full" width="500"/>
<br/>
<img src="readme-res/fuel_used.png" alt="fuel_used" width="500"/>

## 📄 License

Distributed under the MIT License. See `LICENSE` for more information.
