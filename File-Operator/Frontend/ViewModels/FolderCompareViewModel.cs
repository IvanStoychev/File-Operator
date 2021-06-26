using Frontend.Utility;
using System.ComponentModel;
using System.Windows.Forms;

namespace Frontend.ViewModels
{
    public class FolderCompareViewModel : INotifyPropertyChanged
    {
        string sourceFolderPath;
        string targetFolderPath;

        public string SourceFolderPath
        {
            get => sourceFolderPath;
            set
            {
                if (sourceFolderPath != value)
                {
                    sourceFolderPath = value;
                    OnPropertyChanged("SourceFolderPath");
                }
            }
        }

        public string TargetFolderPath
        {
            get => targetFolderPath;
            set
            {
                if (targetFolderPath != value)
                {
                    targetFolderPath = value;
                    OnPropertyChanged("TargetFolderPath");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public RelayCommand BrowseSourceFolderCommand { get; }
        public RelayCommand BrowseTargetFolderCommand { get; }
        public RelayCommand CompareFoldersCommand { get; }

        public FolderCompareViewModel()
        {
            BrowseSourceFolderCommand = new RelayCommand(OnBrowseSourceFolderCommand);
            BrowseTargetFolderCommand = new RelayCommand(OnBrowseTargetFolderCommand);
            CompareFoldersCommand = new RelayCommand(OnCompareFoldersCommand, CanCompareFoldersCommand);
        }

        void OnBrowseSourceFolderCommand()
        {
            FolderBrowserDialog sourceFolderBrowser = new();
            sourceFolderBrowser.Description = "desc";
            sourceFolderBrowser.ShowNewFolderButton = false;
            if (sourceFolderBrowser.ShowDialog() == DialogResult.OK)
            {
                SourceFolderPath = sourceFolderBrowser.SelectedPath;
            }
        }

        void OnBrowseTargetFolderCommand()
        {
            throw new System.NotImplementedException();
        }

        void OnCompareFoldersCommand()
        {
            throw new System.NotImplementedException();
        }

        bool CanCompareFoldersCommand()
        {
            throw new System.NotImplementedException();
        }
    }
}
