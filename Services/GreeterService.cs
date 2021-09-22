using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlatSharp;
using FlatBuffers;
using MongoDB.Bson;
using System.Dynamic;
using MongoDB.Driver;
namespace GameServerGRPC
{

    public class GreeterService: Greeter.GreeterServerBase
    {
        private List<Player> loggedIn = new List<Player>();
        bool listupdated = false;
        PlayerList plList = new PlayerList();
         

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            loggedIn = plList.GetPlayers();
            if (loggedIn.Any(x => x.Name.Contains(request.name)))
            {
                HelloReply testRep = new HelloReply();
                
                return Task.FromResult(new HelloReply
                {
                   message = request.name,
                   success = false
                });
            }
            else
            {
                Player temp = new Player {_id = request.name,  Name = request.name };
                plList.AddPlayer(temp);
                return Task.FromResult(new HelloReply
                {
                    message = "Success " +request.name,
                    success = true
                });
            }

        }
        public override Task<MovementDataReply> Move(MovementData request, ServerCallContext context)
        {
            loggedIn = plList.GetPlayers();
            if (loggedIn.Any(x => x.Name.Contains(request.name)))
            {
                listupdated = true;
                plList.UpdatePlayer(request.name, request.coords);
                return Task.FromResult(new MovementDataReply
                {

                });
            }
            else {
                listupdated = false;
                return Task.FromResult(new MovementDataReply
                {

                });
            }
        }

        public override async Task SyncMovement(MovementData request, IServerStreamWriter<MovementDataReply> responseStream, ServerCallContext context)
        {
            // Keep the stream open so we can continue writing new Messages as they are pushed
            
            var myName = request.name;
            loggedIn = plList.GetPlayers();
            while (loggedIn.Any(x => x.Name == myName))
            {
                loggedIn = plList.GetPlayers();
                foreach (var player in loggedIn)
                {
                    if (player.Name == myName)
                    {
                        // don't send data about myself to myself
                    }
                    else
                    {
                        MovementDataReply testas = new MovementDataReply
                        {
                            name = player.Name,
                            coords = player.Coords
                        };
                        await responseStream.WriteAsync(testas);
                    }
                }
                
            }

        }

    }
}
