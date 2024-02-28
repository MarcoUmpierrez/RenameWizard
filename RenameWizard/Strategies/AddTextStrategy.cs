namespace RenameWizard.Strategies
{
    public class AddTextStrategy : IRenameStrategy
    {
        private readonly bool keepFileName;
        private readonly bool addText;
        private readonly string str;

        public AddTextStrategy(bool keepFileName, bool addText, string str)
        {
            this.keepFileName = keepFileName;
            this.addText = addText;
            this.str = str;
        }

        public string Rename(string fileName)
        {
            if (addText)
            {
                if (keepFileName)
                {
                    return fileName + str;
                }
                else
                {
                    return str;
                }
            }

            return fileName;
        }
    }
}
