using WorldOfEpidemics.Core.Abstractions;
using WorldOfEpidemics.Core.Commands;

namespace WorldOfEpidemics.Core;

public class Registry
{
    ICommand fallback;
    Dictionary<string, ICommand> commands = [];

    public Registry(ICommand fallback)
    {
        this.fallback = fallback;
    }

    public void Register(string name, ICommand command)
    {
        commands.Add(name, command);
    }

    public void Dispatch(string line, IContextModifier contextModifier, IUser user)
    {
        string[] elements = line.Split(" ");
        string command = elements[0];
        string[] parameters = GetParameters(elements);
        (commands.ContainsKey(command) ? GetCommand(command) : fallback).Execute(contextModifier, user, command, parameters);
    }

    public ICommand GetCommand(string commandName)
    {
        return commands[commandName];
    }

    public string[] GetCommandNames()
    {
        return commands.Keys.ToArray();
    }

    #region Helpers
    private string[] GetParameters(string[] input)
    {
        string[] output = new string[input.Length - 1];
        for (int i = 0; i < output.Length; i++)
        {
            output[i] = input[i + 1];
        }
        return output;
    }
    #endregion
}
