using Microsoft.Xna.Framework;

namespace Velentr.Font.Internal
{
    /// <summary>
    /// A Character in the Text object.
    /// </summary>
    internal class TextCharacter
    {
        /// <summary>
        /// The character.
        /// </summary>
        public Glyph Character;

        /// <summary>
        /// The position of the character in the Text object.
        /// </summary>
        public Vector2 Position;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextCharacter"/> class.
        /// </summary>
        /// <param name="character">The character.</param>
        /// <param name="position">The position.</param>
        public TextCharacter(Glyph character, Vector2 position)
        {
            Character = character;
            Position = position;
        }
    }
}
