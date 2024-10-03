namespace RentingOutBooksService.Application.Application.Template.RentRequest
{
    public record UpdateRentRequest(
        Guid RentId,
        Guid? BookId,
        Guid? RentStatusId,
        int? CountRentDays,
        DateTime? RentStartDate
    );
}
