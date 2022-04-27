﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Application;
using Notes.Persistence;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;

namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new NotesDbContext(options);
            context.Database.EnsureCreated(); // db is created?
            context.Notes.AddRange(
               new Note
               {
                   DateCreation = DateTime.Today,
                   Details = "Details1",
                   EditDate = null,
                   Id = Guid.Parse("A6BB65BB-5AC2-4AFA-8A28-2616F675B825"),
                   Title = "Title1",
                   UserID = UserAId
               },
                new Note
                {
                    DateCreation = DateTime.Today,
                    Details = "Details2",
                    EditDate = null,
                    Id = Guid.Parse("909F7C29-891B-4BE1-8504-21F84F262084"),
                    Title = "Title2",
                    UserID = UserBId
                },
                new Note
                {
                    DateCreation = DateTime.Today,
                    Details = "Details3",
                    EditDate = null,
                    Id = NoteIdForDelete,
                    Title = "Title3",
                    UserID = UserAId
                },
                new Note
                {
                    DateCreation = DateTime.Today,
                    Details = "Details4",
                    EditDate = null,
                    Id = NoteIdForUpdate,
                    Title = "Title4",
                    UserID = UserBId
                }
            );
            context.SaveChanges();
            return context;
        }

        public static void Destroy(NotesDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Dispose();
        }
    }
}
