using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

List<string> validCommands = ["echo"];
while(true)
{
    Console.Write("$ ");
    var inputText = Console.ReadLine();
    if(inputText == "exit 0"){        
        return;
    }
    var inputSlice = inputText?.Split(" ", 2);
    var command = inputSlice?.Length > 0 ? inputSlice?[0] : " ";
    var commandParams = inputSlice?.Length == 2 ? inputSlice?[1] : " "; 

  

    if(!validCommands.Contains(command)){
        Console.WriteLine($"{command}: command not found");
    }

    ProcessCommand(command, commandParams);    
}

void ProcessCommand(string command, string commandParams)
{
    switch(command){
        case "echo" :
            Console.WriteLine(commandParams);
        break;
        default : break;
    }
}