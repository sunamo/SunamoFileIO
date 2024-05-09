﻿namespace SunamoFileIO._sunamo;

using SunamoExceptions;
internal static class IListStringExtensions
{
    public static IList<string> ToUnixLineEnding(this IList<string> t)
    {
        for (int i = 0; i < t.Count; i++)
        {
            t[i] = t[i].ToUnixLineEnding();
        }
        return t;
    }
}
