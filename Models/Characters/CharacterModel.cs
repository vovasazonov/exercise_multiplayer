using System;
using System.Collections.Generic;
using Models.Weapons;

namespace Models.Characters
{
    public class CharacterModel : ICharacterModel
    {
        public event EventHandler<AttackEventArgs> EnemyAttacked;
        public event EventHandler<WeaponChangedEventArgs> HoldWeaponChanged;

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

            _data.HoldWeaponUpdated += OnDataHoldWeaponChanged;
        }

        public void Attack(ICharacterModel enemy)
        {
            enemy.HealthPoint.TakePoints(HoldWeapon.Damage);

            var eventArgs = new AttackEventArgs(enemy);
            CallEnemyAttacked(eventArgs);
        }

        public void ChangeHoldWeapon(int weaponExemplarId)
        {
            var weaponExemplar = _weaponModels[weaponExemplarId];
            if (HoldWeapon != weaponExemplar)
            {
                HoldWeapon = weaponExemplar;
                _data.HoldWeaponExemplarId = weaponExemplarId;

                var weaponChangedEventArgs = new WeaponChangedEventArgs(weaponExemplarId);
                CallHoldWeaponChanged(weaponChangedEventArgs);
            }
        }

        private void CallEnemyAttacked(AttackEventArgs e)
        {
            EnemyAttacked?.Invoke(this, e);
        }

        private void OnDataHoldWeaponChanged(object sender, EventArgs e)
        {
            ChangeHoldWeapon(_data.HoldWeaponExemplarId);
        }

        private void CallHoldWeaponChanged(WeaponChangedEventArgs e)
        {
            HoldWeaponChanged?.Invoke(this, e);
        }
    }
}