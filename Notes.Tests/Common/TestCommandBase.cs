﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notes.Persistence;

namespace Notes.Tests.Common
{
    public class TestCommandBase
    {
        protected readonly NotesDbContext Context;

        public TestCommandBase()
        {
            Context = NotesContextFactory.Create();
        }
        public void Dispose()
        {
            NotesContextFactory.Destroy(Context);
        }
    }
}