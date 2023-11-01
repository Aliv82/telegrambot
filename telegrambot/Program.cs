using Bot_project.Model;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Types;




class program
{
    static TelegramBotClient mybot;
    static int AAUpDate;
    static int lastHeroRequestId = 0;
    static string userParam = "";

    static async Task Main()
    {

        //TOKEN ==========================================================================
        IConfigurationBuilder configBuilder = new ConfigurationBuilder();
        configBuilder.AddUserSecrets<program>();
        IConfiguration config = configBuilder.Build();
        string TOKEN = config["Token"];
        mybot = new TelegramBotClient("6969152310:AAHsuU4f3BEYhVS2UVuYZwlO-w6PyXsS36Y");
        //TOKEN ==========================================================================

        Console.WriteLine("start");
        //start payam ===========================================================================
        while (true)
        {
            var updates = await mybot.GetUpdatesAsync(AAUpDate);

            foreach (var update in updates)
            {
                AAUpDate = update.Id + 1;

                var message = update.Message != null ? update.Message : null;
                var text = update.Message.Text != null ? update.Message.Text : null;
                var chatid = update.Message.Chat.Id;
                var messageid = update.Message.MessageId;

                if (message != null && text != null)
                {
                    if (text.ToLower().Equals("/start"))
                    {
                        await mybot.SendTextMessageAsync(chatid, "Hello\nWelcome to my bot", replyToMessageId: messageid);
                    }
                    else if (message.MessageId == lastHeroRequestId + 2)
                    {
                        var route = new Uri($"https://api.opendota.com/api/heroes");
                        using (var client = new HttpClient())
                        {
                            HttpResponseMessage response = await client.GetAsync(route);
                            Console.WriteLine(response);
                            string data = await response.Content.ReadAsStringAsync();
                            var heroes = JsonConvert.DeserializeObject<List<Hero>>(data);


                            foreach (var hero in heroes)
                            {
                                if (hero.Name.ToLower() == text.ToLower())
                                {
                                    await mybot.SendTextMessageAsync(chatid, $"{text}: is a {hero.AttackType} hero." + $"\n{text} " + " " + $"id in game : {hero.Id}" + $"\n{text}" + " " +$"roles : {hero.Roles[0]}" +"," + $"{hero.Roles[1]}", replyToMessageId: messageid);
                                }
                            }
                        }


                    }
                    else if (text.ToLower().Equals("/heroes"))
                    {
                        lastHeroRequestId = message.MessageId;
                        await mybot.SendTextMessageAsync(chatid, "Please Enter the Hero Name", replyToMessageId: messageid);
                    }


                }
            }

        }


    }

}






