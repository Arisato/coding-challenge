using System;
using System.Collections.Generic;
using System.Linq;
using OceanShipNavigation.Constants;
using OceanShipNavigation.Data;
using OceanShipNavigation.Parsing;
using OceanShipNavigation.Ships;
using OceanShipNavigation.Validation;

namespace OceanShipNavigation
{
    /// <summary>
    /// Class for managing ships.
    /// </summary>
    public class MissionControl
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="numberOfShips"><see cref="NumberOfShips"/></param>
        /// <param name="gridSize">Ocean grid size.</param>
        private MissionControl(int gridSize, int numberOfShips)
        {
            GridX = gridSize;
            GridY = gridSize;
            NumberOfShips = numberOfShips;
        }

        /// <summary>
        /// Creates new mission control object.
        /// </summary>
        /// <param name="numberOfShips"><see cref="NumberOfShips"/></param>
        /// <param name="gridSize">Ocean grid size.</param>
        /// <returns>New mission control.</returns>
        public static MissionControl Create(int gridSize, int numberOfShips)
        {
            return new MissionControl(gridSize, numberOfShips);
        }

        /// <summary>
        /// Initializes ships and executes instructions.
        /// </summary>
        /// <param name="shipConfigurations">Ship coordinates/instructions.</param>
        public void InitAndExecuteShips(List<KeyValuePair<string, string>> shipConfigurations)
        {
            Validator.ValidateShipConfig(shipConfigurations, this);

            List<ShipConfigurationDto> shipConfigs = Parser.ParseShipConfiguration(shipConfigurations);

            ProcessShips(shipConfigs);
        }

        private void ProcessShips(List<ShipConfigurationDto> shipConfigs)
        {
            foreach (ShipConfigurationDto shipConfig in shipConfigs)
            {
                Ship ship = Ship.Create(shipConfig.Orientation, shipConfig.CoordinateX, shipConfig.CoordinateY);

                foreach (Instruction instruction in shipConfig.Instructions)
                {
                    PerformShipAction(ship, instruction);
                }

                EmitShipLocation(ship);
            }
        }

        private void PerformShipAction(Ship ship, Instruction instruction)
        {
            if (instruction == Instruction.F && IsValidAction(ship))
            {
                ship.MoveForward();
            }
            else if (instruction == Instruction.R)
            {
                ship.TurnRight();
            }
            else if (instruction == Instruction.L)
            {
                ship.TurnLeft();
            }

            CheckShipPosition(ship);
        }

        private bool IsValidAction(Ship ship)
        {
            if (LostShipsCoordinates
                .Any(x => x.Orientation == ship.Orientation && x.CoordinateX == ship.CoordinateX && x.CoordinateY == ship.CoordinateY))
            {
                return false;
            }
            return true;
        }

        private void CheckShipPosition(Ship ship)
        {
            if (ship.CoordinateY > GridY || ship.CoordinateY < 0 || ship.CoordinateX > GridX || ship.CoordinateX < 0)
            {
                LostShipsCoordinates
                    .Add(new LostShipDto
                    {
                        Id = ship.Id,
                        Orientation = ship.Orientation,
                        CoordinateX = ship.CoordinateX > GridX ? ship.CoordinateX - 1 : ship.CoordinateX < 0 ? 0 : ship.CoordinateX,
                        CoordinateY = ship.CoordinateY > GridY ? ship.CoordinateY - 1 : ship.CoordinateY < 0 ? 0 : ship.CoordinateY,
                    });
            }
        }

        private void EmitShipLocation(Ship ship)
        {
            if (LostShipsCoordinates.Any(x => x.Id == ship.Id))
            {
                LostShipDto lostShip = LostShipsCoordinates.First(x => x.Id == ship.Id);
                Console.WriteLine($"{lostShip.CoordinateY} {lostShip.CoordinateX} {lostShip.Orientation} {Lost}");
            }
            else
            {
                Console.WriteLine($"{ship.CoordinateY} {ship.CoordinateX} {ship.Orientation}");
            }
        }

        /// <summary>
        /// Ocean grid latitude length.
        /// </summary>
        public int GridX { get; private set; }

        /// <summary>
        /// Ocean grid longitude length.
        /// </summary>
        public int GridY { get; private set; }

        /// <summary>
        /// Label for ships off the grid.
        /// </summary>
        public string Lost { get { return "LOST"; } }

        /// <summary>
        /// Lost ships coordinates.
        /// </summary>
        public List<LostShipDto> LostShipsCoordinates { get; private set; } = new List<LostShipDto>();

        /// <summary>
        /// Number of ships observed.
        /// </summary>
        public int NumberOfShips { get; private set; }
    }
}
