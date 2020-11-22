using System;

namespace Models.Characters
{
    public class WeaponChangedEventArgs : EventArgs
    {
        public int WeaponExemplarId { get; }

        public WeaponChangedEventArgs(int weaponExemplarId)
        {
            WeaponExemplarId = weaponExemplarId;
        }
    }
}