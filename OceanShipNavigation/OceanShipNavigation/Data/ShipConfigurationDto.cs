using System.Collections.Generic;
using OceanShipNavigation.Constants;

namespace OceanShipNavigation.Data
{
    /// <summary>
    /// Data transfer object for ship configuration.
    /// </summary>
    public class ShipConfigurationDto
    {
        /// <summary>
        /// Ship instruction.
        /// </summary>
        public IEnumerable<Instruction> Instructions { get; set; }

        /// <summary>
        /// Current orientation of the ship.
        /// </summary>
        public Orientation Orientation { get; set; }

        /// <summary>
        /// Current coordinate x of the ship.
        /// </summary>
        public int CoordinateX { get; set; }

        /// <summary>
        /// Current coordinate y of the ship.
        /// </summary>
        public int CoordinateY { get; set; }
    }
}
