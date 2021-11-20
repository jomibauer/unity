using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ListExtensions
{
    public static IList<TSource> Swap<TSource>(this IList<TSource> source, int indexA, int indexB)
    {
        TSource tmp = source[indexA];
        source[indexA] = source[indexB];
        source[indexB] = tmp;
        return source;
    }
}
