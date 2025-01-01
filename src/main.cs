using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

List<string> validCommands = new List<string>();

Console.Write("$ ");

var inputCommand = Console.ReadLine();

if(string.IsNullOrEmpty(inputCommand) || string.IsNullOrEmpty(inputCommand) || !validCommands.Contains(inputCommand)){
    Console.Write($"{inputCommand}: command not found");
}

Console.ReadLine();

