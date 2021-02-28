using System;

namespace AsteroidsGame
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new SpaceGame())
                game.Run();
        }
    }
}
