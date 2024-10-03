namespace RentingOutBooksService.Application.Application.Template.RentRequest
{
    public record CreateRentAndClientRequest(
        string Name,
        string Surname,
        string Patronymic,
        string Phone,
        string Address,
        /*для создания книги*/
        Guid BookId,
        int CountRentDays,
        DateTime RentStartDate
    );
}
