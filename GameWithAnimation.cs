using SplashKitSDK;

public class GameWithAnimation {
    private Player _Player;
    private Window _GameWindow;
    private List<Item> _Items;
    public bool Quit { get { return _Player.Quit; } }
    private SplashKitSDK.Timer _GameTimer;
    public int TimeRecord { get { return _Player.TimeRecord; } }
    public int Scores { get { return _Player.Scores; } }

    public GameWithAnimation(Window gameWindow) {
        _GameWindow = gameWindow;

        _GameTimer = new SplashKitSDK.Timer("Game Timer");
        _GameTimer.Start();

        _Player = new Player(_GameWindow);
        // Initialize Item
        _Items = new List<Item>();
        // _Items.Add(RandomItem());
        _Items.Add(new Item(_GameWindow));
    }

    // ====== Essential methods to be call ====== //
    // Handle the key input of the player
    public void HandleInput() {
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }

    // Update data
    public void Update() {
        _Player.Update();
        
        _Player.UpdateProgress(_GameTimer);
        // Update all Items and check collision
        foreach (Item Item in _Items) {
            Item.Update();
        }

        CheckCollision();
        // Limit the amount of Items
        if ( _Items.Count < 8 && TimeRecord%_Items.Count==0 && TimeRecord!=0 ) {
            // _Items.Add(RandomItem());
            _Items.Add(new Item(_GameWindow));
        }
    }

    // Ok giong
    public void Draw() {
        // Clear window to draw new frame
        _GameWindow.Clear(Color.White);

        // Draw objects: the land, the player, the progress, the items
        SplashKit.FillRectangle(SplashKitSDK.Color.SandyBrown, 0, (_GameWindow.Height-_Player.LAND_HEIGHT), _GameWindow.Width, _Player.LAND_HEIGHT);
        _Player.Draw();
        _Player.DrawProgress();        
        foreach (Item Item in _Items) {
            Item.Draw();
        }

        // Refresh the window to make change
        _GameWindow.Refresh(60);
    }

    // New change in RandomItem()
    // public Item RandomItem() {
    //     int rndNum = SplashKit.Rnd(2);
    //     Item Item;
    //     if ( rndNum==0 ) {
    //         Item = new Bomb(_GameWindow);
    //     } else {
    //         Item = new Apple(_GameWindow);
    //     }
    //     return Item;
    // }

    private void CheckCollision() {
        List<Item> itemsToBeRemoved = new List<Item>();
        // Loop all items
        foreach (Item Item in _Items) {
            // Check if the player receive any item
            // if ( _Player.ReceiveItem(Item) ) {
            //     itemsToBeRemoved.Add(Item);
            //     // Check if the item is danger
            //     if ( Item.isDanger ) {
            //         _Player.ReduceLive();
            //     } else {
            //         _Player.increaseScore();
            //     }
            // } else 
            if ( _Player.ReceiveItem(Item) ) {
                Console.WriteLine("Received");
                itemsToBeRemoved.Add(Item);
            } else if ( Item.IsOffScreen(_GameWindow) ) {
                // Also remove item if it is offscreen
                Console.WriteLine("Out of screen");
                itemsToBeRemoved.Add(Item);
            }
        }
        // Remove items
        foreach (Item Item in itemsToBeRemoved) {
            _Items.Remove(Item);
        }
    }
}