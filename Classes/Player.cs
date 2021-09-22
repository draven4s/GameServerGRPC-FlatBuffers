using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameServerGRPC
{
    [BsonIgnoreExtraElements]
    public class Player
    {
        public object _id { get; set; }
        public string Name { get; set; }
        public Vector3 Coords { get; set; }

    }
}
