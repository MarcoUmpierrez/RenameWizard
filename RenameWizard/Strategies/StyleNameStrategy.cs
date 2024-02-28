using System.Globalization;

namespace RenameWizard.Strategies
{
    public class StyleNameStrategy : IRenameStrategy
    {
        private readonly TextStyle textStyle;

        public StyleNameStrategy(TextStyle textStyle)
        {
            this.textStyle = textStyle;
        }
        public string Rename(string fileName)
        {
            switch (textStyle)
            {
                case TextStyle.KeepIt:
                    return fileName;

                case TextStyle.CapitalLetter:
                    return ToCapitalLetter(fileName);

                case TextStyle.TitleCase:
                    return ToTitleCase(fileName);

                case TextStyle.UpperCase:
                    return fileName.ToUpper();

                case TextStyle.LowerCase:
                    return fileName.ToLower();

                default: 
                    return fileName;
            }


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
