using System;
using SplashKitSDK;

namespace SpriteGame
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Game With Sprite", 1200, 600);

            AnimatedItemCatch animatedItemCatch = new AnimatedItemCatch(gameWindow);

            while ( !animatedItemCatch.Quit ) {
                // Let SplashKit to process event
                SplashKit.ProcessEvents();

                animatedItemCatch.HandleInput();
                animatedItemCatch.Update();
                animatedItemCatch.Draw();

                if ( gameWindow.CloseRequested ) {
                    break;
                }
            }
            gameWindow.Close();

            Console.WriteLine($"Time record:    {animatedItemCatch.TimeRecord} second(s)");
            Console.WriteLine($"Score record:   {animatedItemCatch.Scores} apple(s)");
        }
    }
}