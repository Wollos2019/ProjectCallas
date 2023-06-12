using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CallaciMaui.Helpers
{
    public class Answer
    {
        public int Id {  get; set; }
        public string Text { get; set; }
    }

    [JsonSerializable(typeof(List<Answer>))]
    internal sealed partial class AnswerContext : JsonSerializerContext
    {

    }
}
