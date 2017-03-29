using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Connector;
using System;
using System.Threading.Tasks;

namespace ScorableTest.Dialogs
{

    [Serializable]
    public class HelpDialog : IDialog<object>
    {
        private string value;

        public HelpDialog(string value)
        {
            this.value = value;
        }

        public async Task StartAsync(IDialogContext context)
        {
            // State transition - wait for 'start' message from user
            await context.PostAsync("[HelpDialog] You are now in the scorable dialog. Say something to complete the dialog.");
            context.Wait(MessageReceivedStart);
        }

        public async Task MessageReceivedStart(IDialogContext context, IAwaitable<IMessageActivity> argument)
        {
            await context.PostAsync("[HelpDialog] Done, returning to parent dialog.");
            context.Done<object>(new object());
        }
    }
}