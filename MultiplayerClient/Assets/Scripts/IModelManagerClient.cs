using System;
using Models;

public interface IModelManagerClient
{
    event EventHandler ControllablePlayerSet;
    int ControllablePlayerExemplarId { get; }
    IModelManager ModelManager { get; }

    void SetControllablePlayer(int id);
}