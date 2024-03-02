using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using RenameWizard.Handlers;
using RenameWizard.Models;

namespace RenameWizard.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Files = new();
            TextStyle = TextStyle.KeepIt;
            ExtensionStyle = ExtensionStyle.LowerCase;
            NumerationType = NumerationType.Simple;
            Order = Order.TextNumber;
            StartNumber = 0;
        }

        #region Properties
        public string Help 
        {
            get => Get<string>();
            set => Set(value);
        }

        public FileModel SelectedRow
        {
            get => Get<FileModel>();
            set => Set(value);
        }

        public ObservableCollection<FileModel> Files
        {
            get => Get<ObservableCollection<FileModel>>();
            set => Set(value);
        }

        public bool KeepSourceName
        {
            get => Get<bool>();
            set => Set(value);
        }

        public TextStyle TextStyle
        {
            get => Get<TextStyle>();
            set => Set(value);
        }

        public ExtensionStyle ExtensionStyle
        {
            get => Get<ExtensionStyle>();
            set => Set(value);
        }
        public bool ReplaceExtension
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string Extension
        {
            get => Get<string>();
            set => Set(value);
        }
        public bool AddText
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string Text
        {
            get => Get<string>();
            set => Set(value);
        }
        public bool AddNumbers
        {
            get => Get<bool>();
            set => Set(value);
        }
        public NumerationType NumerationType
        {
            get => Get<NumerationType>();
            set => Set(value);
        }
        public int StartNumber
        {
            get => Get<int>();
            set => Set(value);
        }
        public Order Order
        {
            get => Get<Order>();
            set => Set(value);
        }
        public bool ReplaceText
        {
            get => Get<bool>();
            set => Set(value);
        }
        public string SubTextSource
        {
            get => Get<string>();
            set => Set(value);
        }
        public string SubTextDestination
        {
            get => Get<string>();
            set => Set(value);
        }
        #endregion Properties

        #region Commands
        public ICommand PreviewCommand => new RelayCommand((obj) =>
        {
            IRenameHandler replaceHandler = new ReplaceTextHandler(this.ReplaceText, this.SubTextSource, this.SubTextDestination);
            IRenameHandler addNameHandler = new AddTextHandler(this.KeepSourceName, this.AddText, this.Text);
            IRenameHandler numberHandler = new NumberHandler(this.AddNumbers, this.NumerationType, this.Order, this.Files.Count, this.StartNumber);
            IRenameHandler textStyleHandler = new StyleNameHandler(this.TextStyle);
            IRenameHandler extensionHandler = new StyleExtensionHandler(this.ReplaceExtension, this.ExtensionStyle, this.Extension);

            replaceHandler.SetHandler(addNameHandler);
            addNameHandler.SetHandler(numberHandler);
            numberHandler.SetHandler(textStyleHandler);
            textStyleHandler.SetHandler(extensionHandler);

            foreach (FileModel file in Files)
            {
                string name = Path.GetFileNameWithoutExtension(file.Destination);
                string ext = Path.GetExtension(file.Destination);
                file.Destination = replaceHandler.Rename(name, ext);
            }
        },
        (obj) => Files.Count > 0);

        public ICommand RenameCommand => new RelayCommand((obj) =>
        {
            foreach (FileModel file in Files)
            {
                File.Move(Path.Combine(file.Path, file.Source), Path.Combine(file.Path, file.Destination));
                file.Source = file.Destination;
            }
        },
        (obj) => Files.Count > 0);

        public ICommand RestoreSourceCommand => new RelayCommand((obj) => 
        { 
            foreach(FileModel file in Files)
            {
                file.Destination = file.Source;
            }
        },
        (obj) => Files.Count > 0);

        public ICommand MoveRowUpCommand => new RelayCommand((obj) =>
        {
            int index = Files.IndexOf(SelectedRow);
            if (index > 0)
            {
                Files.Move(index, index - 1);
            }
        },
        (obj) => Files.Count > 0 && SelectedRow != null);

        public ICommand MoveRowDownCommand => new RelayCommand((obj) =>
        {
            int index = Files.IndexOf(SelectedRow);
            if (index >= 0 && index < Files.Count - 1)
            {
                Files.Move(index, index + 1);
            }
        },
        (obj) => Files.Count > 0 && SelectedRow != null);
        #endregion Commands
    }
}
