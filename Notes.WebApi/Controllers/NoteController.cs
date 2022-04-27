using AutoMapper;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using Notes.Application.Notes.Queries.GetNoteList;
using Notes.Application.Notes.Queries.GetNoteDetails;
using Notes.Application.Notes.Commands.CreateNote;
using Notes.Application.Notes.Commands.UpdateNote;
using Notes.Application.Notes.Commands.DeleteNote;
using Notes.WebApi.Models;


namespace Notes.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class NoteController : BaseController
    {
        private readonly IMapper _mapper;
        public NoteController(IMapper mapper) => _mapper = mapper;
        /// <summary>
        /// Gets the list of notes
        /// </summary>
        /// <remarks>
        /// Sample reques:
        /// Get /note
        /// </remarks>
        /// <returns>Returns NoteListVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<NoteListVm>> GetAll()
        {
            var query = new GetNoteListQuery
            {
                UserId = UserID
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }

        /// <summary>
        /// Gets the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/D34D349E-43B8-429E-BCA4-793C932FD580
        /// </remarks>
        /// <param name="id">Note id (guid)</param>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="400">If error</response>
        
        [HttpGet("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<NoteDetailsVm>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery
            {
                UserId = UserID,
                Id = id
            };
            var vm = await Mediator.Send(query);
            return Ok(vm);
        }
        /// <summary>
        /// Creates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDto">CreateNoteDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="400">If error</response>
        
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDto createNoteDto)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDto);
            command.UserId = UserID;
            var noteId = await Mediator.Send(command);
            return Ok(noteId);
        }
        /// <summary>
        /// Updates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "updated note title"
        /// }
        /// </remarks>
        /// <param name="updateNoteDto">UpdateNoteDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If erorr</response>
        
        [HttpPut]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update([FromBody] UpdateNoteDto updateNoteDto)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDto);
            command.UserId = UserID;
            await Mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Deletes the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the note (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="400">If error</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand
            {
                Id = id,
                UserId = UserID
            };

            await Mediator.Send(command);
            return NoContent();
        }
    }
}
