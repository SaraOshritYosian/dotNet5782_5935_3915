
Drone Delivery Management System

A C# desktop application for managing a drone-based delivery system. The application provides management of drones, customers, charging stations, and delivery parcels, alongside an automated simulation of drone operations.

Project Overview

The system simulates and manages a complete drone delivery workflow, from parcel creation and drone assignment to package collection, delivery, and battery charging.

It was developed using a multi-layered architecture, separating the Presentation Layer, Business Logic Layer, and Data Access Layer to improve modularity and maintainability.

Key Features

Drone Management

* Add, update, and manage drones.
* Track drone status, location, and battery level.
* Assign delivery parcels based on drone availability, weight capacity, and battery level.
* Calculate distances between drones and charging stations.

Parcel Management

* Create and manage delivery requests.
* Assign parcels to suitable drones.
* Track parcel status throughout the delivery process.
* Manage package pickup and final delivery.

Customer Management

* Register and manage customers.
* Maintain customer information and locations.
* View parcels associated with each customer.

Charging Station Management

* Manage charging stations and available charging slots.
* Track drones currently charging at each station.
* Select nearby charging stations for drones.

Drone Simulation

* Simulate autonomous drone operations.
* Update drone location and battery consumption during delivery.
* Handle parcel assignment, pickup, delivery, and charging processes.
* Support ongoing simulation with periodic UI updates.

Architecture

The application follows a layered architecture:

Layer	Responsibility
Presentation Layer (PL)	WPF desktop interface and user interactions
Business Logic Layer (BL)	Business rules, delivery management, drone operations, and simulation
Data Access Layer (DAL)	Data storage, retrieval, and persistence

Data Access Implementations

The project separates data access through interfaces and provides different implementations:

* DALObject – In-memory data storage.
* DALXML – XML-based data persistence.
* DALFacade – Data access interfaces and factory configuration.

The data access implementation is selected through configuration, allowing the business logic to work independently of the underlying storage implementation.

Technologies

* Language: C#
* Framework: .NET 5
* Desktop UI: Windows Presentation Foundation (WPF), XAML
* Architecture: Multi-layered architecture
* Data Storage: XML, in-memory objects
* Programming Concepts: Object-Oriented Programming (OOP), interfaces, encapsulation, LINQ
* Concurrency: Multithreading, synchronization, locking mechanisms

Getting Started

Prerequisites

* Windows operating system
* Visual Studio
* .NET 5 SDK

Run the Application

1. Clone the repository:

git clone https://github.com/SaraOshritYosian/dotNet5782_5935_3915.git

2. Open the solution file:
    dotNet5782_5935_3915.sln
3. Set the PL project as the startup project.
4. Build and run the application using Visual Studio.

Project Structure

├── PL/              # WPF Presentation Layer
├── BLL/             # Business Logic Layer
├── DALFacade/       # Data Access Interfaces and Factory
├── DALObject/       # In-memory Data Access
├── DALXML/          # XML Data Access
├── ConsoleUI/       # Console User Interface
├── ConsoleUI_BL/    # Console Interface using Business Logic
└── xml/             # XML data files and configuration

Learning Outcomes

This project provided practical experience in:

* Designing and implementing a multi-layered software architecture.
* Applying object-oriented programming principles.
* Separating business logic from data access.
* Implementing a simulation involving state management and battery calculations.
* Working with multithreading and synchronization.
* Building a desktop application using C# and WPF.

⸻

Academic Project | Computer Science | Lev Academic Center

