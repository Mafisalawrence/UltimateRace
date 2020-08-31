using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ultimaterace.models;

namespace ultimaterace.classes
{
    public class UltimateRace
    {
        private readonly Scoreboard scoreBoard = new Scoreboard();
        private readonly Random random = new Random();
        private bool bikeHasStopped, teslaHasStopped, nuclearSubHasStopped, chopperHasStopped;
        private int position = 1;
        private Dictionary<string, int> rankings = new Dictionary<string, int>();

        public void WelcomeMessage()
        {

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************************************************");
            Console.WriteLine($"{Graphics.welcome}".PadRight(Console.WindowWidth - 1));
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.WriteLine("\t  To the ULTIMATE RACE");
            Thread.Sleep(1000);
            Console.WriteLine("\tTravel from Cairo to Cape Town");
            Thread.Sleep(1000);
            Console.Write("     A journey of blood");
            Thread.Sleep(1000);
            Console.Write(", sweat");
            Thread.Sleep(1000);
            Console.WriteLine(", and gears");
            Thread.Sleep(1000);
            Console.WriteLine("  Ladies and Gentlemen, start your engine\n");
            Thread.Sleep(1000);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("********************************************************");
            Thread.Sleep(1000);
            Console.WriteLine($"{Graphics.three}");
            Thread.Sleep(1000);
            Console.WriteLine($"{Graphics.two}");
            Thread.Sleep(1000);
            Console.WriteLine($"{Graphics.one}");
            Thread.Sleep(1000);
            Console.ResetColor();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"{Graphics.go}");
            Console.ResetColor();
            Thread.Sleep(1000);

        }
        public void StartRace()
        {
            Task startBike = Task.Factory.StartNew(() => StartBike());
            Task startChopper = Task.Factory.StartNew(() => StartChopper());
            Task startTesla = Task.Factory.StartNew(() => StartTesla());
            Task startNuclearSub = Task.Factory.StartNew(() => StartNuclearSub());
            Task.WaitAll();
        }

