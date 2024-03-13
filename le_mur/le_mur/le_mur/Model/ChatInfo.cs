﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TL;
using Xamarin.Forms;
using le_mur.Helpers;

namespace le_mur.Model
{
    public class ChatInfo : INotifyPropertyChanged
    {
        InputPeer id;
        string title;
        ImageSource image;

        public InputPeer Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                OnPropertyChanged("Title");
            }
        }

        public ImageSource Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public ChatInfo(InputPeer id, string title, byte[] image)
        {
            Id = id;
            Title = title;
            Image = ImageConverterHelper.ConvertByteArrayToImageSource(image);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}