using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using Newtonsoft.Json.Linq;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class TypesWindowViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<DiskType> DiskTypes { get; set; }
        //public RestCollection<DiskDirectory> DiskDirectories { get; set; }

        private DiskType selectedDiskType;

        public DiskType SelectedDiskType
        {
            get { return selectedDiskType; }
            set
            {
                if (value != null)
                {
                    selectedDiskType = new DiskType()
                    {
                        TypeName = value.TypeName,
                        Id = value.Id,
                        VendorName = value.VendorName,
                        Capacity = value.Capacity,                        
                    };
                    OnPropertyChanged();
                    (DeleteDiskTypeCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateDiskTypeCommand { get; set; }

        public ICommand DeleteDiskTypeCommand { get; set; }

        public ICommand UpdateDiskTypeCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public TypesWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                DiskTypes = new RestCollection<DiskType>("http://localhost:53910/", "DiskType", "hub");                
                //DiskDirectories = new RestCollection<DiskDirectory>("http://localhost:53910/", "DiskDirectory", "hub");

                CreateDiskTypeCommand = new RelayCommand(() =>
                {
                    DiskTypes.Add(new DiskType()
                    {
                        TypeName = SelectedDiskType.TypeName,
                        VendorName = SelectedDiskType.VendorName,
                        Capacity = SelectedDiskType.Capacity,                        
                    });
                });

                UpdateDiskTypeCommand = new RelayCommand(() =>
                {
                    try
                    {
                        DiskTypes.Update(SelectedDiskType);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteDiskTypeCommand = new RelayCommand(() =>
                {
                    DiskTypes.Delete(SelectedDiskType.Id);
                },
                () =>
                {
                    return SelectedDiskType != null;
                });
                SelectedDiskType = new DiskType();
            }
        }
    }
}
