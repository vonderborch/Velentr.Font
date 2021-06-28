using System.Collections.Generic;

namespace Velentr.Font.Internal
{
    /// <summary>
    /// The implementation of the Text object.
    /// </summary>
    /// <seealso cref="Text" />
    public class TextImplementation : Text
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextImplementation"/> class.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="font">The font.</param>
        public TextImplementation(string text, Font font)
        {
            String = text;
            Characters = new List<TextCharacter>();
            Font = font;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextImplementation"/> class.
        /// </summary>
        /// <param name="text">The old text object to copy.</param>
        public TextImplementation(Text text) : base(text) { }
    }

}
