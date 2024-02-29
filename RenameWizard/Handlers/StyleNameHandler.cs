using System.Globalization;
using static System.Net.Mime.MediaTypeNames;

namespace RenameWizard.Handlers
{
    public class StyleNameHandler : IRenameHandler
    {
        private readonly TextStyle textStyle;
        private IRenameHandler handler;

        public StyleNameHandler(TextStyle textStyle)
        {
            this.textStyle = textStyle;
        }
        public string Rename(string name, string ext)
        {
            string fileName = string.Empty;
            switch (this.textStyle)
            {
                case TextStyle.KeepIt:
                    fileName = name;
                    break;

                case TextStyle.CapitalLetter:
                    fileName = this.ToCapitalLetter(name);
                    break;

                case TextStyle.TitleCase:
                    fileName = this.ToTitleCase(name);
                    break;

                case TextStyle.UpperCase:
                    fileName = name.ToUpper();
                    break;

                case TextStyle.LowerCase:
                    fileName = name.ToLower();
                    break;

                default: 
                    fileName = name;
                    break;
            }

            return this.handler?.Rename(fileName, ext) ?? $"{fileName}{ext}";
        }

        public void SetHandler(IRenameHandler handler)
        {
            this.handler = handler;
        }
        private string ToTitleCase(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        private string ToCapitalLetter(string input)
        {
            // Range operator uses ^0 for the last item, ^1 for the previous one, etc.
            return $"{input[..1].ToUpper()}{input[1..^1].ToLower()}";
        }
    }
}
