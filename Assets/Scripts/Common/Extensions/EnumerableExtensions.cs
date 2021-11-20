using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EnumerableExtensions
{

    public static IEnumerable<TSource> Interleave<TSource>(this IEnumerable<TSource> source1, IEnumerable<TSource> source2)
    {

        if (source1 == null) { throw new ArgumentNullException(nameof(source1)); }
        if (source2 == null) { throw new ArgumentNullException(nameof(source2)); }

        using (var enumerator1 = source1.GetEnumerator())
        {
            using (var enumerator2 = source2.GetEnumerator())
            {

                var continue1 = true;
                var continue2 = true;

                do
                {

                    if (continue1 = enumerator1.MoveNext())
                    {
                        yield return enumerator1.Current;
                    }

                    if (continue2 = enumerator2.MoveNext())
                    {
                        yield return enumerator2.Current;
                    }

                }
                while (continue1 || continue2);

            }
        }

    }

    public static void Print<TSource>(this IEnumerable<TSource> source)
    {
        string res = string.Join(", ", source);
        Debug.Log(res);
    }

    

}