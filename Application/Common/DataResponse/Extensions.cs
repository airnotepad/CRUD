using System;
using System.Text;

namespace Application.Common.DataResponse
{
    public static class ActResultExtensions
    {
        public static IHaveDataObject? AddInfo(this ActResult source, string message)
        {
            source.Metadata = new Metadata(source, message);
            return source.Metadata;
        }

        public static IHaveDataObject? AddSuccess(this ActResult source, string message)
        {
            source.Metadata = new Metadata(source, message, MetadataType.Success);
            return source.Metadata;
        }

        public static IHaveDataObject? AddWarning(this ActResult source, string message)
        {
            source.Metadata = new Metadata(source, message, MetadataType.Warning);
            return source.Metadata;
        }

        public static IHaveDataObject? AddError(this ActResult source, string message)
        {
            source.Metadata = new Metadata(source, message, MetadataType.Error);
            return source.Metadata;
        }

        public static IHaveDataObject? AddError(this ActResult source, Exception exception)
        {
            source.Exception = exception;
            source.Metadata = new Metadata(source, exception?.Message, MetadataType.Error);
            return source.Metadata;
        }

        public static IHaveDataObject? AddError(this ActResult source, string message, Exception exception)
        {
            source.Exception = exception;
            source.Metadata = new Metadata(source, message, MetadataType.Error);
            return source.Metadata;
        }

        public static string GetMetadataMessages(this ActResult source)
        {
            if (source == null) throw new ArgumentNullException();

            var sb = new StringBuilder();
            if (source.Metadata != null)
            {
                sb.AppendLine($"{source.Metadata.Message}");
            }
            return sb.ToString();

        }
    }
}