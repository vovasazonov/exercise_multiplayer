using System.Collections.Generic;

namespace Descriptions
{
    public interface IDescriptionManager
    {
        IEnumerable<ICharacterDescription> CharacterDescriptionsList { get; }
        IEnumerable<IWeaponDescription> WeaponDescriptionsList { get; }
    }
}