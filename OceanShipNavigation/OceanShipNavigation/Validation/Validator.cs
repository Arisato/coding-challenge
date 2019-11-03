using System;
using System.Collections.Generic;
using OceanShipNavigation.Constants;

namespace OceanShipNavigation.Validation
{
    /// <summary>
    /// Class for validating ship data input.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Validate ship configuration/instruction.
        /// </summary>
        /// <param name="shipConfigurations">Ship coordinates/instructions.</param>
        public static void ValidateShipConfig(List<KeyValuePair<string, string>> shipConfigurations, MissionControl missionControl)
        {
            foreach (KeyValuePair<string, string> shipConfig in shipConfigurations)
            {
                string[] config = shipConfig.Key.Split(' ');

                if (config.Length < 3)
                {
                    return;
                }

                ValidateCoordinateY(config[0], missionControl);
                ValidateCoordinateX(config[1], missionControl);
                ValidateOrientation(config[2]);
                ValidateInstructions(shipConfig.Value);
            }
        }

        private static void ValidateCoordinateY(string coordinateY, MissionControl missionControl)
        {
            if (!int.TryParse(coordinateY, out int parsedCoordinateY))
            {
                throw new Exception($"Invalid string value: {parsedCoordinateY} for coordinate Y.");
            }

            if (parsedCoordinateY > 50)
            {
                throw new Exception($"Coordinate Y value: {parsedCoordinateY} cannot be more than 50.");
            }

            if (parsedCoordinateY > missionControl.GridY || parsedCoordinateY < 0)
            {
                throw
                    new Exception($"Cannot spawn the ship outside the grid. Current grid size is - Y{missionControl.GridY}:X{missionControl.GridX}");
            }
        }

        private static void ValidateCoordinateX(string coordinateX, MissionControl missionControl)
        {
            if (!int.TryParse(coordinateX, out int parsedCoordinateX))
            {
                throw new Exception($"Invalid string value: {parsedCoordinateX} for coordinate X.");
            }

            if (parsedCoordinateX > 50)
            {
                throw new Exception($"Coordinate X value: {parsedCoordinateX} cannot be more than 50.");
            }

            if (parsedCoordinateX > missionControl.GridX || parsedCoordinateX < 0)
            {
                throw
                    new Exception($"Cannot spawn the ship outside the grid. Current grid size is - Y{missionControl.GridY}:X{missionControl.GridX}");
            }
        }

        private static void ValidateOrientation(string orientation)
        {
            switch (orientation)
            {
                case nameof(Orientation.N):
                    break;
                case nameof(Orientation.E):
                    break;
                case nameof(Orientation.S):
                    break;
                case nameof(Orientation.W):
                    break;
                default:
                    throw new Exception($"Invalid orientation value: {orientation}. Allowed values - N, E, S, W.");
            }
        }

        private static void ValidateInstructions(string instruction)
        {
            if (instruction.Length > 99)
            {
                throw new Exception($"Instructions length: {instruction.Length} cannot be more than 99.");
            }

            foreach (char instructionChar in instruction)
            {
                switch (instructionChar.ToString())
                {
                    case nameof(Instruction.F):
                        break;
                    case nameof(Instruction.R):
                        break;
                    case nameof(Instruction.L):
                        break;
                    default:
                        throw new Exception($"Invalid instruction value: {instructionChar}. Allowed values - F, R, L.");
                }
            }
        }
    }
}
