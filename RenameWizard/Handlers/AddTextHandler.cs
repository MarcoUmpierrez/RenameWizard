namespace RenameWizard.Handlers
{
    public class AddTextHandler : IRenameHandler
    {
        private readonly bool keepFileName;
        private readonly bool addText;
        private readonly string str;
        private IRenameHandler handler;

        public AddTextHandler(bool keepFileName, bool addText, string str)
        {
            this.keepFileName = keepFileName;
            this.addText = addText;
            this.str = str;
        }

        public string Rename(string name, string ext)
        {
            string fileName = name;
            if (this.addText)
            {
                if (this.keepFileName)
                {
                    fileName = name + str;
                }
                else
                {
                    fileName = str;
                }
            }

            return this.handler?.Rename(fileName, ext) ?? $"{fileName}{ext}";
        }

        public void SetHandler(IRenameHandler handler)
        {
            this.handler = handler;
        }
    }
}
