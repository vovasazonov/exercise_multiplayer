﻿namespace Replications
{
    public interface ICustomCastObject
    {
        TResult To<TResult>(object obj);
    }
}