using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpFont;

namespace Velentr.Font
{
    /// <summary>
    /// Various constants defined for the font system.
    /// </summary>
    internal sealed class Constants
    {
        /// <summary>
        /// The default hidef texture size
        /// </summary>
        public const int DEFAULT_HIDEF_TEXTURE_SIZE = 4096;

        /// <summary>
        /// The default reach texture size
        /// </summary>
        public const int DEFAULT_REACH_TEXTURE_SIZE = 2048;

        /// <summary>
        /// The default cache surface format
        /// </summary>
        public const SurfaceFormat DEFAULT_CACHE_SURFACE_FORMAT = SurfaceFormat.Bgra4444;

        /// <summary>
        /// The glyph bitmap origin
        /// </summary>
        public static FTVector26Dot6 GlyphBitmapOrigin = new FTVector26Dot6(0, 0);

        /// <summary>
        /// The default character list
        /// </summary>
        private char[] _defaultCharacterList;

        /// <summary>
        /// The default characters
        /// </summary>
        public string DefaultCharacters = " AaBbCcDdEeFfGgHhIiJjKkLlMmNnOoPpQqRrSsTtUuVvWwXxYyZz0123456789~`!@#$%^&*()_+-=[]\\{}|;':\",./<>?。？　【】｛｝、｜《》（）…￥";

        /// <summary>
        /// The default load flags
        /// </summary>
        public LoadFlags DefaultLoadFlags = LoadFlags.Default;

        /// <summary>
        /// The default load target
        /// </summary>
        public LoadTarget DefaultLoadTarget = LoadTarget.Normal;

        /// <summary>
        /// The default render mode
        /// </summary>
        public RenderMode DefaultRenderMode = RenderMode.Normal;

        /// <summary>
        /// The default spaces in a tab
        /// </summary>
        public int DefaultSpacesInTab = 4;

        /// <summary>
        /// The kerning sanity multiplier
        /// </summary>
        public int KerningSanityMultiplier = 5;

        /// <summary>
        /// Initializes the <see cref="Constants"/> class.
        /// </summary>
        static Constants() { }

        /// <summary>
        /// Prevents a default instance of the <see cref="Constants"/> class from being created.
        /// </summary>
        private Constants() { }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        /// <value>
        /// The settings.
        /// </value>
        public static Constants Settings { get; } = new Constants();

        /// <summary>
        /// Gets or sets the default character list.
        /// </summary>
        /// <value>
        /// The default character list.
        /// </value>
        public char[] DefaultCharacterList
        {
            get => _defaultCharacterList ?? (_defaultCharacterList = DefaultCharacters.ToCharArray());
            set
            {
                _defaultCharacterList = value;
                DefaultCharacters = new string(_defaultCharacterList);
            }
        }

        /// <summary>
        /// Whether to store the font's file data or not.
        /// Increases memory usage, but allows the program to not have to re-read the font file when generating the same font at different sizes.
        /// </summary>
        /// <value>
        /// The default character list.
        /// </value>
        public bool StoreFontFileData { get; set; } = true;

        /// <summary>
        /// A full arc of a circle
        /// </summary>
        public const float TWO_PI = (float)(2 * Math.PI);

        /// <summary>
        /// The maximum size of the TextCache object on a particular font
        /// </summary>
        public int MaxTextCacheSize = 16;

        /// <summary>
        ///     The color mapping.
        /// </summary>
        private Dictionary<string, Color> _colorMapping = null;

        /// <summary>
        ///     Gets the color mapping.
        /// </summary>
        ///
        /// <value>
        ///     The color mapping.
        /// </value>
        public Dictionary<string, Color> ColorMapping
        {
            get
            {
                if (_colorMapping == null)
                {
                    _colorMapping = new Dictionary<string, Color>();
                    var props = typeof(Color).GetProperties();

                    foreach (var color in props)
                    {
                        switch (color.Name)
                        {
                            case "PackedValue":
                            case "B":
                            case "G":
                            case "R":
                            case "A":
                                break;
                            default:
                                _colorMapping[color.Name.ToUpperInvariant()] = (Color)color.GetValue(color);
                                break;
                        }
                    }
                }

                return _colorMapping;
            }
        }

    }
}