        public void TrackRace()
        {

            while (!(bikeHasStopped && teslaHasStopped && chopperHasStopped && nuclearSubHasStopped))
            {
                // different modes of transport can take shorter routes
                if (!bikeHasStopped && scoreBoard.BikePosition > VehicleDistanceToTravel.bike)
                {
                    UpdateVehicleEndOfRaceStatus("Bike");
                    bikeHasStopped = true;
                }
                else if (!teslaHasStopped && scoreBoard.TeslaPostion > VehicleDistanceToTravel.tesla)
                {
                    UpdateVehicleEndOfRaceStatus("Tesla");
                    teslaHasStopped = true;
                }
                else if (!chopperHasStopped && scoreBoard.ChopperPosition > VehicleDistanceToTravel.chopper)
                {
                    UpdateVehicleEndOfRaceStatus("Chopper");
                    chopperHasStopped = true;

                }
                else if (!nuclearSubHasStopped && scoreBoard.SubPosition > VehicleDistanceToTravel.sub)
                {
                    UpdateVehicleEndOfRaceStatus("Nuclear sub");
                    nuclearSubHasStopped = true;
                }
                // Every second represents an hour
                Thread.Sleep(1000);
                Console.WriteLine("\n-------------------------------------------------------------------------\n");
            }
            WriteToConsole(Graphics.endRace, false, true);
        }
        private void UpdateVehicleEndOfRaceStatus(string vehicle)
        {

            if (position == 1)
            {
                string vehicleWon = $"{vehicle} has won!";
                rankings.Add(vehicle, 1);
                WriteToConsole(vehicleWon);
            }
            else
            {
                string vehicleCameInPosition = $"{vehicle} came in position { position}";
                rankings.Add(vehicle, position);
                WriteToConsole(vehicleCameInPosition);
            }
            position++;
        }
        private void WriteToConsole(string value, bool alert = false, bool endRace = false)
        {
            Console.ForegroundColor = alert ? ConsoleColor.Red : ConsoleColor.Yellow;
            Console.WriteLine("\n*************************************************************************");
            Console.WriteLine($"\t\t {value}.");
            if (endRace)
            {
                Console.WriteLine("Vehicle \t\t\t\t\t\t\t Position\n");
                Console.WriteLine("-----------------------------------------------------------------------");
                foreach (var vehicle in rankings)
                {
                    Console.WriteLine($"{vehicle.Key} \t\t\t\t\t\t\t {vehicle.Value}");
                }
            }
            Console.WriteLine("\n*************************************************************************\n");
            Console.ResetColor();
        }
        private void StartBike()
        {
            scoreBoard.BikePosition = 0;
            int fuel = (int)Vehicle.bikeFuelTankLiters;

            while (!bikeHasStopped)
            {
                Thread.Sleep(1000);
                scoreBoard.BikePosition += (int)(Vehicle.bikeSpeedMph * 1.6);
                Console.WriteLine($"Bike has travellled \t\t\t\t {scoreBoard.BikePosition}km");
                fuel -= (int)(Vehicle.bikeSpeedMph / Vehicle.bikeFuelTankLiters);

                if (fuel < 0)
                {
                    WriteToConsole("Bike out of fuel", true);
                    Thread.Sleep((int)(Vehicle.bikeTimetoreFuelHrs * 1000));
                    fuel = (int)Vehicle.bikeFuelTankLiters;
                    Console.WriteLine("Bike is back on the dirt tracks and in the race!!");
                }

                if (random.NextDouble() < Vehicle.bikeBreakDownProbability)
                {
                    VehicleBrokenDown("Bike");
                }
            }
        }
        private void StartChopper()
        {
            scoreBoard.ChopperPosition = 0;
            int fuel = Vehicle.chopperFuelCapacityGallons;

            while (!chopperHasStopped)
            {
                Thread.Sleep(1000);
                scoreBoard.ChopperPosition += Vehicle.chopperAvgSpeedKmh;
                Console.WriteLine($"Chopper has travelled \t\t\t\t {scoreBoard.ChopperPosition }km");
                fuel -= Vehicle.chopperFuelCapacityGallons;

                if (fuel < 0)
                {
                    WriteToConsole("Chopper out of fuel", true);
                    Thread.Sleep(Vehicle.chopperTimeToreFuelHrs * 1000);
                    fuel = Vehicle.chopperFuelCapacityGallons;
                    Console.WriteLine("Chopper is back in the air and in the race!!");
                }

                if (random.NextDouble() < Vehicle.chopperBreakDownProbability)
                {
                    VehicleBrokenDown("Chopper");
                }
            }
        }
        private void VehicleBrokenDown(string vehicleType)
        {
            int repairTime = (int)(random.NextDouble() / random.NextDouble() * 1000) + 1000; // Stop at least 1 hour
            string vehicleBrokenDown = $"{vehicleType} has broken down. It will take {repairTime / 1000}hrs to repair";
            WriteToConsole(vehicleBrokenDown, true);
            Thread.Sleep(repairTime);
            Console.WriteLine($"{vehicleType} is back on the road and in the race!!");
        }
        private void StartTesla()
        {
            double batteryLeft = Vehicle.teslaBatteryPack;
            scoreBoard.TeslaPostion = 0;

            while (!teslaHasStopped)
            {
                Thread.Sleep(1000);
                scoreBoard.TeslaPostion += Vehicle.teslaSpeed;
                Console.WriteLine($"Tesla has travelled \t\t\t\t {scoreBoard.TeslaPostion}km");
                batteryLeft -= Vehicle.teslaEngineKw;

                if (batteryLeft < 0)
                {
                    WriteToConsole("Tesla out of battery", true);
                    Thread.Sleep((int)Vehicle.teslaTimetoreFuelHrs * 1000);
                    batteryLeft = Vehicle.teslaBatteryPack;
                    Console.WriteLine("Tesla is back on the road and in the race!!");
                }

                if (random.NextDouble() < Vehicle.teslaBreakdownProbability)
                {
                    VehicleBrokenDown("Tesla");
                }
            }
        }
        private void StartNuclearSub()
        {
            scoreBoard.SubPosition = 0;

            while (!nuclearSubHasStopped)
            {
                Thread.Sleep(1000); // Every second represents an hour
                scoreBoard.SubPosition += Vehicle.nuclearSubSpeedKnots * 2;

                Console.WriteLine($"Nuclear sub has travelled \t\t\t {scoreBoard.SubPosition}km");

                if (random.NextDouble() < Vehicle.subBreakDownProbability)
                {
                    VehicleBrokenDown("Nuclear sub");
                }
            }
        }

    }
}
