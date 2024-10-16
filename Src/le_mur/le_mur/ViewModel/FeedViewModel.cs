using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using le_mur.Model;
using TL;
using le_mur.NetworkCalling;
using System.Collections.Generic;
using System.Linq;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.CommunityToolkit;//.Core;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using le_mur.Consts;
using le_mur.View;
using Xamarin.Forms.Internals;
using le_mur.Helpers;

namespace le_mur.ViewModel
{
    public class FeedViewModel : BaseViewModel
    {
        public INavigation Navigation { get; set; }
        public Command FavouritesCommand { get; }
        public Command LikeCommand { get; }
        public Command CommentCommand { get; }
        public Command LoadVideoCommand { get; }
        public Command ToChannelsCommand { get; }


        private ObservableCollection<MessageInfo> messages;
        public ObservableCollection<MessageInfo> Messages
        {
            get { return messages; }
            set
            {
                if (messages != value)
                {
                    messages = value;
                    OnPropertyChanged("Messages");
                }
            }
        }

        public FeedViewModel()
        {
            Messages = new ObservableCollection<MessageInfo>();

            LikeCommand = new Command(OnLikeCommand);
            CommentCommand = new Command(OnCommentCommand);
            FavouritesCommand = new Command(OnFavouritesCommand);
            LoadVideoCommand = new Command(OnLoadVideoCommand);
            ToChannelsCommand = new Command(onChannelsCommand);

            GetMessages(0);
        }

        public async void GetMessages(int id)
        {
            // todo видимые каналы  
            var chats = await TelegramApi.GetChatsInfo();
            var tmp = new List<ChatInfo> { chats[0], chats[1], chats[2], chats[3], chats[4], chats[5], chats[6], chats[7], chats[8] };
            var wallInfo = await TelegramApi.GetCustomWall(new CustomWallInfo(tmp));

            List<MessageInfo> messages = wallInfo.Messages.ToList();
            for (int i = 0; i < messages.Count; i++)
            {
                if (messages[i].Video.Count > 0)
                    messages[i].HeightVideo = 300;
                var channel = wallInfo.ChatInfos.Find(x => x.Item1.Id.ID == messages[i].ChatId.ID);
                messages[i].GroupName = channel.Item1.Title;
                messages[i].GroupImage = channel.Item1.Image;
                var allMessages = Messages.ToList();
                allMessages.AddRange(messages);
                Messages = new ObservableCollection<MessageInfo>(allMessages);
            }
        }

        private void OnFavouritesCommand(object obj)
        {

        }

        public void OnCommentCommand(object obj)
        {

        }

        public void OnLikeCommand(object obj)
        {
            Messages.Where(m => m.Id == (int)obj).First().IsLiked = !Messages.Where(m => m.Id == (int)obj).First().IsLiked;
        }

        public async void OnLoadVideoCommand(object obj)
        {
            foreach (var mess in Messages)
                foreach (var media in mess.Video)
                    if (media.Filename == obj.ToString())
                    {
                        await media.GetFile();
                        return;
                    }
        }

        private async void onChannelsCommand()
        {
            await Navigation.PushAsync(new ChannelsPage());
        }

        private void UpdateTimers()
        {
            var today = DateTime.Today;

            //todo получение всех таймеров из бд
            var timersList = new List<Model.Timer>() { new Model.Timer(0), new Model.Timer(1) };


            for (var index = 0; index < timersList.Count; index++)
            {
                var t = timersList[index];
                var needToChangeDate = t.Dates.Where(date => date.HasValue && date < DateTime.Now).ToList();
                for (var i = 0; i < needToChangeDate.Count; i++)
                    switch (t.Repeat)
                    {
                        case Timer.RepeatStatus.Not:
                            needToChangeDate[i] = null;
                            break;
                        case Timer.RepeatStatus.Weekly:
                            if (needToChangeDate[i].HasValue)
                                needToChangeDate[i] = ((DateTime)needToChangeDate[i]).MoveByWeek();
                            break;
                        case Timer.RepeatStatus.Fortnightly:
                            if (needToChangeDate[i].HasValue)
                                needToChangeDate[i] = ((DateTime)needToChangeDate[i]).MoveByTwoWeeks();
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                timersList[index].Dates = needToChangeDate.OrderBy(x => x).ThenByDescending(x => x.HasValue).ToArray();
            }
        }
    }
}
