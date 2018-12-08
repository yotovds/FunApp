using System;
using System.Collections.Generic;
using System.Text;

namespace FunApp.Services.Models.Jokes
{
    public class CreateJokeInputModel
    {
        public string Content { get; set; }

        public int CategoryId { get; set; }
    }
}
