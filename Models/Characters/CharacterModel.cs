using System;
using Models.Weapons;

namespace Models.Characters
{
    public class CharacterModel : ICharacterModel
    {
        public event EventHandler<AttackEventArgs> EnemyAttacked;
        public event EventHandler HoldWeaponChanged; 
        
        private readonly ICharacterData _data;

        public string Id => _data.Id;
        public IHealthPointModel HealthPoint { get; }
        public IWeaponModel HoldWeapon { get; private set; }

        public CharacterModel(ICharacterData data)
        {
            _data = data;
            
            HealthPoint = new HealthPointModel(_data.HealthPointData);

            _data.HoldWeaponUpdated += OnHoldWeaponChanged;
        }

        public void Attack(ICharacterModel enemy)
        {
            enemy.HealthPoint.TakePoints(HoldWeapon.Damage);
            
            var eventArgs = new AttackEventArgs(enemy);
            OnEnemyAttacked(eventArgs);
        }

        public void ChangeHoldWeapon(IWeaponModel weapon)
        {
            HoldWeapon = weapon;
            _data.HoldWeaponId = weapon.Id;
        }

        private void OnEnemyAttacked(AttackEventArgs e)
        {
            EnemyAttacked?.Invoke(this, e);
        }

        private void OnHoldWeaponChanged()
        {
            HoldWeaponChanged?.Invoke(this, EventArgs.Empty);
        }
        
        private void OnHoldWeaponChanged(object sender, EventArgs e)
        {
            OnHoldWeaponChanged();
        }
    }
}