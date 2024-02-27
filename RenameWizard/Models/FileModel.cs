namespace RenameWizard.Models
{
    public class FileModel : BaseViewModel
    {
        public string Path { get; set; }
        public string Source
        { 
            get => Get<string>();
            set => Set(value);
        }
        public string Destination
        {
            get => Get<string>();
            set => Set(value);
        }
    }
}
