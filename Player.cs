using SplashKitSDK;

public class Player {
    // Fields for state & sprite control
    // * _CurrentSprite: currently using Sprite
    // * _CurrentState: stores action state, e.g. idle, run left, run right
    // * _CurrentSpriteName: refer to the name defined while create sprite
    // * _CurrentAnimationName: refer to the identifier defined in corresponding text script
    private Sprite _CurrentSprite;
    private string _CurrentState;
    private string _CurrentSpriteName { get { return _CurrentState + "Sprite"; } }
    private string _CurrentAnimationName { get { return _CurrentState + "Animation"; } }
    
    // Controlling the speed
    private const float SPEED = 5.0F;

    // Storing game progress
    public int Lives { get; private set; }
    public int TimeRecord { get; private set; }
    public int Scores { get; private set; }

    // Extra
    public float LAND_HEIGHT { get; set; }
    public bool Quit { get; private set; }

    public Player(Window gameWindow) {
        // Defining height of the land that the player stands on
        LAND_HEIGHT = gameWindow.Height / 5;
        // Other game information
        Lives = 5;                  
        TimeRecord = 0;
        Scores = 0;

        // Create sprite
        CreateSprite();
        // Set default state, which is idle right
        _CurrentState = "IdleRight";
        _CurrentSprite = SplashKit.SpriteNamed(_CurrentSpriteName);
        // Initialize the position for all Sprite
        SetPostionX((gameWindow.Width - _CurrentSprite.Width) / 2);
        SetPostionY((gameWindow.Height - _CurrentSprite.Height) - LAND_HEIGHT);

        // Start default animation
        _CurrentSprite.StartAnimation(_CurrentAnimationName);
    }

    public void CreateSprite() {
        // Load bitmap contains cells
        Bitmap _ChikBoyIdleLeftBitmap = SplashKit.LoadBitmap("ChikBoyIdleLeft", "ChikBoy_idle_left.png");
        Bitmap _ChikBoyIdleRightBitmap = SplashKit.LoadBitmap("ChikBoyIdleRight", "ChikBoy_idle_right.png");
        Bitmap _ChikBoyRunLeftBitmap = SplashKit.LoadBitmap("ChikBoyRunLeft", "ChikBoy_run_left.png");
        Bitmap _ChikBoyRunRightBitmap = SplashKit.LoadBitmap("ChikBoyRunRight", "ChikBoy_run_right.png");
        // Set cell detail: cell width, height, cols, rows, count
        _ChikBoyIdleLeftBitmap.SetCellDetails(96, 96, 1, 6, 6);
        _ChikBoyIdleRightBitmap.SetCellDetails(96, 96, 1, 6, 6);
        _ChikBoyRunLeftBitmap.SetCellDetails(96, 96, 1, 10, 10);
        _ChikBoyRunRightBitmap.SetCellDetails(96, 96, 1, 10, 10);
        // Load the animation script (animation script of left/right is the same)
        AnimationScript _IdleAnimationScript = SplashKit.LoadAnimationScript("ChikBoyIdleScript", "chikboy_idle.txt");
        AnimationScript _RunAnimationScript = SplashKit.LoadAnimationScript("ChikBoyRunScript", "chikboy_run.txt");
        // Use sprite to manage animations
        SplashKit.CreateSprite("IdleLeftSprite",_ChikBoyIdleLeftBitmap, _IdleAnimationScript);
        SplashKit.CreateSprite("IdleRightSprite",_ChikBoyIdleRightBitmap, _IdleAnimationScript);
        SplashKit.CreateSprite("RunLeftSprite", _ChikBoyRunLeftBitmap, _RunAnimationScript);
        SplashKit.CreateSprite("RunRightSprite", _ChikBoyRunRightBitmap, _RunAnimationScript);
    }


    // ====== Set position for Sprites ====== //
    public void SetPostionX(float newX) {
        SplashKit.SpriteNamed("IdleLeftSprite").X = newX;
        SplashKit.SpriteNamed("IdleRightSprite").X = newX;
        SplashKit.SpriteNamed("RunLeftSprite").X = newX;
        SplashKit.SpriteNamed("RunRightSprite").X = newX;
    }

