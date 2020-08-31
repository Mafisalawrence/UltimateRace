using System;
namespace ultimaterace
{
    public static class Vehicle
    {
        // Robinson R22
        public const int chopperFuelCapacityGallons = 20;
        public const int chopperFuelUsagePerHourGallons = 8;
        public const int chopperAvgSpeedKmh = 180;    // Traveling in the air. No limits!
        public const int chopperTimeToreFuelHrs = 3;
        public const double chopperBreakDownProbability = 0.2;

        // KTM 450 Rally
        public const double bikeFuelTankLiters = 33.6;
        public const double bikeKmPerLitre = 8;
        public const int bikeSpeedMph = 100;      // Traveling on dirt roads. No cops there!
        public const double bikeTimetoreFuelHrs = 0.5;
        public const double bikeBreakDownProbability = 0.5;

        // Tesla Model-S
        public const int teslaEngineKw = 310;
        public const int teslaBatteryPack = 700;
        public const int teslaSpeed = 120;    // Traveling on public roads.
        public const double teslaTimetoreFuelHrs = 1;
        public const double teslaBreakdownProbability = 0.1;

        // Virginia-Class Submarine
        public const int nuclearSubSpeedKnots = 35; 
        public const double subBreakDownProbability = 0.0;
    }
}
