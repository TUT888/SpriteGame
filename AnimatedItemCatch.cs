using SplashKitSDK;

public class AnimatedItemCatch {
    // Game objects
    private Player _Player;
    private List<Item> _Items;
    // Game information
    private Window _GameWindow;
    private SplashKitSDK.Timer _GameTimer;
    // Game progress
    public bool Quit { get { return _Player.Quit; } }
    public int TimeRecord { get { return _Player.TimeRecord; } }
    public int Scores { get { return _Player.Scores; } }

    public AnimatedItemCatch(Window gameWindow) {
        _GameWindow = gameWindow;

        _GameTimer = new SplashKitSDK.Timer("Game Timer");
        _GameTimer.Start();

        // Initialize Player and Items
        _Player = new Player(_GameWindow);
        _Items = new List<Item>();
        _Items.Add(RandomItem());
    }

    // ====== Essential methods to be call ====== //
    // Handle the key input of the player
    public void HandleInput() {
        _Player.HandleInput();
        _Player.StayOnWindow(_GameWindow);
    }

    // Update data
    public void Update() {
        // Update the Player's data
        _Player.Update();
        _Player.UpdateProgress(_GameTimer);

        // Add new item, but limit the total items to 8
        if ( _Items.Count < 8 && TimeRecord%_Items.Count==0 && TimeRecord!=0 ) {
            _Items.Add(RandomItem());
        }
        // Update all Item's data: their location and animation
        foreach (Item Item in _Items) {
            Item.Update();
        }

        // Check Collision
        CheckCollision();
    }

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

    public Item RandomItem() {
        int rndNum = SplashKit.Rnd(2);
        Item Item;
        if ( rndNum==0 ) {
            Item = new Bomb(_GameWindow);
        } else {
            Item = new Apple(_GameWindow);
        }
        return Item;
    }

    private void CheckCollision() {
        List<Item> itemsToBeRemoved = new List<Item>();
        // Loop all items
        foreach (Item Item in _Items) {
            // Check if the player receive any falling item
            if ( Item.IsFalling && _Player.ReceiveItem(Item) ) {
                if ( Item.Explode() ) {
                    // If the item explode, reduce the player's live 
                    // and explosion animation will be start, item will be remove after
                    _Player.ReduceLive();
                } else {
                    // If the item not explode, increase the player's score
                    // and immediately remove this item
                    _Player.IncreaseScore();
                    itemsToBeRemoved.Add(Item);
                }
            }
            // else, remove item if it is offscreen
            else if ( Item.IsOffScreen(_GameWindow) ) {
                itemsToBeRemoved.Add(Item);
            } 
            // else, remove the item that has complete the explosion animation
            else if ( !Item.IsFalling && Item._ItemSprite.AnimationHasEnded ) {
                itemsToBeRemoved.Add(Item);
            }
        }
        // Remove items
        foreach (Item Item in itemsToBeRemoved) {
            _Items.Remove(Item);
        }
    }
}