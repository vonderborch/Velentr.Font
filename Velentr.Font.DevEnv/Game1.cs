using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Velentr.Font.DevEnv
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

            var size1a = font1.MeasureText("Hello World!");
            var size1b = font1.MakeText("Hello World!").Size;
            var size1c = font1.MeasureText("Hello World!");

            var size2a = font1.MeasureText("Hello\nWorld!");
            var size2b = font1.MakeText("Hello\nWorld!").Size;
            var size2c = font1.MeasureText("Hello\nWorld!");



            var size3a = font2.MeasureText("Hello World!");
            var size3b = font2.MakeText("Hello World!").Size;
            var size3c = font2.MeasureText("Hello World!");

            var size4a = font2.MeasureText("Hello\nWorld!");
            var size4b = font2.MakeText("Hello\nWorld!").Size;
            var size4c = font2.MeasureText("Hello\nWorld!");

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Red);

            _spriteBatch.Begin();

            //_spriteBatch.DrawString(font1, testString, new Vector2(0, -15), Color.Blue);
            /*
            _spriteBatch.DrawStringWithMarkdown(font1, testString3, new Vector2(0, -15), Color.Black);
            _spriteBatch.DrawString(font1, testString, new Vector2(150, -15), Color.Pink, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text2, new Vector2(50, 150), Color.Red);
            _spriteBatch.DrawString(text1, new Vector2(150, 300), Color.Black, 0.1f, Vector2.Zero, new Vector2(.5f, .5f), SpriteEffects.None, 1f);
            _spriteBatch.DrawString(text1, new Vector2(150, 350), Color.Black, 0.1f, new Vector2(50, 50), new Vector2(.5f, .5f), SpriteEffects.FlipVertically, 1f);
            */

            var text = "cake";
            var text2 = font1.MakeText(text);
            //_spriteBatch.DrawString(font1, text, (new Vector2(75, 75) - (font1.MeasureText(text) / 2)), Color.Blue);
            //_spriteBatch.DrawString(font1, text, (new Vector2(75, 75) - (font1.MeasureText(text) / 2)), Color.White, 0, Vector2.Zero, Vector2.One, SpriteEffects.None, 0.5F);

            _spriteBatch.DrawString(text2, (new Vector2(75, 75) - (font1.MeasureText(text) / 2)), Color.Blue);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
