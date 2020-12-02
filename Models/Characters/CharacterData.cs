using System;

namespace Models.Characters
{
    public sealed class CharacterData : ICharacterData
    {
        public event EventHandler HoldWeaponUpdated;

        private int _holdWeaponExemplarId;
        private readonly HealthPointData _healthPointData = new HealthPointData();

        public string Id { get; set; }

        public int HoldWeaponExemplarId
        {
            get => _holdWeaponExemplarId;
            set
            {
                _holdWeaponExemplarId = value;
                CallHoldWeaponUpdated();
            }
        }

        public IHealthPointData HealthPointData => _healthPointData;

        private void CallHoldWeaponUpdated()
        {
            HoldWeaponUpdated?.Invoke(this, EventArgs.Empty);
        }
    }
}