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
using System.Xml.Linq;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class NonCrudWindowViewModel : ObservableRecipient
    {
        private string errorMessage;
        private RestService restService;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        private ObservableCollection<string> result;

        public ObservableCollection<string> Result
        {
            get { return result; }
        }

        private string inputText;

        public string InputText
        {
            get { return inputText; }
            set
            {
                if (inputText != value)
                {
                    inputText = value;
                    OnPropertyChanged("InputText");
                }
            }
        }

        public ICommand AddressSearch { get; set; }

        public ICommand GenreSearch { get; set; }

        public ICommand SwSearch { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public NonCrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                restService = new RestService("http://localhost:53910/", "DiskDirectory");

                AddressSearch = new RelayCommand(() =>
                {
                    result.Clear();
                    result.Add($"{inputText} cím keresés eredménye:");
                    if (!string.IsNullOrEmpty(inputText))
                    {
                        var strres = restService.Get<string>("ALotOfNonCrudLogic/WhoLiveInMyStreet/" + inputText);
                        foreach (var item in strres)
                        {
                            result.Add(item);
                        }
                    }
                    if (result.Count < 2) result.Add("Nincs ilyen cím.");
                });

                GenreSearch = new RelayCommand(() =>
                {
                    result.Clear();
                    result.Add($"{inputText} típus keresés eredménye:");
                    if (!string.IsNullOrEmpty(inputText))
                    {
                        var strres = restService.Get<string>("ALotOfNonCrudLogic/Genre/" + inputText);
                        foreach (var item in strres)
                        {
                            result.Add(item);
                        }
                    }
                    if (result.Count < 2) result.Add("No such genre.");
                });

                SwSearch = new RelayCommand(() =>
                {
                    result.Clear();
                    result.Add($"{inputText} kinél és melyik lemezen van:");
                    if (!string.IsNullOrEmpty(inputText))
                    {
                        var strres = restService.Get<Friend>("ALotOfNonCrudLogic/WhoHasThisSoftware/" + inputText);
                        foreach (var item in strres) { result.Add("Kinél van: " + item.Name); }
                        var diskres = restService.Get<MyDisk>("/ALotOfNonCrudLogic/ContainsaGivenSoftware/" + inputText);
                        foreach (var item in diskres) { result.Add("Lemez címke: " + item.Label + " Id: "+item.Id); }
                    }
                    if (result.Count < 2) result.Add("Nincs ilyen szoftver.");
                });
                result = new ObservableCollection<string>();
            }
        }
    }
}
