## Features

- **Configurable Business Logic**: Create and manage business configurations using ScriptableObjects.
- **Flexible Data Providers**: Supports player preferences and custom data provider implementations.
- **Upgrade System**: Enhance your businesses with various upgrades.
- **ECS Architecture**: Organized and efficient data management using an entity-component-system design pattern.

## Structure

### Configs

- **BaseDataProviderConfig**: Abstract class for creating data provider configurations.
- **BusinessConfig**: Configuration for business properties such as initial level, income delay, and costs.
- **PlayerPrefProviderConfig**: Player preferences data provider that offers an option to clear stored preferences.
- **UpgradeConfig**: Configuration for defining upgrades, including cost and income multipliers.

### Ecs.Components

- **Balance**: Component to manage player's balance and save/load data.
- **Business**: Represents a business entity with properties and methods to load/save state.
- **Progress**: Component for tracking progress related to business income.
- **Upgrade**: Manages upgrades for businesses, including their purchase and state.

### Ecs.Systems

- **BuySystem**: Handles purchasing logic for businesses and upgrades.
- **CanBuyUpdater**: Updates the UI to reflect whether purchases can be made.
- **IncomeSystem**: Manages income generation from businesses based on progress.
- **UpgradeSystem**: Manages the application of upgrades to businesses.

### Providers

- **BaseProvider**: Abstract class to facilitate data management for components.
- **BusinessProvider**: Responsible for managing business entities using the corresponding configurations.
- **UpgradeProvider**: Similar to `BusinessProvider`, but specifically for upgrades.

### Bootstrap

- **Bootstrap**: Main entry point for initializing game components, including providers and systems. Handles UI and gameplay events.

## Acknowledgments

- Unity Technologies for creating such a powerful game engine.
- Leopotam for the ECS Lite framework.

Happy gaming!
