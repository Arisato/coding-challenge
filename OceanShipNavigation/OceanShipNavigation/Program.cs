using System;
using System.Collections.Generic;

namespace OceanShipNavigation
{
    class Program
    {
        static void Main(string[] args)
        {
            MissionControl missionControl = MissionControl.Create(15, 3);

            List<KeyValuePair<string, string>> shipConfigurations = new List<KeyValuePair<string, string>>();

            for (int i = 0; i < missionControl.NumberOfShips; i++)
            {
                var shipConfig = Console.ReadLine().ToUpperInvariant();
                var shipInstriction = Console.ReadLine().ToUpperInvariant();

                shipConfigurations.Add(KeyValuePair.Create(shipConfig, shipInstriction));
            }

            Console.WriteLine();

            missionControl.InitAndExecuteShips(shipConfigurations);
        }
    }
}
