# Game with Sprites and Animation using C# SplashKit
This is a game project named **Animated Item Catch**.

Screencast of the game while running can be found at: [Game with animation using Sprite in SplashKit - C#](https://youtu.be/l3gkwFXfVLA)

![GameWindow](Walkthrough/images/GameWindow.png)

# Overview
General ideas of the game:
- Player is able to move on the screen horizontally.
- Items will slowly fall from the top. There are 2 types of items:
    - **Bomb**: A danger item that we need to dodge it, player will reduce lives when interact with bombs.
    - **Apple**: A safe item that we should receive, player will increase score when interact with apples.
- Game records includes remaining lives, scores gained from receiving apples, and the time we can survive.
- There is no limit of time for playing, player can press ESC to exit the game.

# Walkthrough
To keep this as a brief introduction page, please go to `Walkthrough` folder to view the game walkthrough.

Or use this quick link: <a href="/Walkthrough/">Walkthrough</a>

# Acknowledgements

Official guide/documentations that I read:

- SplashKit’s official API documentation of Animation: [Animations | SplashKit](https://splashkit.io/api/animations/)
- SplashKit’s official API documentation of Sprite: [Sprites | SplashKit](https://splashkit.io/api/sprites/#create-sprite)
- SplashKit’s official Animation guide: [Using Animations | SplashKit](https://splashkit.io/guides/03-00-animation/)

Game assets are downloaded from [itch.io](https://itch.io/game-assets):

- For Player’s Sprite, thanks **9EO** for sharing free asset: [Chick-Boy Animation Pack by 9E0 (itch.io)](https://9e0.itch.io/chick-boy)
- For Apple’s Sprite, thanks **elenetari** for sharing free asset: [apple sprites by elenetari (itch.io)](https://elenetari.itch.io/apple-sprites)
- For Bomb’s Sprite, thanks **AleezuX** for sharing free asset: [Free Sprite BOOM by AleezuX (itch.io)](https://aleezux.itch.io/free-sprite-bom)

Sound effects are downloaded from [Pixabay](https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=103779):

- For the Apple’s sound (*receive-apple.mp3),* refer to “Take Item Sound Effect” by [zennnsounds](https://pixabay.com/users/zennnsounds-35538808/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=163073) from [Pixabay](https://pixabay.com//?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=163073)
- For the Bomb’s sound (*explosion.mp3*), refer to “small explosion” by [Pixabay](https://pixabay.com/sound-effects/?utm_source=link-attribution&utm_medium=referral&utm_campaign=music&utm_content=103779)