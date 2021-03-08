using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BlazorFluentUI
{
    public interface IComponentStyle
    {
        bool ClientSide { get; }

        ICollection<ILocalCSSheet> LocalCSSheets { get; set; }

        ICollection<string> GlobalCSRules { get; set; }

        string PrintRule(IRule rule);

        void SetDisposedAction();
    }
}
