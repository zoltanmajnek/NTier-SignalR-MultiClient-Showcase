using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class MainWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<MyDisk> MyDisks { get; set; }
        //public RestCollection<Friend> Friends { get; set; }
        //public RestCollection<DiskType> Types { get; set; }
        //public RestCollection<DiskDirectory> DiskDirectories { get; set; }

        private MyDisk selectedMyDisk;

        public MyDisk SelectedMyDisk
        {
            get { return selectedMyDisk; }
            set
            {
                if (value != null)
                {
                    selectedMyDisk = new MyDisk()
                    {
                        Label = value.Label,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteMyDiskCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateMyDiskCommand { get; set; }

        public ICommand DeleteMyDiskCommand { get; set; }

        public ICommand UpdateMyDiskCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public MainWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                MyDisks = new RestCollection<MyDisk>("http://localhost:53910/", "MyDisk", "hub");
                //DiskDirectories = new RestCollection<DiskDirectory>("http://localhost:53910/", "DiskDirectory", "hub");
                CreateMyDiskCommand = new RelayCommand(() =>
                {
                    MyDisks.Add(new MyDisk()
                    {
                        Label = SelectedMyDisk.Label,
                    });
                });

                UpdateMyDiskCommand = new RelayCommand(() =>
                {
                    try
                    {
                        MyDisks.Update(SelectedMyDisk);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteMyDiskCommand = new RelayCommand(() =>
                {
                    MyDisks.Delete(SelectedMyDisk.Id);
                },
                () =>
                {
                    return SelectedMyDisk != null;
                });
                SelectedMyDisk = new MyDisk();
            }

        }
    }
}
