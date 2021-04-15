# Velentr.Font
An alternative solution for Monogame/FNA/XNA-derived frameworks that utilizes SharpFont to draw text rather than the traditional SpriteFont approach.

# Installation
There are nuget packages available for Monogame and FNA.
- Monogame: [Velentr.Font.Monogame](https://www.nuget.org/packages/Velentr.Font.Monogame/)
- FNA: [Velentr.Font.FNA](https://www.nuget.org/packages/Velentr.Font.FNA/)

Running into an error with the freetype6 dll such as the below in your project?
![Screenshot](https://github.com/vonderborch/Velentr.Font/blob/main/BadDependency.PNG?raw=true)

**To fix the error, add the following nuget package to your project: [SharpFont.NetStandard](https://www.nuget.org/packages/SharpFont.NetStandard/)**

# Usage
Approach 1: Draw Text Directly
```
var fontSize = 48;

var manager = new FontManager(GraphicsDevice);
var font = manager.GetFont("pathToFontFile", fontSize);
_spriteBatch.DrawString(font, "Hello World!", new Vector2(50, 50), Color.White);
```

Approach 2: Cache Text (is a bit quicker since we don't need to rebuild the text glyph list on subsequent calls)
```
var fontSize = 48;
var manager = new FontManager(GraphicsDevice);
var font = manager.GetFont("pathToFontFile", fontSize);
var text = font.MakeText("Hello World!");
_spriteBatch.DrawString(text, new Vector2(50, 50), Color.White);

```

# Markdown
Some new draw methods have been added to SpriteBatch (`DrawStringWithMarkdown`). These methods accept string that have had markdown applied to them, such as the following string: `Hello [c: Pink]World![/]`. Current Markdown Commands supported:
- `c`/`C`/`Color`/`COLOR`: Accepts the string name of an XNA-derived framework color (such as White, Black, etc. Anything under Color.).

# Example
Code:
```
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Font.MonogameDevEnv
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private string testString = "Hello\nWorld! 123 () *&^$%#";
        private string testString2 = "I am a test string!";
        private string testString3 = "I am a [c: blue]test[/] string!";
        private string fontFile1 = "Content\\PlayfairDisplayRegular-ywLOY.ttf";
        private string fontFile2 = "Content\\Trueno-wml2.otf";
        private Text text1;
        private Text text2;

        private FontManager manager;


        Font font1;
        Font font2;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            manager = new FontManager(GraphicsDevice);

            font1 = manager.GetFont(fontFile1, 80);
            text1 = font1.MakeText(testString);

            font2 = manager.GetFont(fontFile2, 34);
            text2 = font2.MakeText(testString2);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);

            _spriteBatch.Begin();

            //_spriteBatch.DrawString(font1, testString, new Vector2(0, -15), Color.Blue);
            _spriteBatch.DrawStringWithMarkdown(font1, testString3, new Vector2(0, -15), Color.Black);
            _spriteBatch.DrawString(font1, testString, new Vector2(150, -15), Color.Pink, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text2, new Vector2(50, 150), Color.Red);
            _spriteBatch.DrawString(text1, new Vector2(150, 300), Color.Black, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text1, new Vector2(150, 300), Color.Black, 0.1f, new Vector2(50, 50), new Vector2(.5f, .5f), SpriteEffects.FlipVertically, 1f);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}


```

Screenshot:
![Screenshot](https://github.com/vonderborch/Velentr.Font/blob/main/Example.PNG?raw=true)


# Future Plans
See list of issues under the Milestones: https://github.com/vonderborch/Velentr.Font/milestones
