using System;
using MediatR;

namespace Notes.Application.Notes.Queries.GetNoteList
{
    class GetNoteListQuery : IRequest<NoteListVm>
    {
        public Guid UserId { get; set; }
    }
}
