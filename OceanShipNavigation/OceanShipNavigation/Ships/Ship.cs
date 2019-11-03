using System;
using OceanShipNavigation.Constants;

namespace OceanShipNavigation.Ships
{
    /// <summary>
    /// Implementation for <see cref="IShip"/>.
    /// </summary>
    public class Ship : IShip
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="orientation"><see cref="Orientation"/></param>
        /// <param name="coordinateX"><see cref="CoordinateX"/></param>
        /// <param name="coordinateY"><see cref="CoordinateY"/></param>
        private Ship(Orientation orientation, int coordinateX, int coordinateY)
        {
            Orientation = orientation;
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// Creates new ship.
        /// </summary>
        /// <param name="orientation"><see cref="Orientation"/></param>
        /// <param name="coordinateX"><see cref="CoordinateX"/></param>
        /// <param name="coordinateY"><see cref="CoordinateY"/></param>
        /// <returns>New Ship object.</returns>
        public static Ship Create(Orientation orientation, int coordinateX, int coordinateY)
        {
            return new Ship(orientation, coordinateX, coordinateY);
        }

        /// <inheritdoc />
        public void MoveForward()
        {
            switch (Orientation)
            {
                case Orientation.N:
                    CoordinateY++;
                    break;
                case Orientation.E:
                    CoordinateX++;
                    break;
                case Orientation.S:
                    CoordinateY--;
                    break;
                case Orientation.W:
                    CoordinateX--;
                    break;
            }
        }

        /// <inheritdoc />
        public void TurnRight()
        {
            Orientation = GetEnumIncrement();
        }

        /// <inheritdoc />
        public void TurnLeft()
        {
            Orientation = GetEnumDecrement();
        }

        private Orientation GetEnumIncrement()
        {
            int value = (int)Orientation;
            value++;
            return Enum.IsDefined(typeof(Orientation), value) ? (Orientation)value : Orientation.N;
        }

        private Orientation GetEnumDecrement()
        {
            int value = (int)Orientation;
            value--;
            return Enum.IsDefined(typeof(Orientation), value) ? (Orientation)value : Orientation.W;
        }

        /// <summary>
        /// Ship id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Current orientation of the ship.
        /// </summary>
        public Orientation Orientation { get; private set; }

        /// <summary>
        /// Current coordinate x of the ship.
        /// </summary>
        public int CoordinateX { get; private set; }

        /// <summary>
        /// Current coordinate y of the ship.
        /// </summary>
        public int CoordinateY { get; private set; }
    }
}
