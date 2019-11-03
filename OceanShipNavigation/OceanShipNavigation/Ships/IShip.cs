namespace OceanShipNavigation.Ships
{
    /// <summary>
    /// The default ship behaviour.
    /// </summary>
    public interface IShip
    {
        /// <summary>
        /// Ship is moved forward on the grid based on current orientation.
        /// </summary>
        public void MoveForward();

        /// <summary>
        /// Ship orientation turned clockwise based on current orientation.
        /// </summary>
        public void TurnRight();

        /// <summary>
        /// Ship orientation turned counter-clockwise based on current orientation.
        /// </summary>
        public void TurnLeft();
    }
}
