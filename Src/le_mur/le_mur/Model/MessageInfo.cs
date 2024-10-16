﻿using le_mur.Helpers;
using le_mur.NetworkCalling.MediaTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using TL;
using Xamarin.Forms;
using System.Collections.ObjectModel;

namespace le_mur.Model
{
    public class MessageInfo : INotifyPropertyChanged
    {
        int id;
        string text;
        List<Photo> imagesLinks;
        ObservableCollection<VideoInfo> video;
        ObservableCollection<ImageInfo> images;
        long groupId;
        int height;
        int heightVideo;
        DateTime date;
        string groupName;
        ImageSource groupImage;
        bool isLiked = false;

        public InputPeer ChatId;

        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }

        public string Text
        {
            get { return text; }
            set
            {
                text = value;
                OnPropertyChanged("Text");
            }
        }

        public List<Photo> ImagesLinks
        {
            get { return imagesLinks; }
            set
            {
                imagesLinks = value;
                OnPropertyChanged("ImagesLinks");
            }
        }

        public ObservableCollection<VideoInfo> Video
        {
            get { return video; }
            set
            {
                video = value;
                OnPropertyChanged("Video");
            }
        }

        public ObservableCollection<ImageInfo> Images
        {
            get { return images; }
            set
            {
                images = value;
                OnPropertyChanged("Images");
            }
        }

        public int Height
        {
            get { return height; }
            set
            {
                if (height != value)
                {
                    height = value;
                    OnPropertyChanged("Height");
                }
            }
        }

        public int HeightVideo
        {
            get { return heightVideo; }
            set
            {
                if (heightVideo != value)
                {
                    heightVideo = value;
                    OnPropertyChanged("HeightVideo");
                }
            }
        }

        public DateTime Date
        {
            get { return date; }
            set
            {
                if (date != value)
                {
                    date = value;
                    OnPropertyChanged("Date");
                }
            }
        }

        public long GroupId
        {
            get { return groupId; }
            set
            {
                groupId = value;
                OnPropertyChanged("GroupId");
            }
        }
        public string GroupName
        {
            get { return groupName; }
            set
            {
                groupName = value;
                OnPropertyChanged("GroupName");
            }
        }

        public ImageSource GroupImage
        {
            get { return groupImage; }
            set
            {
                groupImage = value;
                OnPropertyChanged("GroupImage");
            }
        }

        public bool IsLiked
        {
            get { return isLiked; }
            set
            {
                isLiked = value;
                OnPropertyChanged("IsLiked");
            }
        }

        public MessageInfo(int id, string text, long groupId, DateTime date, InputPeer chatId)
        {
            Id = id;
            Text = text;
            GroupId = groupId;
            ImagesLinks = new List<Photo>();
            Video = new ObservableCollection<VideoInfo>();
            Images = new ObservableCollection<ImageInfo>();
            Height = 0;
            HeightVideo = 0;
            Date = date;
            ChatId = chatId;
        }

        public MessageInfo(int id, string text, long groupId, DateTime date, InputPeer chatId, string groupName, ImageSource groupImage) : this(id, text, groupId, date, chatId)
        {
            GroupImage = groupImage;
            GroupName = groupName;
        }

        public void AddImage(byte[] image)
        {
            Images.Add(new ImageInfo(ImageSource.FromStream(() => new MemoryStream(image))));
            if (ImageHelper.GetHeightOfImage(image) > Height)
                Height = ImageHelper.GetHeightOfImage(image);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
