using System;
using System.Collections.Generic;
using SharpFont;
using Velentr.Font.Internal;

namespace Velentr.Font
{
    /// <summary>
    /// A Font that can be used to draw text to the screen
    /// </summary>
    /// <seealso cref="System.IEquatable{Font}" />
    /// <seealso cref="System.IDisposable" />
    public abstract class Typeface : IEquatable<Typeface>, IDisposable
    {
        /// <summary>
        /// The fonts
        /// </summary>
        private Dictionary<int, Font> Fonts = new Dictionary<int, Font>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Typeface"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="typefaceData">The typeface data.</param>
        /// <param name="preGenerateCharacters">if set to <c>true</c> [pre generate characters].</param>
        /// <param name="charactersToPreGenerate">The characters to pre generate.</param>
        /// <param name="storeTypefaceFileData">if set to <c>true</c> [store typeface file data].</param>
        protected Typeface(string name, byte[] typefaceData, bool preGenerateCharacters, char[] charactersToPreGenerate, bool storeTypefaceFileData)
        {
            Name = name;

            if (storeTypefaceFileData)
            {
                TypefaceData = typefaceData;
            }
            else
            {
                TypefaceData = null;
            }

            PreGenerateCharacters = preGenerateCharacters;
            CharactersToPreGenerate = charactersToPreGenerate;
        }

        /// <summary>
        /// Gets the name of the typeface.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }

        /// <summary>
        /// Gets the typeface data.
        /// </summary>
        /// <value>
        /// The typeface data.
        /// </value>
        public byte[] TypefaceData { get; }

        /// <summary>
        /// Gets or sets a value indicating whether [pre generate characters].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [pre generate characters]; otherwise, <c>false</c>.
        /// </value>
        public bool PreGenerateCharacters { get; set; }

        /// <summary>
        /// Gets or sets the characters to pre generate.
        /// </summary>
        /// <value>
        /// The characters to pre generate.
        /// </value>
        public char[] CharactersToPreGenerate { get; set; }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            DisposeFinal();
            VelentrFont.Core.RemoveTypeface(Name);
        }

        /// <summary>
        /// Disposes the final.
        /// </summary>
        public void DisposeFinal()
        {
            for (var i = 0; i < Fonts.Count; i++)
            {
                Fonts[i].DisposeFinal();
            }
        }

        /// <summary>
        /// Indicates whether the current object is equal to another object of the same type.
        /// </summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>
        /// true if the current object is equal to the <paramref name="other">other</paramref> parameter; otherwise, false.
        /// </returns>
        public bool Equals(Typeface other)
        {
            return !(other is null) && Equals(Name, other.Name);
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object" />, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            return obj != null && (obj is Typeface || obj is TypefaceImplementation) && Equals((Typeface)obj);
        }

        /// <summary>
        /// Gets the font.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="preGenerateCharacters">The pre generate characters.</param>
        /// <param name="charactersToPreGenerate">The characters to pre generate.</param>
        /// <returns>The requested font</returns>
        public Font GetFont(int size, bool? preGenerateCharacters = null, char[] charactersToPreGenerate = null)
        {
            if (!Fonts.TryGetValue(size, out var font))
            {
                var face = GetFace();
                face.SetCharSize(size, size, 0, 0);
                face.SetTransform();
                font = new FontImplementation(size, face, Name);

                preGenerateCharacters = preGenerateCharacters ?? PreGenerateCharacters;
                charactersToPreGenerate = charactersToPreGenerate ?? CharactersToPreGenerate;
                if ((bool)preGenerateCharacters && charactersToPreGenerate != null)
                {
                    font.PreGenerateCharacterGlyphs(charactersToPreGenerate);
                }

                Fonts.Add(size, font);
            }

            return font;
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table. 
        /// </returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Removes the font.
        /// </summary>
        /// <param name="size">The size.</param>
        public void RemoveFont(int size)
        {
            Fonts.Remove(size);
        }

        /// <summary>
        /// Gets the face.
        /// </summary>
        /// <returns>The face</returns>
        private Face GetFace()
        {
            if (TypefaceData == null)
            {
                return VelentrFont.Core.FontLibrary.NewFace(Name, 0);
            }

            return new Face(VelentrFont.Core.FontLibrary, TypefaceData, 0);
        }
    }
}
