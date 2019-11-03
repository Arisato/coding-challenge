using System.Collections.Generic;
using OceanShipNavigation.Constants;
using OceanShipNavigation.Data;

namespace OceanShipNavigation.Parsing
{
    /// <summary>
    /// Class for parsing ship configuration/instruction input.
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// Parse ship configuration.
        /// </summary>
        /// <param name="shipConfigurations">Ship location, orientation and instructions.</param>
        /// <returns>List of parsed configurations.</returns>
        public static List<ShipConfigurationDto> ParseShipConfiguration(List<KeyValuePair<string, string>> shipConfigurations)
        {
            List<ShipConfigurationDto> shipConfigs = new List<ShipConfigurationDto>();

            foreach (KeyValuePair<string, string> shipConfig in shipConfigurations)
            {
                string[] config = shipConfig.Key.Split(' ');

                if (config.Length < 3)
                {
                    continue;
                }

                ShipConfigurationDto parsedConfig = new ShipConfigurationDto
                {
                    CoordinateY = ParseCoordinate(config[0]),
                    CoordinateX = ParseCoordinate(config[1]),
                    Orientation = ParseOrientation(config[2]),
                    Instructions = ParseInstruction(shipConfig.Value)
                };

                shipConfigs.Add(parsedConfig);
            }

            return shipConfigs;
        }

        /// <summary>
        /// Parse ship coordinate.
        /// </summary>
        /// <param name="coordinate">Coordinate input.</param>
        /// <returns>Coordinate of type int.</returns>
        private static int ParseCoordinate(string coordinate)
        {
            return int.Parse(coordinate);
        }

        /// <summary>
        /// Parse orientation.
        /// </summary>
        /// <param name="orientation">Orientation input.</param>
        /// <returns>Orientation of type enum.</returns>
        private static Orientation ParseOrientation(string orientation)
        {
            switch (orientation)
            {
                case nameof(Orientation.N):
                    return Orientation.N;
                case nameof(Orientation.E):
                    return Orientation.E;
                case nameof(Orientation.S):
                    return Orientation.S;
                default:
                    return Orientation.W;
            }
        }

        /// <summary>
        /// Parse ship instruction.
        /// </summary>
        /// <param name="instructions">Instructions input.</param>
        /// <returns>Instruction of type enum.</returns>
        private static List<Instruction> ParseInstruction(string instructions)
        {
            List<Instruction> instructionsList = new List<Instruction>();

            foreach (var instruction in instructions)
            {
                switch (instruction.ToString())
                {
                    case nameof(Instruction.F):
                        instructionsList.Add(Instruction.F);
                        break;
                    case nameof(Instruction.R):
                        instructionsList.Add(Instruction.R);
                        break;
                    case nameof(Instruction.L):
                        instructionsList.Add(Instruction.L);
                        break;
                }
            }

            return instructionsList;
        }
    }
}
