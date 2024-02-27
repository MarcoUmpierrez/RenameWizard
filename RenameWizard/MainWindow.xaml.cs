using System.IO;
using System.Windows;
using Microsoft.Win32;
using RenameWizard.Models;
using RenameWizard.ViewModels;

namespace RenameWizard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        #region SelectButton
        private void SelectFilesButton_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                if (viewModel.Files.Count > 0)
                {
                    var discardFiles = MessageBox.Show(
                        "Do you want to discard the current selection?",
                        Title,
                        MessageBoxButton.YesNoCancel,
                        MessageBoxImage.Question);

                    if (discardFiles == MessageBoxResult.Yes)
                    {
                        viewModel.Files.Clear();
                    }
                    else
                    {
                        return;
                    }
                }

                OpenFileDialog ofd = new()
                {
                    Title = "Select Files",
                    Multiselect = true,
                    Filter = "All Files|*.*",
                };

                if (ofd.ShowDialog() == true)
                {
                    foreach (var file in ofd.FileNames)
                    {
                        FileInfo fileInfo = new (file);
                        viewModel.Files.Add(new FileModel
                        {
                            Path=fileInfo.Directory!.FullName,
                            Source = fileInfo.Name,
                            Destination = fileInfo.Name,
                        });                    
                    }
                }
            }
        }

        private void SelectFilesButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                viewModel.Help = "Select the files to rename";
            }
        }

        #endregion SelectButton

        #region PreviewButton
        private void PreviewButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                viewModel.Help = "Preview the new file names";
            }
        }
        #endregion PreviewButton

        #region RenameButton
        private void RenameButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                viewModel.Help = "Execute renaming of files";
            }
        }
        #endregion RenameButton

        private void Button_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                viewModel.Help = Title;
            }
        }

        private void RestoreSourceButton_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (DataContext != null && DataContext is MainViewModel viewModel)
            {
                viewModel.Help = "Restores the source name into the destination column for restarting the edition";
            }
        }
    }
}