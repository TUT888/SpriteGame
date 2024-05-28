using System;
using SplashKitSDK;

namespace SpriteGame
{
    public class Program
    {
        public static void Main()
        {
            Window gameWindow = new Window("Game With Sprite", 1200, 600);

            GameWithAnimation gameWithAnimation = new GameWithAnimation(gameWindow);

            while ( !gameWithAnimation.Quit ) {
                // Let SplashKit to process event
                SplashKit.ProcessEvents();

                gameWithAnimation.HandleInput();
                gameWithAnimation.Update();
                gameWithAnimation.Draw();

                if ( gameWindow.CloseRequested ) {
                    break;
                }
            }
            gameWindow.Close();

            // Load bitmap & define the cells
            // Cell detail: cell width, height, cols, rows, count
            // Bitmap wizard = SplashKit.LoadBitmap("WitchIdle", "B_witch_idle_x3.png");
            // wizard.SetCellDetails(96, 144, 1, 6, 6);
            
            // // Load the animation script
            // AnimationScript _IdleAnimationScript = SplashKit.LoadAnimationScript("WitchIdleScript", "witch_idle.txt");

            // // Instead of create animation, we use sprite
            // // Animation lizardAnimation = danceScript.CreateAnimation("Idle");
            // Sprite _WizardSprite = SplashKit.CreateSprite(wizard, _IdleAnimationScript);

            // _WizardSprite.StartAnimation("IdleAnimation");
            // while(! gameWindow.CloseRequested) {
            //     SplashKit.ProcessEvents();
            //     gameWindow.Clear(Color.White);
            //     _WizardSprite.UpdateAnimation();
            //     _WizardSprite.Draw(50, 50);

            //     gameWindow.Refresh(60);
                
            // }
        }
    }
}
