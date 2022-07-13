namespace Application.Common.DataResponse
{
    public interface IMetadataMessage : IHaveDataObject
    {
        string Message { get; }
        object DataObject { get; }
    }
}