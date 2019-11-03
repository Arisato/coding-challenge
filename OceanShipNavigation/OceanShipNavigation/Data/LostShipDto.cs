using System;
using OceanShipNavigation.Constants;

namespace OceanShipNavigation.Data
{
    /// <summary>
    /// Lost ships data transfer object.
    /// </summary>
    public class LostShipDto
    {
        /// <summary>
        /// Ship id.
        /// </summary>
        public Guid Id { get; set; }

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
