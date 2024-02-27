using System.Collections.ObjectModel;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Input;
using RenameWizard.Models;

namespace RenameWizard.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel()
        {
            Files = [];
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
            int count = StartNumber;
            foreach (FileModel file in Files)
            {
                string name = Path.GetFileNameWithoutExtension(file.Destination);
                string extension = ReplaceExtension
                ? (Extension.Contains('.') ? Extension : $".{Extension}")
                : Path.GetExtension(file.Destination);

                // do any replacement or aditition to the text
                name = AdjustFileName(name);

                // Add a number
                if (AddNumbers)
                {
                    string number = string.Empty;
                    switch (NumerationType)
                    {
                        case NumerationType.Simple:
                            number = $"{count++}";
                            break;

                        case NumerationType.Smart:
                            number = FindSmartNumber(count++, Files.Count);
                            break;

                        case NumerationType.FindInFile:
                            number = FindNumberInFile(file.Source);
                            break;
                    }

                    switch (Order)
                    {
                        case Order.TextNumber:
                            name = $"{number}{name}";
                            break;
                        case Order.NumberText:
                            name = $"{name}{number}";
                            break;
                    }
                }

                // style file name
                name = StyleFileName(name);

                // style extension
                extension = StyleExtension(extension);

                // combine both parts again
                file.Destination = $"{name}{extension}";
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
        #endregion Commands

        private static string FindSmartNumber(int count, int total)
        {
            int positions = total.ToString().Length;
            return count.ToString("D" + positions);
        }
        private static string FindNumberInFile(string input)
        {
            // Use Regex.Match to find the first occurrence
            // of a number in the input text
            string pattern = @"\d+";
            Match match = Regex.Match(input, pattern);
            
            if (match.Success)
            {
                return match.Value;
            }
            else
            {
                return string.Empty;
            }
        }
        private string AdjustFileName(string name)
        {
            string newName = string.Empty;
            try
            {
                if (ReplaceText)
                {
                    newName = name.Replace(SubTextSource, SubTextDestination);
                }

                if (AddText)
                {
                    if (KeepSourceName)
                    {
                        newName += Text;
                    }
                    else
                    {
                        newName = Text;
                    }
                }
            }
            catch (Exception)
            {
                // TODO
            }

            return newName;
        }

        private string StyleFileName(string name)
        {
            try
            {
                switch (TextStyle)
                {
                    case TextStyle.KeepIt:
                        return name;

                    case TextStyle.CapitalLetter:
                        return ToCapitalLetter(name);

                    case TextStyle.TitleCase:
                        return ToTitleCase(name);

                    case TextStyle.UpperCase:
                        return name.ToUpper();

                    case TextStyle.LowerCase:
                        return name.ToLower();
                }
            }
            catch (Exception)
            {
                // TODO
            }

            return string.Empty;
        }

        private string StyleExtension(string extension)
        {
            switch (ExtensionStyle)
            {
                case ExtensionStyle.UpperCase:
                    return extension.ToUpper();

                case ExtensionStyle.LowerCase:
                    return extension.ToLower();
            }

            return string.Empty;
        }

        private static string ToTitleCase(string input)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            return textInfo.ToTitleCase(input.ToLower());
        }

        private static string ToCapitalLetter(string input)
        {
            // Range operator uses ^0 for the last item, ^1 for the previous one, etc.
            return $"{input[..1].ToUpper()}{input[1..^1].ToLower()}";
        }
    }
}
