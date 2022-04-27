using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Notes.Tests.Common;
using Notes.Application.Notes.Commands.CreateNote;
using Xunit;
using NUnit.Framework;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            //Arrange
            var handler = new CreateNoteCommandHandler(Context);
            var noteName = "note name";
            var noteDetail = "note details";

            //Act
            var noteId = await handler.Handle(
                new CreateNoteCommand
                {
                    Title = noteName,
                    Details = noteDetail,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            //Assert
            Assert.NotNull(
                await Context.Notes.SingleOrDefaultAsync(note => note.Id == noteId && note.Title == noteName && note.Details == noteDetail));
        }
    }
}
