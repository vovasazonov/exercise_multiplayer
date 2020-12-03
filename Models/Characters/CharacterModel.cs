using System.Collections.Generic;
using Models.Weapons;

namespace Models.Characters
{
    public class CharacterModel : ICharacterModel
    {
        public event EnemyAttackedHandler EnemyAttacked;
        public event HoldWeaponChangedHandler HoldWeaponChanged;

        private readonly ICharacterData _data;
        readonly ITrackableDictionary<int, IWeaponModel> _weaponModels;

        public string Id => _data.Id;
        public IHealthPointModel HealthPoint { get; }
        public IWeaponModel HoldWeapon { get; private set; }

        public CharacterModel(ICharacterData data, ITrackableDictionary<int, IWeaponModel> weaponModels)
        {
            _data = data;
            _weaponModels = weaponModels;

            HealthPoint = new HealthPointModel(_data.HealthPointData);

            _data.HoldWeaponUpdated += OnHoldWeaponChanged;
        }

        public void Attack(ICharacterModel enemy)
        {
            enemy.HealthPoint.TakePoints(HoldWeapon.Damage);

            CallEnemyAttacked(enemy);
        }

        public void ChangeHoldWeapon(int weaponExemplarId)
        {
            var weaponExemplar = _weaponModels[weaponExemplarId];
            if (HoldWeapon != weaponExemplar)
            {
                HoldWeapon = weaponExemplar;
                _data.HoldWeaponExemplarId = weaponExemplarId;

                CallHoldWeaponChanged(weaponExemplarId);
            }
        }

        private void CallEnemyAttacked(ICharacterModel enemy)
        {
            EnemyAttacked?.Invoke(enemy);
        }

        private void OnHoldWeaponChanged()
        {
            ChangeHoldWeapon(_data.HoldWeaponExemplarId);
        }

        private void CallHoldWeaponChanged(int weaponExemplarId)
        {
            HoldWeaponChanged?.Invoke(weaponExemplarId);
        }
    }
}