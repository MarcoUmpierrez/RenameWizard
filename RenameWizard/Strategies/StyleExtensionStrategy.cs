namespace RenameWizard.Strategies
{
    class StyleExtensionStrategy : IRenameStrategy
    {
        private readonly bool replaceExtension;
        private readonly ExtensionStyle style;
        private readonly string str;

        public StyleExtensionStrategy(bool replaceExtension, ExtensionStyle style, string str)
        {
            this.replaceExtension = replaceExtension;
            this.style = style;
            this.str = str;
        }
        public string Rename(string extension)
        {
            string ext = replaceExtension ? str : extension;
            switch (style)
            {
                case ExtensionStyle.UpperCase:
                    ext = ext.ToUpper();
                    break;

                case ExtensionStyle.LowerCase:
                    ext = ext.ToLower();
                    break;
            }

            return ext;
        }
    }
}
