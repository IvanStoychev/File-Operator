using Backend;
using Frontend.Utility;
using System;
using System.ComponentModel;
using System.Text;
using IvanStoychev.StringExtensions;
using System.Windows.Forms;
using Shell32;

namespace Frontend.ViewModels
{
    public class FolderCompareViewModel : INotifyPropertyChanged
    {
        string sourceFolderPath;
        string targetFolderPath;
        string sourceResults;
        string targetResults;
        readonly Shell shell = new();
        readonly FolderBrowserDialog sourceFolderBrowser = new();
        readonly FolderBrowserDialog targetFolderBrowser = new();

        public int Hwnd { get; private set; }

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

        public string SourceResults
        {
            get => sourceResults;
            set
            {
                if (sourceResults != value)
                {
                    sourceResults = value;
                    OnPropertyChanged("SourceResults");
                }
            }
        }

        public string TargetResults
        {
            get => targetResults;
            set
            {
                if (targetResults != value)
                {
                    targetResults = value;
                    OnPropertyChanged("TargetResults");
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

            sourceFolderBrowser.Description = "Select source folder";
            sourceFolderBrowser.UseDescriptionForTitle = true;
            sourceFolderBrowser.ShowNewFolderButton = false;
            targetFolderBrowser.Description = "Select target folder";
            targetFolderBrowser.UseDescriptionForTitle = true;
            targetFolderBrowser.ShowNewFolderButton = false;
        }

        void OnBrowseSourceFolderCommand()
        {
            if (sourceFolderBrowser.ShowDialog() == DialogResult.OK)
                SourceFolderPath = sourceFolderBrowser.SelectedPath;

            //SourceFolderPath = ShellBrowseForFolder()?.Path;
        }

        void OnBrowseTargetFolderCommand()
        {
            if (targetFolderBrowser.ShowDialog() == DialogResult.OK)
                TargetFolderPath = targetFolderBrowser.SelectedPath;

            //TargetFolderPath = ShellBrowseForFolder()?.Path;
        }

        void OnCompareFoldersCommand()
        {
            var diffData = new LogisEngine().CompareFilesInFolders(SourceFolderPath, TargetFolderPath);

            string sourceResults = diffData.MissingDirectoriesText + Environment.NewLine + diffData.MissingFilesText;
            string targetResults = diffData.ExtraDirectoriesText + Environment.NewLine + diffData.ExtraFilesText;
            SourceResults = sourceResults.TrimStart(Environment.NewLine).TrimEnd(Environment.NewLine);
            TargetResults = targetResults.TrimStart(Environment.NewLine).TrimEnd(Environment.NewLine);
        }

        bool CanCompareFoldersCommand()
        {
            return true;
        }

        /// <summary>
        /// Uses Shell32 to open a "folder browser" dialog and returns the selected folder.
        /// </summary>
        /// <returns></returns>
        FolderItem ShellBrowseForFolder()
        {
            Folder folder = shell.BrowseForFolder(Hwnd, "", 0, 0);
            if (folder != null)
            {
                FolderItem fi = (folder as Folder3).Self;
                return fi;
            }

            return null;
        }
    }
}
