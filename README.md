# Player Manager Console App 🧑‍💻

A simple C# console application for managing a list of players with basic CRUD-like operations and local file persistence using a `.yml` file.

---

## 🚀 Features

- Create new players with name and gender
- Delete players by ID
- Display list of all registered players
- Save players to a local `players.yml` file
- Loadless in-memory player management (session-based)
- Random "fun feature" — find out which player has cheese 🧀
- Simple console-based command system

---

## 🧑‍💻 Commands

| Command | Description |
|--------|-------------|
| `/createPlayer <name> <gender>` | Creates a new player |
| `/deletePlayer <id>` | Deletes a player by ID |
| `/savePlayers` | Saves all players to `players.yml` |
| `/playersList` | Displays all registered players |
| `/whoseCheese` | Randomly selects a player with "cheese" |
| `/clearChat` | Clears console |
| `/version` | Shows app version |

---

## 💾 Data Storage

Players are stored in a simple semicolon-separated format:

```

Id;Name;Gender

```

Example:
```

1;John;male
2;Anna;female

```

Stored in:
```

players.yml

```

> ⚠️ Note: This is a custom format, not strict YAML.

---

## ⚙️ How It Works

- Players are stored in a static `SortedList<int, Player>`
- Each new player gets an auto-incremented ID
- Data is persisted manually using file write operations
- Deletion is handled via temporary file rewriting
- Console loop keeps the application running until exit

---

## 🧠 Design Notes

- Encapsulated `Player` model with properties
- Static `PlayersManager` handles storage logic
- Simple command dispatcher using `switch`
- Minimal dependencies (pure .NET console app)

---

## 🛠️ Requirements

- .NET 6+ (recommended)
- Any C# compatible IDE (Visual Studio / Rider / VS Code)

---

## 🎯 Goal of Project

This project is designed as a learning exercise for:

- OOP in C#
- File I/O operations
- Console-based user interaction
- Basic data management systems

---

## 📄 License

MIT © RDProject
