using SplashKitSDK;

public class Item {
    // Basic item information
    // protected Bitmap _ItemBitmap;
    public Sprite _ItemSprite { get; private set; }
    private const float SPEED = 3.0F;
    // public double X { get; set; }
    // public double Y { get; set; }
    public int Width { get { return _ItemSprite.Width; } }
    public int Height { get { return _ItemSprite.Width; } }

    // Extra fields
    public bool isDanger { get; set; }

    public Item(Window gameWindow) {        
        // Randomly pick a position on top of the screen
        // X = SplashKit.Rnd(gameWindow.Width);
        // Y = 0;
        
        isDanger = false;
        // Load bitmap and create Sprite
        Bitmap _ItemBitmap = new Bitmap("Apple", "Apple.png");
        _ItemSprite = SplashKit.CreateSprite("AppleSprite", _ItemBitmap);
        _ItemSprite.X = SplashKit.Rnd(gameWindow.Width);
        _ItemSprite.Y = 0;
    }

    public void Draw() {
        // Draw bitmap 
        _ItemSprite.Draw();
    }

    public void Update() {
        // The item fall from the top, so we only update the coordinate Y
        _ItemSprite.Y += SPEED;
    }

    public bool IsOffScreen(Window screen) {
        // Check if the item is out of window screen
        return (_ItemSprite.Y<-Height || _ItemSprite.Y>screen.Height);
    }
}