namespace BLTS.WebUi.DtoModels
{
    public class OperationalConfigurationCreateDto
    {
        public long ApplicationId { get; set; }
        public string PropertyName { get; set; }
        public dynamic PropertyValue { get; set; }
        public bool IsUpdateDatabase { get; set; }
    }
}
