using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Velentr.Collections.Collections;
using Velentr.Font.Internal;

namespace Velentr.Font
{
    /// <summary>
    /// A pre-generated list of characters that can be rendered
    /// </summary>
    public abstract class Text
    {
        /// <summary>
        /// The characters that are used by this Text object
        /// </summary>
        internal List<TextCharacter> Characters;

        /// <summary>
        /// The font that is used by this Text object
        /// </summary>
        protected Font Font;

        /// <summary>
        /// Gets the width.
        /// </summary>
        /// <value>
        /// The width.
        /// </value>
        public float Width { get; internal set; }

        /// <summary>
        /// Gets the width as an int.
        /// </summary>
        /// <value>
        /// The width int.
        /// </value>
        public int WidthInt => Helpers.ConvertFloatToInt(Width);

        /// <summary>
        /// Gets the height.
        /// </summary>
        /// <value>
        /// The height.
        /// </value>
        public float Height { get; internal set; }

        /// <summary>
        /// Gets the height as an int.
        /// </summary>
        /// <value>
        /// The height int.
        /// </value>
        public int HeightInt => Helpers.ConvertFloatToInt(Height);

        /// <summary>
        /// Gets the string that will be printed by this Text object.
        /// </summary>
        /// <value>
        /// The string.
        /// </value>
        public string String { get; internal set; }

        /// <summary>
        /// Gets the size of the object.
        /// </summary>
        /// <value>
        /// The size.
        /// </value>
        public Vector2 Size { get; internal set; }

        /// <summary>
        ///     The calculation cache.
        /// </summary>
        private SizeLimitedOrderedDictionary<(Vector2, float, Vector2, Vector2, SpriteEffects), List<Vector2>> _calculationCache = new SizeLimitedOrderedDictionary<(Vector2, float, Vector2, Vector2, SpriteEffects), List<Vector2>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        protected Text()
        {
            _calculationCache.MaxSize = 8;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Text"/> class.
        /// </summary>
        /// <param name="oldObject">The old object.</param>
        protected Text(Text oldObject)
        {
            String = oldObject.String;
            Size = oldObject.Size;
            Height = oldObject.Height;
            Width = oldObject.Width;
            Font = oldObject.Font;
            Characters = new List<TextCharacter>(oldObject.Characters);
            _calculationCache.MaxSize = oldObject.MaxCalculationCacheSize;
        }

        /// <summary>
        ///     Gets or sets the maximum size of the calculation cache.
        /// </summary>
        ///
        /// <value>
        ///     The maximum size of the calculation cache.
        /// </value>
        public long MaxCalculationCacheSize
        {
            get => _calculationCache.MaxSize;
            set => _calculationCache.MaxSize = value;
        }

        /// <summary>
        /// Adds a character to the Text object.
        /// </summary>
        /// <param name="character">The character to add.</param>
        internal void AddCharacter(TextCharacter character)
        {
            Characters.Add(character);
        }

        /// <summary>
        /// Draws the text to the string.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="position">The position to draw the text at.</param>
        /// <param name="color">The color to draw the text with.</param>
        /// <param name="color">The color to draw the text with.</param>
        /// <param name="applyMarkdown">Whether to apply markdown commands or not. Defaults to false. If set to true, Color will be the default color.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, bool applyMarkdown = false)
        {
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < Characters.Count; i++)
            {
                spriteBatch.Draw(Characters[i].Character.GlyphCache.Texture, Characters[i].Position + position, Characters[i].Character.Boundary, applyMarkdown ? Characters[i].Color ?? color : color);
            }
        }

        /// <summary>
        /// Draws the text to the string.
        /// </summary>
        /// <param name="spriteBatch">The sprite batch.</param>
        /// <param name="position">The position to draw the text at.</param>
        /// <param name="color">The color to draw the text with.</param>
        /// <param name="rotation">A rotation of this string.</param>
        /// <param name="origin">Center of the rotation. 0,0 by default.</param>
        /// <param name="scale">A scaling of this string.</param>
        /// <param name="effects">Modifications for drawing. Can be combined.</param>
        /// <param name="layerDepth">A depth of the layer of this string.</param>
        /// <param name="applyMarkdown">Whether to apply markdown commands or not. Defaults to false. If set to true, Color will be the default color.</param>
        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects effects, float layerDepth, bool applyMarkdown = false)
        {
            var key = (position, rotation, origin, scale, effects);
            if (_calculationCache.Exists(key))
            {
                var positions = _calculationCache.GetItem(key);
                for (var i = 0; i < Characters.Count; i++)
                {
                    spriteBatch.Draw(Characters[i].Character.GlyphCache.Texture, positions[i], Characters[i].Character.Boundary, applyMarkdown ? Characters[i].Color ?? color : color, rotation, origin, scale, effects, layerDepth);
                }
            }
            else
            {
                var flipAdjustment = Vector2.Zero;
                var flippedVertically = effects.HasFlag(SpriteEffects.FlipVertically);
                var flippedHorizontally = effects.HasFlag(SpriteEffects.FlipHorizontally);

                // if we've flipped, handle adjusting our location as required
                if (flippedVertically || flippedHorizontally)
                {
                    if (flippedHorizontally)
                    {
                        origin.X *= -1;
                        flipAdjustment.X -= Size.X;
                    }

                    if (flippedVertically)
                    {
                        origin.Y *= -1;
                        flipAdjustment.Y = Font.GlyphHeight - Size.Y;
                    }
                }

                // Handle our rotation as required
                var transformation = Matrix.Identity;
                float cos, sin = 0;
                var xScale = flippedHorizontally ? -scale.X : scale.X;
                var yScale = flippedVertically ? -scale.Y : scale.Y;
                var xOrigin = flipAdjustment.X - origin.X;
                var yOrigin = flipAdjustment.Y - origin.Y;
                if (Helpers.FloatsAreEqual(rotation, 0) || Helpers.FloatsAreEqual(rotation / Constants.TWO_PI, 1))
                {
                    transformation.M11 = xScale;
                    transformation.M22 = yScale;
                    transformation.M41 = xOrigin * transformation.M11 + position.X;
                    transformation.M42 = yOrigin * transformation.M22 + position.Y;
                }
                else
                {
                    cos = (float)Math.Cos(rotation);
                    sin = (float)Math.Sin(rotation);
                    transformation.M11 = xScale * cos;
                    transformation.M12 = xScale * sin;
                    transformation.M21 = yScale * -sin;
                    transformation.M22 = yScale * cos;
                    transformation.M41 = (xOrigin * transformation.M11 + yOrigin * transformation.M21) + position.X;
                    transformation.M42 = (xOrigin * transformation.M12 + yOrigin * transformation.M22) + position.Y;
                }

                var positions = new List<Vector2>();
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < Characters.Count; i++)
                {
                    var characterPosition = Characters[i].Position;
                    Vector2.Transform(ref characterPosition, ref transformation, out characterPosition);
                    positions.Add(characterPosition);
                    spriteBatch.Draw(Characters[i].Character.GlyphCache.Texture, characterPosition, Characters[i].Character.Boundary, applyMarkdown ? Characters[i].Color ?? color : color, rotation, origin, scale, effects, layerDepth);
                }

                _calculationCache.AddItem(key, positions);
            }
        }

    }
}
