using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Internals;
using Microsoft.Bot.Builder.Internals.Fibers;
using Microsoft.Bot.Builder.Scorables.Internals;
using Microsoft.Bot.Connector;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ScorableTest.Dialogs
{
    public class HelpScorable : ScorableBase<IActivity, Tuple<string, string>, double>
    {
        private readonly IDialogStack _stack;

        public HelpScorable(IDialogStack stack)
        {
            SetField.NotNull(out _stack, nameof(stack), stack);

        }
        protected override Task DoneAsync(IActivity item, Tuple<string, string> state, CancellationToken token)
        {
            return Task.CompletedTask;
        }

        protected override double GetScore(IActivity item, Tuple<string, string> state)
        {
            return (state != null && state.Item1 == "help") ? 1 : 0;
        }

        protected override bool HasScore(IActivity item, Tuple<string, string> state)
        {
            return (state != null && state.Item1 == "help");
        }

        protected override Task PostAsync(IActivity item, Tuple<string, string> state, CancellationToken token)
        {
            var message = item as IMessageActivity;
            var dialog = new HelpDialog(state.Item1);
            var interruption = dialog.Void(_stack);
            _stack.Call(interruption, null);
            return Task.CompletedTask;
        }

        protected override async Task<Tuple<string, string>> PrepareAsync(IActivity item, CancellationToken token)
        {
            var message = item.AsMessageActivity();
            var messageText = message.Text;

            if (messageText == "help")
                return Tuple.Create<string, string>("help", null);
            return Tuple.Create<string, string>(null, null);
        }
    }
}