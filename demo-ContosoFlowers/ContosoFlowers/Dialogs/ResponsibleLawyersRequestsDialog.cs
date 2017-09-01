using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace ContosoFlowers.Dialogs
{
    [Serializable]
    public class ResponsibleLawyersRequestsDialog : IDialog<string>
    {

        public Task StartAsync(IDialogContext context)
        {
            //context.Wait(this.MessageReceivedAsync);
            //context.Wait(MessageReceivedAsync);

            var reply = context.MakeMessage();
            var message = context.Activity.AsMessageActivity().Text;
            reply.AttachmentLayout = AttachmentLayoutTypes.List;
            reply.Attachments = new List<Attachment>() { GetLaryPerson(), GetRobertPerson() };
            context.PostAsync(reply);
            context.Done("");

            return Task.CompletedTask;
        }

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        //{

        //    var reply = context.MakeMessage();
        //    var message = context.Activity.AsMessageActivity().Text;
        //    reply.AttachmentLayout = AttachmentLayoutTypes.List;
        //    reply.Attachments = new List<Attachment>() { GetLaryPerson(), GetRobertPerson() };
        //    await context.PostAsync(reply);
        //    //context.EndConversation("");
        //    context.Done(message);
        //}

        private static Attachment GetLaryPerson()
        {
            var title = "Larry ...Whatever";

            var sb = new StringBuilder();
            sb.AppendFormat("Law School:\t University of Georgia School of Law");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Recognized since:\t 2006");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Phone:\t (404) 555 01 51");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Languages:\t English & Spanish");
            var subtitile = sb.ToString();

            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitile,
                Text = String.Empty,
                Images = new List<CardImage>
                { new CardImage("https://www.intapp.com/media/zoo/images/dan_harsell_250992389d8c41d9f47bd0ab74906cbf.png") }
            };
            return heroCard.ToAttachment();

        }
        private static Attachment GetRobertPerson()
        {
            var title = "Robert Latrick";
            var sb = new StringBuilder();
            sb.AppendFormat("Law School:\t American University Washington College of Law");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Recognized since:\t 2000");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Phone:\t (404) 785 01 52");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("Languages:\t English & French");
            var subtitile = sb.ToString();
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitile,
                Text = String.Empty,
                Images = new List<CardImage>
                { new CardImage("http://i.imgur.com/7dEjQH9.png") }
            };
            return heroCard.ToAttachment();
        }
    }
}