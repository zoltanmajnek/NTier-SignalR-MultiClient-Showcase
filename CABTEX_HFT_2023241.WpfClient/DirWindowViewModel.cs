using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class DirWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<MyDisk> MyDisks { get; set; }
        public RestCollection<Software> Softwares { get; set; }
        public RestCollection<DiskDirectory> DiskDirectories { get; set; }


        private MyDisk selectedMyDisk;
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
                    (AddToDiskDir as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

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
                    (AddToDiskDir as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand AddToDiskDir { get; set; }
        public ICommand DeleteFromDisk { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public DirWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                MyDisks = new RestCollection<MyDisk>("http://localhost:53910/", "MyDisk", "hub");
                Softwares = new RestCollection<Software>("http://localhost:53910/", "Software", "hub");
                DiskDirectories = new RestCollection<DiskDirectory>("http://localhost:53910/", "DiskDirectory", "hub");

                AddToDiskDir = new RelayCommand(() =>
                {
                    DiskDirectories.Add(new DiskDirectory()
                    {
                        DiskId = SelectedMyDisk.Id,
                        SoftwareId = SelectedSoftware.Id,
                    });

                    Thread.Sleep(300);
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

                DeleteFromDisk = new RelayCommand(() =>
                {
                    var whichOne = DiskDirectories.FirstOrDefault(x => x.DiskId == SelectedMyDisk.Id && x.SoftwareId == SelectedSoftware.Id);
                    if (whichOne != null)
                    {
                        DiskDirectories.Delete(whichOne.Id);
                        Thread.Sleep(300);
                        try
                        {
                            var sendMsgToDiskWindow = MyDisks.First();
                            MyDisks.Update(sendMsgToDiskWindow);
                        }
                        catch (ArgumentException ex)
                        {
                            ErrorMessage = ex.Message;
                        }
                    }

                }, () =>
                {
                    return SelectedMyDisk != null;
                });

                SelectedMyDisk = new MyDisk();
                SelectedSoftware = new Software();
            }
        }
    }
}
