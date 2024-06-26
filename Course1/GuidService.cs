﻿using Course1.Properties.Services;

namespace Course1
{
    public class GuidService : IResponseFormatter
    {
        private Guid _guid = Guid.NewGuid();
        public async Task Format(HttpContext context, string content)
        {
            await context.Response.WriteAsync($"Guid {_guid}\n {content}");
        }
    }
}
