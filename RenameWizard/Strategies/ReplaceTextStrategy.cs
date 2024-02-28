namespace RenameWizard.Strategies
{
    class ReplaceTextStrategy : IRenameStrategy
    {
        private readonly bool doReplacement;
        private readonly string str1;
        private readonly string str2;

        public ReplaceTextStrategy(bool doReplacement, string str1, string str2)
        {
            this.doReplacement = doReplacement;
            this.str1 = str1;
            this.str2 = str2;
        }
        public string Rename(string fileName)
        {
            if (doReplacement)
            {
                return fileName.Replace(str1, str2);
            }

            return fileName;
        }
    }
}
