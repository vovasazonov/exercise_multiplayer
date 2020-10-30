using System;
using Models;

public class ModelManagerClient : IModelManagerClient
{
    public event EventHandler ControllablePlayerSet;
    private int? _controllablePlayerExemplarId;
    public int ControllablePlayerExemplarId => (int) _controllablePlayerExemplarId;

    public IModelManager ModelManager { get; } = new ModelManager();

    public void SetControllablePlayer(int id)
    {
        _controllablePlayerExemplarId = id;
        OnControllablePlayerSet();
    }

    private void OnControllablePlayerSet()
    {
        ControllablePlayerSet?.Invoke(this, EventArgs.Empty);
    }
}