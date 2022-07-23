using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Bot.Builder;

public interface ITurnContext
{
    bool Responded { get; }
}
