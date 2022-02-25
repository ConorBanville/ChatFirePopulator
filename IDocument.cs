namespace ChatFirePoupulator 
{
    public abstract class IDocument
    {
        public abstract string Uid {get; set;}
        public abstract string Serialize();

        public abstract Dictionary<string, object> ToDocument();
    }
}