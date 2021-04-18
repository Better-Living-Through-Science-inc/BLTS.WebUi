namespace BLTS.WebUi.DtoModels
{
    public interface IDeleteDtoEntity<TPrimaryKey>
    {
        TPrimaryKey Id { get; set; }
        bool IsSoftDelete { get; set; }
    }
}