using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;

class Program
{
    const string EXIT = "exit";
    const string ECHO = "echo";
    const string TYPE = "type"; 

    static readonly List<string> validCommands = [EXIT, ECHO, TYPE];

    static void Main(string[] args)
    {
        while (true)
        {
            Console.Write("$ ");
            var inputText = Console.ReadLine();
            if (string.IsNullOrEmpty(inputText) || string.IsNullOrWhiteSpace(inputText))
                continue;

            var inputSlice = inputText?.Split(' ', 2);
            var command = inputSlice?.Length > 0 ? inputSlice?[0] : " ";
            var commandParams = inputSlice?.Length == 2 ? inputSlice?[1] : " ";  

            ProcessCommand(command, commandParams);       
        }
    }

    static void ProcessCommand(string command, string commandParams)
    {
        switch (command)
        {
            case ECHO:
                Console.WriteLine(commandParams);
                break;
            case TYPE:
                HandleTypeCommand(commandParams);
                break;
            case EXIT:
                HandleExitCommand(commandParams);
                break;
            default: 
                Console.WriteLine($"{command}: command not found");
                break;
        }
    }

    static void HandleExitCommand(string commandParams)
    {
        if (commandParams == "0")
        {
            Environment.Exit(0);
        }
        else
        {
            HandleInvalidCommandParams();
        }
    }

    static void HandleInvalidCommandParams()
    {
        Console.WriteLine("Command parameter is not correct");
    }

    static void HandleTypeCommand(string commandParams)
    {
        if (validCommands.Exists(command => command == commandParams))
        {
            var pathENVVaribale = Environment.GetEnvironmentVariable("PATH");
            
            if(pathENVVaribale != null) {
                foreach(var path in pathENVVaribale.Split(':')){
                    if(path.Contains(commandParams)) {
                        Console.WriteLine($"{commandParams} is {path}");
                        break;
                    }
                }
                
                Console.WriteLine($"invalid_command: not found");
            }
        }
        else
        {
            Console.WriteLine($"{commandParams}: not found");
        }
    }
}
