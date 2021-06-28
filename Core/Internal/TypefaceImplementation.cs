namespace Velentr.Font.Internal
{
    internal class TypefaceImplementation : Typeface
    {
        public TypefaceImplementation(string name, byte[] typefaceData, bool preGenerateCharacters, char[] charactersToPreGenerate, bool storeTypefaceFileData, FontManager manager) : base(name, typefaceData, preGenerateCharacters, charactersToPreGenerate, storeTypefaceFileData, manager)
        {

        }
    }
}
