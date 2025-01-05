using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.InteropServices;
using System.Diagnostics;

class Program
{
    const string EXIT = "exit";
    const string ECHO = "echo";
    const string TYPE = "type";
    const string PWD = "pwd"; 

    static readonly List<string> shellBuiltinCommands = [EXIT, ECHO, TYPE, PWD];

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
            case PWD:
                HandlePWDCommand(commandParams);
                break;    
            default: 
                RunProgram(command, commandParams);
                break;
        }
    }

    private static void HandlePWDCommand(string commandParams)
    {
        Console.WriteLine(Directory.GetCurrentDirectory());
    }

    private static void RunProgram(string command, string commandParams)
    {
        var pathENVVaribale = Environment.GetEnvironmentVariable("PATH");

        if(pathENVVaribale != null) {                
            foreach(var path in pathENVVaribale.Split(':'))
            {
                if(File.Exists($"{path}/{command}")) {
                    Process.Start(new ProcessStartInfo($"{path}/{command}", commandParams));
                    return;
                }
            }

            Console.WriteLine($"{command}: not found");
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
        if(shellBuiltinCommands.Exists(command => command == commandParams))
        {
            Console.WriteLine($"{commandParams} is a shell builtin");
            return;
        }

        var pathENVVaribale = Environment.GetEnvironmentVariable("PATH");
        
        if(pathENVVaribale != null) {                
            foreach(var path in pathENVVaribale.Split(':'))
            {
                if(File.Exists($"{path}/{commandParams}")) {
                    Console.WriteLine($"{commandParams} is {path}/{commandParams}");
                    return;
                }
            }
            
            Console.WriteLine($"{commandParams}: not found");
        }
    }

}
