using Domain.Seedwork;

namespace Centrino.DomainLayer.MainBoundedContext.ValueObjects
{
    public class ChatFile:ValueObject<ChatFile>
    {
        public string Name { get; set; }
        
        public int Size { get; set; }
        
        public  string Type { get; set; }
        
        public string Audio{ get; set; }

        public int Duration { get; set; }
        
        public string Url { get; set; }

        public string Preview { get; set; }
        
        
        public ChatFile(string name,int size,string type,string audio,int duration,string url,string preview)
        {
            Name = name;

            Size = size;
            
            Type = type;

            Audio= audio;

            Duration= duration;

            Url = url;

            Preview = preview;



        }

        public ChatFile()
        {
        }
    }
}