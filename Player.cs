using SplashKitSDK;

public class Player {
    // Fields for state & sprite control
    // * _CurrentSprite: currently using Sprite
    // * currentState: stores action state, e.g. idle, run left, run right
    // * currenSpriteName: refer to the name defined while create sprite
    // * currentAnimationName: refer to the identifier defined in corresponding text script
    private Sprite _CurrentSprite;
    private string currentState;
    private string currentSpriteName { get { return currentState + "Sprite"; } }
    private string currentAnimationName { get { return currentState + "Animation"; } }
    
    // Other fields (removed X, Y since Sprite has its own X, Y)
    private const float SPEED = 5.0F;
    public float Width { get { return _CurrentSprite.Width; } }
    public float Height { get { return _CurrentSprite.Height; } }

    // Extra fields
    public float LAND_HEIGHT { get; set; }
    public bool Quit { get; private set; }
    public int Lives { get; private set; }
    public int TimeRecord { get; private set; }
    public int Scores { get; private set; }

    public Player(Window gameWindow) {
        // Defining height of the land that the player stands on
        LAND_HEIGHT = gameWindow.Height / 5;
        // Other game information
        Lives = 5;                  
        TimeRecord = 0;
        Scores = 0;

        // Create sprite
        CreateSprite();
        // Set default state, which is idle
        currentState = "IdleRight";
        _CurrentSprite = SplashKit.SpriteNamed(currentSpriteName);
        // Initialize the position for all Sprite
        SetPostionX((gameWindow.Width - Width) / 2);
        SetPostionY((gameWindow.Height - Height) - LAND_HEIGHT);

        // Start default animation
        _CurrentSprite.StartAnimation(currentAnimationName);
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


    // ====== Set values for Sprites ====== //
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

    public void SetState(string newState) {
        currentState = newState;
        _CurrentSprite = SplashKit.SpriteNamed(currentSpriteName);
        _CurrentSprite.StartAnimation(currentAnimationName);
    }

    // ====== Essential methods to be call ====== //
    // Sequential flow: Handle input -> Update -> Draw
    public void HandleInput() {
        // Basic move
        if ( SplashKit.KeyDown(KeyCode.LeftKey) ) {
            // Only start RunAnimation if it is not processing
            if ( currentState!="RunLeft" ) {
                SetState("RunLeft");
            }
            SetPostionX(_CurrentSprite.X - SPEED);
        } else if ( SplashKit.KeyDown(KeyCode.RightKey) ) {
            // Only start RunAnimation if it is not processing
            if ( currentState!="RunRight" ) {
                SetState("RunRight");
            }
            SetPostionX(_CurrentSprite.X + SPEED);
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
        // Check if RunAnimation ended, back to idle
        if ( _CurrentSprite.AnimationHasEnded ) {
            if ( currentState=="RunLeft" ) {
                SetState("IdleLeft");
            } else {
                SetState("IdleRight");
            }
        }
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
        if ( (_CurrentSprite.X+Width) > GAP_right ) {
            _CurrentSprite.X = GAP_right - Width;
        }
        if ( _CurrentSprite.Y < GAP_top ) {
            _CurrentSprite.Y = GAP_top;
        }
        if ( (_CurrentSprite.Y+Height) > GAP_bottom ) {
            _CurrentSprite.Y = GAP_bottom - Height;
        }
    }

    public bool ReceiveItem(Item otherItem) {
        // Check if the item fall on the player
        // return _PlayerBitmap.CircleCollision(X, Y, other.CollisionCircle);
        return _CurrentSprite.SpriteCollision(otherItem._ItemSprite);
    }

    public void ReduceLive() {
        if (Lives > 0) {
            Lives -= 1;
        } else {
            Lives = 0;
        }
    }

    public void increaseScore() {
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