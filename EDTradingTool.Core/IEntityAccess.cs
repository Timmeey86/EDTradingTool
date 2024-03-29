﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTradingTool.Core
{
    /// <summary>
    /// This interface must be implemented by classes which access database entities directly.
    /// </summary>
    public interface IEntityAccess
    {
        long AddObject<T>(T dataSet) where T : IEntity;
        void UpdateObject<T>(T dataSet) where T : IEntity;
        void RemoveObject<T>(T dataSet) where T : IEntity;
        T GetObject<T>(long primaryKey) where T : IEntity;
        T GetObject<T>(String name) where T : IEntity;
        List<T> GetAll<T>() where T : IEntity;
        bool HasObject<T>(long primaryKey) where T : IEntity;
        bool HasObject<T>(String name) where T : IEntity;
        void LoadAll();
        void DeleteAll();
        List<Type> HandledTypes { get; }
    }
}
