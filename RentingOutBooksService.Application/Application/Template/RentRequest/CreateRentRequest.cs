namespace RentingOutBooksService.Application.Application.Template.RentRequest
{
    public record CreateRentRequest(
        string FIO,
        Guid BookId,
        int CountRentDays,
        DateTime RentStartDate
    );
}
