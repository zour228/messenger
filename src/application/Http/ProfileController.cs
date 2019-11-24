using System.Threading.Tasks;
using Application.CQS.Profile;
using Application.CQS.Reservation;
using Application.CQS.Transaction;
using Application.CQS.User.Command;
using Application.CQS.User.Input;
using Common.Util;
using Microsoft.AspNetCore.Mvc;

namespace Application.Http
{
    [ApiController]
    [Route("[controller]")]
    public class ProfileController
    {
        private UserExtractor UserExtractor { get; }

        public ProfileController(UserExtractor userExtractor)
        {
            UserExtractor = userExtractor;
        }

        [HttpGet]
        public ProfileOutput GetProfile([FromServices] GetProfileQuery query)
        {
            return query.Execute();
        }

        [HttpGet("reservations")]
        public async Task<PaginatedData<ReservationsOutput>> GetCurrentUserReservations(
            [FromServices] GetAllReservationsQuery query,
            [FromQuery] ReservationsFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            var currentUser = await UserExtractor.ProvideUser();
            filter.UserId = currentUser.Id;

            return query.Execute(filter, pagination);
        }

        [HttpGet("transactions")]
        public async Task<PaginatedData<TransactionOutput>> GetCurrentUserTransactions(
            [FromServices] GetAllTransactionsQuery query,
            [FromQuery] TransactionFilter filter,
            [FromQuery] Pagination pagination
        )
        {
            var currentUser = await UserExtractor.ProvideUser();
            filter.UserId = currentUser.Id;

            return query.Execute(filter, pagination);
        }

        [HttpPut("update-names")]
        public void UpdateUserNames([FromServices] UpdateNamesCommand command, [FromBody] UpdateNamesInput input)
        {
            command.Execute(input);
        }

        [HttpPut("update-password")]
        public void UpdateUserPassword([FromServices] UpdatePasswordCommand command, [FromBody] UpdatePasswordInput input)
        {
            command.Execute(input);
        }
    }
}
