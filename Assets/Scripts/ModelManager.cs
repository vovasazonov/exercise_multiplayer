using System.Collections.Generic;
using Client;
using Descriptions;
using Enemies;
using Weapons.Models;

public class ModelManager
{
    private readonly IClient _client;
    private readonly Database _database;
    public IEnemyModel EnemyModel { get; }
    private Dictionary<string, IWeaponModel> _weaponModels;
    public IReadOnlyDictionary<string, IWeaponModel> WeaponModels => _weaponModels;

    public ModelManager(IClient client, Database database)
    {
        _client = client;
        _database = database;
        
        EnemyModel = new EnemyModel(_client);
        _weaponModels = new Dictionary<string, IWeaponModel>(database.DescriptionsWeapon.Descriptions.Count);
        foreach (var description in database.DescriptionsWeapon.Descriptions)
        {
            var weaponModel = new WeaponModel(description);
            _weaponModels.Add(weaponModel.Id, weaponModel);
        }
    }
}