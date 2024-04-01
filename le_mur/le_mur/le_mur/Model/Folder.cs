using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TL;
using Xamarin.Forms;
using le_mur.Helpers;
using System.IO;
using System.Collections.ObjectModel;

namespace le_mur.Model
{
    public class Folder : INotifyPropertyChanged
    {
        int id;
        string name;
        ObservableCollection<ChatInfo> chats;
        bool isShow = true;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }

        public ObservableCollection<ChatInfo> Chats
        {
            get { return chats; }
            set
            {
                chats = value;
                OnPropertyChanged("Chats");
            }
        }

        public bool IsShow
        {
            get { return isShow; }
            set
            {
                isShow = value;
                OnPropertyChanged("IsShow");
            }
        }

        public Folder(int id, string name)
        {
            Id = id;
            Name = name;
            Chats = new ObservableCollection<ChatInfo>();
        }

        public Folder(string name)
        {
            Name = name;
            Chats = new ObservableCollection<ChatInfo>();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
