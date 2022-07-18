
using StateMachineCore;

Console.WriteLine("Hello, World!");



var machine = new StateMachine<States, StateActions, Data>(States.A, new Data { Count = 1 }) ;

machine.Config(States.A, builder =>
{
    builder
        .OnEntry(async e => { Console.WriteLine("Entry A"); })
        .OnEntry(async e => Console.WriteLine("Exit A"))
        .AddRoute(StateActions.AtB, States.B)
        .AddRoute(StateActions.AtC, States.C, async e => { Console.WriteLine("Excute" + e.Action.ToString()); });

}).Config(States.C, builder =>
{
    builder
        .OnEntry(async e => { Console.WriteLine("Entry C"); })
        .OnExit(async e => Console.WriteLine("Exit C"))
        .AddRoute(StateActions.CtB, States.B);
}).Config(States.B, builder =>
{
builder
    .OnEntry(async e => { Console.WriteLine("Entry B"); Console.WriteLine(e.Context.Count++); })
    .OnExit(async e => { Console.WriteLine("Exit C"); Console.WriteLine(e.Context.Count); })
        .AddRoute(StateActions.BtA, States.A);
});

Console.WriteLine(machine.State.Value);

await machine.ChangeAsync(StateActions.AtC);

Console.WriteLine(machine.State.Value);

await machine.ChangeAsync(StateActions.CtB);

Console.WriteLine(machine.State.Value);

await machine.ChangeAsync(StateActions.BtA);

Console.WriteLine(machine.State.Value);

//error input
var result = await machine.ChangeAsync(StateActions.BtA);
Console.WriteLine(result);
Console.WriteLine(machine.State.Value);

Console.ReadLine();

public class Data
{
    public int Count { get; set; }
}
enum States
{
    A,
    B,
    C,
}

enum StateActions
{
    AtB,
    AtC,
    CtB,
    BtA
}

