# Summer project to create a client-server communication for a video game, that is made with Unity3d, while using as little bandwidth as possible. 
The Gameserver is Authoritative, meaning that it is much harder for players to cheat, but it comes at a cost of some latency. Implementing [Flatbuffers](https://google.github.io/flatbuffers/) serialization into the gameserver and the game itself, means that the data can be accessed without parsing it, making it memory efficient.
  
## What it can do already:
  1. It can sync player positions. In the game it just sets the location of the player prefab.
  2. It can send and receive to a client. Also can save data to a MongoDB database.

## What needs to be done:
  1. A better method to synchronising the client states between clients.
