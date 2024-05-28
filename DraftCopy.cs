using System;
using SplashKitSDK;
public class DraftCopy {
    public void MainMethod()
        {
            Window gameWindow = new Window("Game With Sprite", 500, 500);

            // Load bitmap & define the cells
            // Cell detail: cell width, height, cols, rows, count
            Bitmap wizard = SplashKit.LoadBitmap("WitchIdle", "B_witch_idle_x3.png");
            wizard.SetCellDetails(96, 144, 1, 6, 6);
            
            // Load the animation script
            AnimationScript _IdleAnimationScript = SplashKit.LoadAnimationScript("WitchIdleScript", "witch_idle.txt");

            // Instead of create animation, we use sprite
            // Animation lizardAnimation = danceScript.CreateAnimation("Idle");
            Sprite _WizardSprite = SplashKit.CreateSprite(wizard, _IdleAnimationScript);

            _WizardSprite.StartAnimation("IdleAnimation");
            while(! gameWindow.CloseRequested) {
                gameWindow.Clear(Color.White);
                
                _WizardSprite.Draw(50, 50);

                gameWindow.Refresh(60);
                
                _WizardSprite.UpdateAnimation();
                SplashKit.ProcessEvents();
            }

            /*
            Window w = new Window("Lizard Animation", 500, 500);

            // Load bitmap, which containing the
            Bitmap lizard = SplashKit.LoadBitmap("WizardIdle","B_witch_idle_x3.png");
            // Set cell detail: cell width, height, cols, rows, count
            lizard.SetCellDetails(32*3, 48*3, 1, 6, 6); 

            // Load the animation script
            AnimationScript danceScript = SplashKit.LoadAnimationScript("WitchIdleScript", "witch_idle.txt");
            // Create the animation
            Animation lizardAnimation = danceScript.CreateAnimation("Idle");

            // Create a drawing option
            DrawingOptions opt;
            opt = SplashKit.OptionWithAnimation(lizardAnimation);

            // Basic event loop
            while(! w.CloseRequested)
            {
                // Draw the bitmap - using opt to link to animation
                w.Clear(Color.White);
                w.DrawBitmap(lizard, 20, 20, opt);
                w.Refresh(60);

                // Update the animation
                lizardAnimation.Update();

                SplashKit.ProcessEvents();
            }
            */
        }
}