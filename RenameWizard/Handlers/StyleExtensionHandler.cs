namespace RenameWizard.Handlers
{
    class StyleExtensionHandler : IRenameHandler
    {
        private readonly bool replaceExtension;
        private readonly ExtensionStyle style;
        private readonly string ext;
        private IRenameHandler handler;

        public StyleExtensionHandler(bool replaceExtension, ExtensionStyle style, string ext)
        {
            this.replaceExtension = replaceExtension;
            this.style = style;
            this.ext = ext.Contains('.') ? ext : $".{ext}";
        }
        public string Rename(string name, string ext)
        {
            string extension = this.replaceExtension ? this.ext : ext;
            switch (this.style)
            {
                case ExtensionStyle.UpperCase:
                    extension = extension.ToUpper();
                    break;

                case ExtensionStyle.LowerCase:
                    extension = extension.ToLower();
                    break;
            }

            return this.handler?.Rename(name, extension) ?? $"{name}{extension}";
        }

        public void SetHandler(IRenameHandler handler)
        {
            this.handler = handler;
        }
    }
}
