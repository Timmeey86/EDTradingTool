﻿using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDTradingTool.Entity
{
    /// <summary>
    /// This class represents a solar system in Elite: Dangerous
    /// </summary>
    public class SolarSystem : EntityWithIdAndName
    {
        [Reference]
        public List<SpaceStation> SpaceStations { get; set; }

        public SolarSystem()
        {
            SpaceStations = new List<SpaceStation>();
        }
    }
}