    public void SetPostionY(float newY) {
        SplashKit.SpriteNamed("IdleLeftSprite").Y = newY;
        SplashKit.SpriteNamed("IdleRightSprite").Y = newY;
        SplashKit.SpriteNamed("RunLeftSprite").Y = newY;
        SplashKit.SpriteNamed("RunRightSprite").Y = newY;
    }

    // ====== Set state for Sprite ====== //
    public void SetState(string newState) {
        _CurrentState = newState;
        _CurrentSprite = SplashKit.SpriteNamed(_CurrentSpriteName);
        _CurrentSprite.StartAnimation(_CurrentAnimationName);
    }

    // ====== Essential methods to be call ====== //
    // Sequential flow: Handle input -> Update -> Draw
    public void HandleInput() {
        // Speed up for dashing
        float actual_speed = SPEED;
        if ( SplashKit.KeyDown(KeyCode.SpaceKey) ) {
            actual_speed *= 2;
        } 

        // Basic move
        if ( SplashKit.KeyDown(KeyCode.LeftKey) ) {
            // Only start RunAnimation if it is not processing
            if ( _CurrentState!="RunLeft" ) {
                SetState("RunLeft");
            }
            SetPostionX(_CurrentSprite.X - actual_speed);
        } else if ( SplashKit.KeyDown(KeyCode.RightKey) ) {
            // Only start RunAnimation if it is not processing
            if ( _CurrentState!="RunRight" ) {
                SetState("RunRight");
            }
            SetPostionX(_CurrentSprite.X + actual_speed);
        } else if ( SplashKit.KeyReleased(KeyCode.LeftKey) ) {
            // Back to idle left if release left key
            SetState("IdleLeft");
        } else if (  SplashKit.KeyReleased(KeyCode.RightKey) ) {
            // Back to idle right if release right key
            SetState("IdleRight");
        }

        // Quit game
        if ( SplashKit.KeyDown(KeyCode.EscapeKey) ) {
            Quit = true;
        }
    }

    public void Update() {
        // Update animation
        _CurrentSprite.UpdateAnimation();
    }

    public void Draw() {
        _CurrentSprite.Draw();
    }

    // ====== Other methods ====== //
    public void StayOnWindow(Window limit) {
        // Define the gap between player and window frames
        const int GAP = 10;
        int GAP_left = GAP;
        int GAP_top = GAP;
        int GAP_right = limit.Width - GAP;
        int GAP_bottom = limit.Height - GAP;

        // Keep the player in the window
        if ( _CurrentSprite.X < GAP_left ) {
            _CurrentSprite.X = GAP_left;
        }
        if ( (_CurrentSprite.X+_CurrentSprite.Width) > GAP_right ) {
            _CurrentSprite.X = GAP_right - _CurrentSprite.Width;
        }
        if ( _CurrentSprite.Y < GAP_top ) {
            _CurrentSprite.Y = GAP_top;
        }
        if ( (_CurrentSprite.Y+_CurrentSprite.Height) > GAP_bottom ) {
            _CurrentSprite.Y = GAP_bottom - _CurrentSprite.Height;
        }
    }

    public bool ReceiveItem(Item otherItem) {
        // Check if the item fall on the player
        return _CurrentSprite.SpriteCollision(otherItem._ItemSprite);
    }

    public void ReduceLive() {
        if (Lives > 0) {
            Lives -= 1;
        } else {
            Lives = 0;
        }
    }

    public void IncreaseScore() {
        Scores += 1;
    }

    public void UpdateProgress(SplashKitSDK.Timer timer) {
        if (Lives == 0) {
            Quit = true;
        }
        TimeRecord = Convert.ToInt32(timer.Ticks / 1000);
    }

    public void DrawProgress() {
        SplashKit.DrawText($"TimeRecord: {TimeRecord}", SplashKitSDK.Color.Black, "BOLD_FONT", 12, 20, 20);
        SplashKit.DrawText($"Lives: {Lives}", SplashKitSDK.Color.Black, "BOLD_FONT", 12, 20, 50);
        SplashKit.DrawText($"Score: {Scores}", SplashKitSDK.Color.Black, "BOLD_FONT", 12, 20, 80);
    }
}