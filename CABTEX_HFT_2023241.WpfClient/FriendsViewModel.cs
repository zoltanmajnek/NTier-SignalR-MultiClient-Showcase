using CABTEX_HFT_2023241.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace CABTEX_HFT_2023241.WpfClient
{
    public class FriendsViewModel : ObservableRecipient
    {
        private string errorMessage;

        public string ErrorMessage
        {
            get { return errorMessage; }
            set { SetProperty(ref errorMessage, value); }
        }

        public RestCollection<Friend> Friends { get; set; }
        //public RestCollection<DiskType> Types { get; set; }
        //public RestCollection<DiskDirectory> DiskDirectories { get; set; }

        private Friend selectedFriend;

        public Friend SelectedFriend
        {
            get { return selectedFriend; }
            set
            {
                if (value != null)
                {
                    selectedFriend = new Friend()
                    {
                        Name = value.Name,
                        Id = value.Id
                    };
                    OnPropertyChanged();
                    (DeleteFriendCommand as RelayCommand).NotifyCanExecuteChanged();
                }
            }
        }

        public ICommand CreateFriendCommand { get; set; }

        public ICommand DeleteFriendCommand { get; set; }

        public ICommand UpdateFriendCommand { get; set; }

        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }

        public FriendsViewModel()
        {
            if (!IsInDesignMode)
            {
                Friends = new RestCollection<Friend>("http://localhost:53910/", "Friend", "hub");
                //DiskDirectories = new RestCollection<DiskDirectory>("http://localhost:53910/", "DiskDirectory", "hub");
                CreateFriendCommand = new RelayCommand(() =>
                {
                    Friends.Add(new Friend()
                    {
                        Name = SelectedFriend.Name,
                    });
                });

                UpdateFriendCommand = new RelayCommand(() =>
                {
                    try
                    {
                        Friends.Update(SelectedFriend);
                    }
                    catch (ArgumentException ex)
                    {
                        ErrorMessage = ex.Message;
                    }

                });

                DeleteFriendCommand = new RelayCommand(() =>
                {
                    Friends.Delete(SelectedFriend.Id);
                },
                () =>
                {
                    return SelectedFriend != null;
                });
                SelectedFriend = new Friend();
            }
        }
    }
}
