using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Velentr.Font
{

    /// <summary>
    /// Extensions to SpriteBatch.Draw to handle Velentr.Font
    /// </summary>
// ReSharper disable once CheckNamespace
// ReSharper disable once UnusedMember.Global
    public static class DrawStringMarkdownExtensions
    {
        /// <summary>
        /// Draw text to the screen with the given Font.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="font">The Font to use when rendering the string</param>
        /// <param name="text">The string to render</param>
        /// <param name="position">Position at which to render the string</param>
        /// <param name="color">Color with which to render the string</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Font font, string text, Vector2 position, Color color)
        {
            font.Draw(spriteBatch, text, color, new Rectangle((int) position.X, (int) position.Y, 0, 0), true);
        }

        /// <summary>
        /// Draw text to the screen with the given Font.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modifications for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Font font, string text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            font.Draw(spriteBatch, text, color, new Rectangle((int)position.X, (int)position.Y, 0, 0), rotation, origin, scale, effects, layerDepth, true);
        }

        /// <summary>
        /// Draws text to the screen with the given font.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="font">The Font to use when rendering the string</param>
        /// <param name="text">The string to render.</param>
        /// <param name="position">Position at which to render the string</param>
        /// <param name="color">Color with which to render the string</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Font font, StringBuilder text, Vector2 position, Color color)
        {
            font.Draw(spriteBatch, text.ToString(), color, new Rectangle((int)position.X, (int)position.Y, 0, 0), true);
        }

        /// <summary>
        /// Draw text to the screen with the given Font.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="font">The font.</param>
        /// <param name="text">The text.</param>
        /// <param name="position">The position.</param>
        /// <param name="color">The color.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modifications for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Font font, StringBuilder text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            font.Draw(spriteBatch, text.ToString(), color, new Rectangle((int)position.X, (int)position.Y, 0, 0), rotation, origin, scale, effects, layerDepth, true);
        }

        /// <summary>
        /// Draw text to the screen with the given Font.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="text">The text to render.</param>
        /// <param name="position">Position at which to render the text</param>
        /// <param name="color">Color with which to render the text</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Text text, Vector2 position, Color color)
        {
            text.Draw(spriteBatch, position, color, true);
        }

        /// <summary>
        /// Draw text to the screen with the given Font.
        /// </summary>
        /// <param name="spriteBatch">The spritebatch</param>
        /// <param name="text">The text to render.</param>
        /// <param name="position">Position at which to render the text</param>
        /// <param name="color">Color with which to render the text</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modifications for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
        public static void DrawStringWithMarkdown(this SpriteBatch spriteBatch, Text text, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth)
        {
            text.Draw(spriteBatch, position, color, rotation, origin, scale, effects, layerDepth, true);
        }

    }

}
