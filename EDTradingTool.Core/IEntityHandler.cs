﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTradingTool.Core
{
    /// <summary>
    /// This class handles any action related to entities.
    /// </summary>
    public interface IEntityHandler
    {
        /// <summary>
        /// Registers an entity watcher which will from this point on be notified about any entity changes for its handled type.
        /// </summary>
        /// <param name="entityWatcher">The entity watcher to add.</param>
        /// <typeparam name="T">The type of the entity watcher.</typeparam>
        void RegisterEntityWatcher<T>(Core.IEntityWatcher<T> entityWatcher) where T : Core.IEntity;

        /// <summary>
        /// Unregisters an entity watcher so that it no longer receives entity notifications.
        /// </summary>
        /// <param name="entityWatcher">The entity watcher to add.</param>
        /// <typeparam name="T">The type of the entity watcher.</typeparam>
        void UnregisterEntityWatcher<T>(Core.IEntityWatcher<T> entityWatcher) where T : Core.IEntity;

        /// <summary>
        /// Handles the addition of an object.
        /// </summary>
        /// <typeparam name="T">The type of the entity object to add.</typeparam>
        /// <param name="obj">The object to add.</param>
        /// <param name="relatedObjects">The objects obj is related to.</param>
        void AddObject<T>(T obj, params object[] relatedObjects) where T : IEntity;

        /// <summary>
        /// Handles an update of an object.
        /// </summary>
        /// <typeparam name="T">They type of the entity object to update.</typeparam>
        /// <param name="obj">The object to update.</param>
        void UpdateObject<T>(T obj) where T : IEntity;

        /// <summary>
        /// Hanles the removal of an object.
        /// </summary>
        /// <typeparam name="T">The type of the entity object to remove.</typeparam>
        /// <param name="obj">The object to remove.</param>
        void RemoveObject<T>(T obj) where T : IEntity;
    }
}
