using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;

List<string> validCommands = new List<string>();
var inputCommand = string.Empty;
while(true)
{
    Console.Write("$ ");
    inputCommand = Console.ReadLine();

    if(inputCommand == "exit 0"){        
        return;
    }

    if(string.IsNullOrEmpty(inputCommand) || string.IsNullOrEmpty(inputCommand) || !validCommands.Contains(inputCommand)){
        Console.WriteLine($"{inputCommand}: command not found");
    }
    
}
