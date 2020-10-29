namespace Network
{
    public enum GameCommandType : byte
    {
        PlayerDisconnected,
        PlayerConnected,
        CharacterRemove,
        CharacterAdd,
        CharacterHpChanged,
        CharacterAttackEnemy,
        SetControllablePlayer
    }
}