namespace CreateNewConversationBot
{
    using System;
    using Microsoft.Bot.Builder.FormFlow;

    [Serializable]
    public class Survey
    {
        [Prompt("Client name:")]
        public string ClientName { get; set; }

        [Prompt("Matter name:")]
        public string MatterName { get; set; }

        [Prompt("Assign to:")]
        public string AssignTo { get; set; }
    }
}