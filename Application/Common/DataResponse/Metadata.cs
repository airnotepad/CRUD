using System;

namespace Application.Common.DataResponse
{
    public enum MetadataType
    {
        Info,
        Success,
        Warning,
        Error
    }

    public class Metadata : IMetadataMessage
    {
        private readonly ActResult _source;

        public Metadata()
        {
            Type = MetadataType.Info;
        }

        public Metadata(ActResult source, string message) : this()
        {
            _source = source;
            Message = message;
        }

        public Metadata(ActResult source, string message, MetadataType type = MetadataType.Info)
        {
            Type = type;
            _source = source;
            Message = message;
        }

        public string Message { get; }
        public MetadataType Type { get; }
        public object DataObject { get; private set; }

        public void AddData(object data)
        {
            if (data is Exception exception && _source.Metadata == null)
            {
                _source.Metadata = new Metadata(_source, exception.Message);
            }
            else
            {
                _source.Metadata.DataObject = data;
            }
        }
    }
}