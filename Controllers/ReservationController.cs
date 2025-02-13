using API5.Models;
using API5.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace API5.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository _repository;

        public ReservationController(IRepository repository)
        {
            _repository = repository;
        }

        // Lấy danh sách Reservation
        [HttpGet]
        public IActionResult GetReservations()
        {
            return Ok(_repository.Reservations);
        }

        // Lấy 1 Reservation theo ID
        [HttpGet("{id}")]
        public IActionResult GetReservation(int id)
        {
            var reservation = _repository[id];
            if (reservation == null)
            {
                return NotFound();
            }
            return Ok(reservation); //200
        }

        // Thêm mới Reservation
        [HttpPost]
        public IActionResult AddReservation([FromBody] Reservation reservation)
        {
            if (reservation == null)
            {
                return BadRequest();
            }
            _repository.AddReservation(reservation);
            //201
            return CreatedAtAction(nameof(GetReservation), new { id = reservation.Id }, reservation);
        }

        // Cập nhật Reservation
        [HttpPut("{id}")]
        public IActionResult UpdateReservation(int id, [FromBody] Reservation reservation)
        {
            if (reservation == null || reservation.Id != id)
            {
                return BadRequest(); //400
            }
            var existingReservation = _repository[id];
            if (existingReservation == null)
            {
                return NotFound(); //404`
            }
            _repository.UpdateReservation(reservation);
            //204
            return NoContent();
        }

        // Xóa Reservation
        [HttpDelete("{id}")]
        public IActionResult DeleteReservation(int id)
        {
            var existingReservation = _repository[id];
            if (existingReservation == null)
            {
                return NotFound();
            }
            _repository.DeleteReservation(id);
            //204
            return NoContent();
        }
    }
}
