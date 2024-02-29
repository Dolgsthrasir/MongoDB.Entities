﻿using System.Collections.Generic;

namespace MongoDB.Entities;

public static partial class DB
{
    /// <summary>
    /// Retrieves the 'change-stream' watcher instance for a given unique name. 
    /// If an instance for the name does not exist, it will return a new instance. 
    /// If an instance already exists, that instance will be returned.
    /// </summary>
    /// <typeparam name="T">The entity type to get a watcher for</typeparam>
    /// <param name="name">A unique name for the watcher of this entity type. Names can be duplicate among different entity types.</param>
    public static Watcher<T> Watcher<T>(string name) where T : IEntity
    {
        // Normalize the name once
        var normalizedName = name.ToLower().Trim();

        // Use GetOrAdd to ensure thread-safe retrieval or addition of the watcher.
        return Cache<T>.Watchers.GetOrAdd(normalizedName, newName => new Watcher<T>(newName));
    }

    /// <summary>
    /// Returns all the watchers for a given entity type
    /// </summary>
    /// <typeparam name="T">The entity type to get the watcher of</typeparam>
    public static IEnumerable<Watcher<T>> Watchers<T>() where T : IEntity => Cache<T>.Watchers.Values;
}
