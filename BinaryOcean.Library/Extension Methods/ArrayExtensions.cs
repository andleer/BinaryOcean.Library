using System;

namespace BinaryOcean.Library
{
    public static class ArrayExtensions
    {
       public static T[] SubArray<T>(this T[] array, int start, int length)
       {
           T[] result = new T[length];

           Array.Copy(array, start, result, 0, length);

           return result;
       }

       public static T[] Concat<T>(this T[] a, T[] array)
       {
           var result = new T[a.Length + array.Length];

           Array.Copy(a, 0, result, 0, a.Length);
           Array.Copy(array, 0, result, a.Length, array.Length);

           return result;
       }
    }
}