using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorFluentUI
{
    public partial class BFULocalCS : ComponentBase, ILocalCSSheet, IDisposable
    {
        private string css;
        private ICollection<IRule> rules;

        public ICollection<IRule> Rules
        {
            get => rules;
            set
            {
                if (value == rules)
                {
                    return;
                }
                rules = value;
                RulesChanged.InvokeAsync(value);
            }
        }

        [Parameter] public EventCallback<ICollection<IRule>> RulesChanged { get; set; }

        protected override async Task OnInitializedAsync()
        {

            await base.OnInitializedAsync();
        }

        //Private functionality

        private void SetSelectorNames()
        {
            foreach (var rule in rules)
            {
                var innerRule = rule as Rule;
                if (innerRule.Selector.GetType() == typeof(IdSelector) || innerRule.Selector.GetType() == typeof(MediaSelector))
                    continue;
                if (string.IsNullOrWhiteSpace(innerRule.Selector.SelectorName))
                {
                    innerRule.Selector.SelectorName = $"css-{}"
                }
            }
        }

        public void Dispose()
        {

        }
    }
}
