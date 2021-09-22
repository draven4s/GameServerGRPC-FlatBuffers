using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameServerGRPC;
using MongoDB.Driver;
using MongoDB.Bson;
using FlatSharp;
using MongoDB.Bson.Serialization;

namespace GameServerGRPC
{
    public class PlayerList: Program
    {
        List<Player> testList = new List<Player>();

        
        public void AddPlayer(Player playa)
        {
            var database = dbClient.GetDatabase("Players");
            var collection = database.GetCollection<Player>("Players");
            collection.InsertOne(playa);
            testList.Add(playa);
        }
        public List<Player> GetPlayers()
        {
            var database = dbClient.GetDatabase("Players");
            var collection = database.GetCollection<Player>("Players");
            var data = collection.AsQueryable().ToList();
            return data;
            //var temp = "";
        }
        public void UpdatePlayer(string name, Vector3 IQ)
        {
            var database = dbClient.GetDatabase("Players");
            var collection = database.GetCollection<BsonDocument>("Players");
            collection.FindOneAndUpdate(Builders<BsonDocument>.Filter.Eq("Name", name), Builders<BsonDocument>.Update.Set("Coords", IQ));
            
        }
    }
}
