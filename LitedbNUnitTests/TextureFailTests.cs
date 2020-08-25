using NUnit.Framework;
using System;
using LiteDB;
using System.Collections.Generic;
using LitedbTestProj.Repos;
using LitedbTestProj.Models;

namespace LitedbNUnitTests
{
    class TextureFailTests
    {
        private ITextureRepo repo;
        private LiteDBContext context;
        private LiteDatabase db;


        /// <summary>
        /// Deletes the test-created repository of textures
        /// </summary>
        public void DeleteTextures()
        {
            repo.Textures.DeleteAll();
        }
    }
}
