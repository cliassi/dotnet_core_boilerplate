﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Chat
{
    interface ITypedHubClient
    {
        Task BroadcastMessage(string type, string payload);
    }
}
