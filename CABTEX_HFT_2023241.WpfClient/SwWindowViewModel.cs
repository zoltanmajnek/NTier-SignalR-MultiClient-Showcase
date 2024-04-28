using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class SwWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Software> Softwares { get; set; }
        public RestCollection<MyDisk> MyDisks { get; set; }
        //public RestCollection<DiskType> Types { get; set; }
        //public RestCollection<DiskDirectory> DiskDirectories { get; set; }

        private Software selectedSoftware;

        public Software SelectedSoftware
        {
            get { return selectedSoftware; }
            set
            {
                if (value != null)
                {
                    selectedSoftware = new Software()
                    {
                        Name = value.Name,
                        Id = value.Id,
                        Creator = value.Creator,
                        Genre = value.Genre,
                        Size = value.Size,
                    };
                    OnPropertyChanged();
                    (DeleteSoftwareCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateSoftwareCommand { get; set; }

        public ICommand DeleteSoftwareCommand { get; set; }

        public ICommand UpdateSoftwareCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public SwWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                Softwares = new RestCollection<Software>("http://localhost:53910/", "Software", "hub");
                MyDisks = new RestCollection<MyDisk>("http://localhost:53910/", "MyDisk", "hub");

                //DiskDirectories = new RestCollection<DiskDirectory>("http://localhost:53910/", "DiskDirectory", "hub");
                CreateSoftwareCommand = new RelayCommand(() =>
                {
                    Softwares.Add(new Software()
                    {
                        Name = SelectedSoftware.Name,
                        Creator = SelectedSoftware.Creator,
                        Genre = SelectedSoftware.Genre,
                        Size = SelectedSoftware.Size,
                    });
                });

                UpdateSoftwareCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Softwares.Update(SelectedSoftware);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                    
                    try
                    {
                        var sendMsgToDiskWindow = MyDisks.First();
                        MyDisks.Update(sendMsgToDiskWindow);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }
                });

                DeleteSoftwareCommand = new RelayCommand(() =>
                {
                    Softwares.Delete(SelectedSoftware.Id);
                },
                () =>
                {
                    return SelectedSoftware != null;
                });

                SelectedSoftware = new Software();
            }
        }
    }
}
