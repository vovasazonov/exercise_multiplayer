namespace Game.Characters
{
    public interface IViewPooler<T>
    {
        T GetView();
        void ReturnView(T enemyCharacter);
    }
}