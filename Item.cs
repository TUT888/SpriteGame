using SplashKitSDK;

public abstract class Item {
    // Basic item information
    public Sprite _ItemSprite { get; protected set; }

    // For the falling activity
    private const float SPEED = 3.0F;
    public bool IsFalling { get; set; }

    public Item(Window gameWindow) {
        IsFalling = true;
    }

    public void Draw() {
        // Draw bitmap 
        _ItemSprite.Draw();
    }

    public void Update() {
        // The item fall from the top, so we only update the coordinate Y
        if ( IsFalling ) {
            _ItemSprite.Y += SPEED;
        }
        _ItemSprite.UpdateAnimation();
    }

    public bool IsOffScreen(Window screen) {
        // Check if the item is out of window screen
        return (_ItemSprite.Y<-_ItemSprite.Height || _ItemSprite.Y>screen.Height);
    }
    
    public abstract Boolean Explode();
}

public class Bomb : Item {
    public Bomb(Window gameWindow) : base(gameWindow) {
        // Load bitmap and create Sprite
        // Unlike Apple, Bomb has animation so it must load animation script
        Bitmap _BombBitmap = new Bitmap("Bomb", "Bomb.png");
        _BombBitmap.SetCellDetails(169, 169, 5, 5, 25);
        AnimationScript _BombAnimationScript = SplashKit.LoadAnimationScript("BombScript", "bomb_explosion.txt");
        
        // Randomly pick a position on top of the screen
        _ItemSprite = SplashKit.CreateSprite("BombSprite", _BombBitmap, _BombAnimationScript);
        _ItemSprite.X = SplashKit.Rnd(gameWindow.Width);
        _ItemSprite.Y = 0;

        // Start the bomb's idle animation by default
        _ItemSprite.StartAnimation("IdleAnimation");
    }

    public override bool Explode()
    {
        _ItemSprite.StartAnimation("ExplodeAnimation");
        IsFalling = false;
        return true;
    }
}

public class Apple : Item {
    public Apple(Window gameWindow) : base(gameWindow) {
        // Load bitmap and create Sprite
        Bitmap _AppleBitmap = new Bitmap("Apple", "Apple.png");
        _ItemSprite = SplashKit.CreateSprite("AppleSprite", _AppleBitmap);

        // Randomly pick a position on top of the screen
        _ItemSprite.X = SplashKit.Rnd(gameWindow.Width);
        _ItemSprite.Y = 0;
    }

    public override bool Explode()
    {
        return false;
    }
}