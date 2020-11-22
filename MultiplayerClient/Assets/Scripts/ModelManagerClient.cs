using System;
using Models;

public class ModelManagerClient : IModelManagerClient
{
    public event EventHandler Loaded;
    private int? _controllablePlayerExemplarId;
    public int ControllablePlayerExemplarId => (int)_controllablePlayerExemplarId;
    public IModelManager ModelManager { get; }

    public ModelManagerClient(IWorldData worldData)
    {
        ModelManager = new ModelManager(worldData);
        AddPlayerModelsListener();
    }

    private void AddPlayerModelsListener()
    {
        ModelManager.PlayersModel.ExemplarModelDic.Added += OnPlayerModelsUpdated;
    }
    
    private void RemovePlayerModelsListener()
    {
        ModelManager.PlayersModel.ExemplarModelDic.Added -= OnPlayerModelsUpdated;
    }

    private void OnPlayerModelsUpdated(int arg1, IPlayerModel arg2)
    {
        CheckControllablePlayerLoaded();
    }

    public void SetControllablePlayer(int id)
    {
        _controllablePlayerExemplarId = id;
        CheckControllablePlayerLoaded();
    }

    private void CheckControllablePlayerLoaded()
    {
        bool isPlayerSet = _controllablePlayerExemplarId != null;
        bool isPlayerDataLoaded = isPlayerSet && ModelManager.PlayersModel.ExemplarModelDic.ContainsKey(ControllablePlayerExemplarId);
        
        if (isPlayerDataLoaded)
        {
            OnLoaded();
            RemovePlayerModelsListener();
        }
    }

    private void OnLoaded()
    {
        Loaded?.Invoke(this, EventArgs.Empty);
    }
}