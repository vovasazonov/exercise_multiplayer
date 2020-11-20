namespace System.Collections.Generic
{
    public static class ArrayExtensions
    {
        public static T[] Combine<T>(this T[] array1, T[] array2)
        {
            var commonArray = new T[array1.Length + array2.Length];
            Array.Copy(array1, commonArray, array1.Length);
            Array.Copy(array2, commonArray, array2.Length);
            return commonArray;
        }
    }
}