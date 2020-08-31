using ultimaterace.classes;

namespace ultimaterace
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var ultimateRace = new UltimateRace();
            ultimateRace.WelcomeMessage();
            ultimateRace.StartRace();
            ultimateRace.TrackRace();
        }
    }
}