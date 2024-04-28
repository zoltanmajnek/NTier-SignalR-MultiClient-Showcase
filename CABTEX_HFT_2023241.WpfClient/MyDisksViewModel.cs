using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class MyDisksViewModel : ObservableRecipient
    {
        private string errorMessage;
        private RestService restService;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<MyDisk> MyDisks { get; set; }
        public RestCollection<Software> Softwares { get; set; }

        private MyDisk selectedMyDisk;
        private ObservableCollection<Software> containedSoftwares;
        public ObservableCollection<Software> ContainedSoftwares
        {
            get { return containedSoftwares; }
        }

        public MyDisk SelectedMyDisk
        {
            get
            {
                if (selectedMyDisk != null)
                {                    
                    var strres = restService.Get<Software>("ALotOfNonCrudLogic/DiskDir/" + selectedMyDisk.Id);
                    containedSoftwares.Clear();

                    foreach (var item in strres)
                    {
                        if (item != null) containedSoftwares.Add(item);
                    }
                }
                return selectedMyDisk;
            }

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

        public MyDisksViewModel()
        {
            if (!IsInDesignMode)
            {
                MyDisks = new RestCollection<MyDisk>("http://localhost:53910/", "MyDisk", "hub");
                Softwares = new RestCollection<Software>("http://localhost:53910/", "Software", "hub");
                restService = new RestService("http://localhost:53910/", "DiskDirectory");
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
                containedSoftwares = new ObservableCollection<Software>();
            }
        }
    }
}
