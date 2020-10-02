using Client;
using Descriptions;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private protected ViewContainer _viewContainer;
    [SerializeField] private protected Database _database;
    private readonly IClient _client = new CustomHttpClient();
    private ModelManager _modelManager;
    private PresenterManager _presenterManager;
    
    private void Awake()
    {
        _modelManager = new ModelManager(_client, _database);
        _presenterManager = new PresenterManager(_viewContainer,_modelManager, _database);
        
        _presenterManager.Activate();
    }

    private void OnDestroy()
    {
        _presenterManager.Deactivate();
    }
}
