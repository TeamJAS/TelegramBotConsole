using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{

    struct BotUpdate
    {
        public string text;
        public long id;
        public string? username;
    }
    public class Program
    {
        static TelegramBotClient Bot = new TelegramBotClient("557430547:AAGlbrSBQkXtxkEYISxuDa_C7C929oaJ1AM");
        static string fileName = "updates.json";
        static List<BotUpdate> botUpdates = new List<BotUpdate>();
        private static Task ErrorHandler(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        private static async Task UpdateHandler(ITelegramBotClient bot, Update update, CancellationToken arg3)
        {
            if (update.Type == UpdateType.Message)
            {
                if (update.Message.Type == MessageType.Text)
                {
                    //write an update
                    var _botUpdate = new BotUpdate
                    {
                        text = update.Message.Text,
                        id = update.Message.Chat.Id,
                        username = update.Message.Chat.Username
                    };
                    if(_botUpdate.text.ToLower() == "/start")
                    {
                        var rmu = new ReplyKeyboardMarkup(new KeyboardButton[][]
                            {
                                new KeyboardButton[]
                                {
                                    new KeyboardButton("OTT")
                                },

                                new KeyboardButton[]
                                {
                                    new KeyboardButton("Interviews")
                                },
                                new KeyboardButton[]
                                {
                                    new KeyboardButton("TSPSC"),
                                    new KeyboardButton("Pocie"),
                                    new KeyboardButton("Constable Exams")
                                }
                            });

                        await Bot.SendTextMessageAsync(_botUpdate.id, $"Hi <b>{_botUpdate.username}</b>, Welcome to TeamJAS service", ParseMode.Html);
                        await Bot.SendTextMessageAsync(_botUpdate.id, $"Please select below Services", replyMarkup: rmu);
                    }
                    else
                    {
                        await Bot.SendTextMessageAsync(_botUpdate.id, $"You have selected following input: {_botUpdate.text}");
                    }
                    
                }
            }
        }
        static async Task Main(string[] args)
        {
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = new UpdateType[]
               {
                    UpdateType.Message,
                    UpdateType.EditedMessage,
               }
            };
            Bot.StartReceiving(UpdateHandler, ErrorHandler, receiverOptions);
            Console.Read();
        }
    }
}
