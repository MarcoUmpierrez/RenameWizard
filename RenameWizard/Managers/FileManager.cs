using System.IO;
using RenameWizard.Strategies;

namespace RenameWizard.Managers
{
    public class FileManager
    {
        readonly IRenameStrategy replaceStrategy;
        readonly IRenameStrategy addNameStrategy;
        readonly IRenameStrategy numberStrategy;
        readonly IRenameStrategy textStyleStrategy;
        readonly IRenameStrategy extensionStrategy;
        public FileManager(
            bool doReplacement,
            bool keepFileNames,
            bool addText,
            bool addNumber,
            bool replaceExtension,
            string str1,
            string str2,
            string text,
            string extension,
            int total,
            int count,
            NumerationType type,
            Order order,
            TextStyle textStyle,
            ExtensionStyle extensionStyle
            )
        {
            replaceStrategy = new ReplaceTextStrategy(doReplacement, str1, str2);
            addNameStrategy = new AddTextStrategy(keepFileNames, addText, text);
            numberStrategy = new NumberStrategy(addNumber, type, order, total, count);
            textStyleStrategy = new StyleNameStrategy(textStyle);
            extensionStrategy = new StyleExtensionStrategy(replaceExtension, extensionStyle, extension);
        }
        public string Rename(string fileName)
        {
            string name = Path.GetFileNameWithoutExtension(fileName);
            string ext = Path.GetExtension(fileName);
            name = replaceStrategy.Rename(name);
            name = addNameStrategy.Rename(name);
            name = numberStrategy.Rename(name);
            name = textStyleStrategy.Rename(name);
            ext = extensionStrategy.Rename(ext);
            return $"{name}{ext}";
        }
    }
}
