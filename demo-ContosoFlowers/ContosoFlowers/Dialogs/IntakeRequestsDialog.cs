namespace ContosoFlowers.Dialogs
{
    using System;
    using System.Text;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Connector;
    using System.Threading;

    [Serializable]
    public class IntakeRequestsDialog : IDialog<IMessageActivity>
    {
        public Task StartAsync(IDialogContext context)
        {
            var reply = context.MakeMessage();
            var text = context.Activity.AsMessageActivity().Text.ToLower();

            if (text.Contains("requests"))
            {
                reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
                reply.Attachments = new List<Attachment>()
                {
                    GetRequestState212(),
                    GetRequestState213(),
                    GetRequestState214()
                };
            }
            else
            {
                if (text.Contains("ibm") || text.Contains("212"))
                {
                    reply.Attachments = new List<Attachment>() { GetRequestState212() };
                }
                else if (text.Contains("apple") || text.Contains("213"))
                {
                    reply.Attachments = new List<Attachment>() { GetRequestState213() };
                }
                else if (text.Contains("intapp") || text.Contains("214"))
                {
                    reply.Attachments = new List<Attachment>() { GetRequestState214() };
                }
            }

            context.PostAsync(reply);
            context.Done(reply);
            return Task.CompletedTask;
        }

        //private async Task MessageReceivedAsync(IDialogContext context, IAwaitable<IMessageActivity> result)
        //{
        //    var reply = context.MakeMessage();
        //    var text = context.Activity.AsMessageActivity().Text.ToLower();

        //    if (text.Contains("requests"))
        //    {
        //        reply.AttachmentLayout = AttachmentLayoutTypes.Carousel;
        //        reply.Attachments = new List<Attachment>()
        //        {
        //            GetRequestState212(),
        //            GetRequestState213(),
        //            GetRequestState214()
        //        };
        //    }
        //    else
        //    {
        //        if (text.Contains("ibm") || text.Contains("212"))
        //        {
        //            reply.Attachments = new List<Attachment>() { GetRequestState212() };
        //        }
        //        else if (text.Contains("apple") || text.Contains("213"))
        //        {
        //            reply.Attachments = new List<Attachment>() { GetRequestState213() };
        //        }
        //        else if (text.Contains("intapp") || text.Contains("214"))
        //        {
        //            reply.Attachments = new List<Attachment>() { GetRequestState214() };
        //        }
        //    }
                        
        //    await context.PostAsync(reply);
        //    context.Done(reply);
        //}

        private static Attachment GetRequestState212()
        {
            var title = string.Format("{0}# {1}", "Request", "212");

            var sb = new StringBuilder();
            sb.AppendFormat("042 IBM Inc");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("001 Legal support service");
            var subtitle = sb.ToString();

            sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", "Created", "31/08/2017");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Assigned to", "Administrator");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Current State", "AML");
            var text = sb.ToString();

            var image = new CardImage(url: "https://intapphack2017v3.blob.core.windows.net:443/imgs/r212.png");

            var actions = new List<CardAction>()
            {
                new CardAction(ActionTypes.OpenUrl, "Go To Request", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/212"),
                new CardAction(ActionTypes.OpenUrl, "Status", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/212")
            };

            return GetThumbnailCard(title, subtitle, "", image, actions);
        }

        private static Attachment GetRequestState213()
        {
            var title = string.Format("{0}# {1}", "Request", "213");

            var sb = new StringBuilder();
            sb.AppendFormat("011 Apple Inc");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("003 Super important matter");
            var subtitle = sb.ToString();

            sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", "Created", "31/08/2017");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Assigned to", "Administrator");
            sb.Append(Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Current State", "Preliminary Review");
            var text = sb.ToString();

            var image = new CardImage(url: "https://intapphack2017v3.blob.core.windows.net:443/imgs/r213.png");

            var actions = new List<CardAction>()
            {
                new CardAction(ActionTypes.OpenUrl, "Go To Request", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/213"),
                new CardAction(ActionTypes.OpenUrl, "Status", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/213")
            };

            return GetThumbnailCard(title, subtitle, "", image, actions);
        }

        private static Attachment GetRequestState214()
        {
            var title = string.Format("{0}# {1}", "Request", "214");

            var sb = new StringBuilder();
            sb.AppendFormat("012 Intapp Inc");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("023 Mavericks the best");
            var subtitle = sb.ToString();

            sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}", "Created", "31/08/2017");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Assigned to", "Administrator");
            sb.Append(Environment.NewLine + Environment.NewLine);
            sb.AppendFormat("{0}: {1}", "Current State", "CS");
            var text = sb.ToString();

            var image = new CardImage(url: "https://intapphack2017v3.blob.core.windows.net:443/imgs/r214.png");

            var actions = new List<CardAction>()
            {
                new CardAction(ActionTypes.OpenUrl, "Go To Request", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/214"),
                new CardAction(ActionTypes.OpenUrl, "Status", value: "https://wilco1.opendev.intapp.com/app/app/index.html#/requests/214")
            };

            return GetThumbnailCard(title, subtitle, "", image, actions);
        }
        
        private static Attachment GetHeroCard(string title, string subtitle, string text, CardImage cardImage, List<CardAction> cardActions)
        {
            var heroCard = new HeroCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                //Images = new List<CardImage>() { cardImage },
                Buttons = cardActions,
            };

            return heroCard.ToAttachment();
        }

        private static Attachment GetThumbnailCard(string title, string subtitle, string text, CardImage cardImage, List<CardAction> cardActions)
        {
            var heroCard = new ThumbnailCard
            {
                Title = title,
                Subtitle = subtitle,
                Text = text,
                Images = new List<CardImage>() { cardImage },
                Buttons = cardActions,
            };

            return heroCard.ToAttachment();
        }
                
    }
}
