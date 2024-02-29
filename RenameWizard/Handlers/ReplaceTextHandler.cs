namespace RenameWizard.Handlers
{
    class ReplaceTextHandler : IRenameHandler
    {
        private readonly bool doReplacement;
        private readonly string str1;
        private readonly string str2;
        private IRenameHandler handler;

        public ReplaceTextHandler(bool doReplacement, string str1, string str2)
        {
            this.doReplacement = doReplacement;
            this.str1 = str1;
            this.str2 = str2;
        }
        public string Rename(string name, string ext)
        {
            string fileName = name;
            if (this.doReplacement)
            {
                fileName = name.Replace(this.str1, this.str2);
            }

            return this.handler?.Rename(fileName, ext) ?? $"{fileName}{ext}";
        }

        public void SetHandler(IRenameHandler handler)
        {
            this.handler = handler;
        }
    }
}
