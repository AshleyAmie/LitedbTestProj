using NUnit.Framework;
using LitedbTestProj.Models;
using System.Collections.Generic;
using LitedbTestProj.Repos;
using System.Linq;
using System;
using LiteDB;
using System.IO;

namespace LitedbNUnitTests
{
    public class TextureTests
    {
        private List<TextureModel> textures = new List<TextureModel>();
        private ITextureRepo repo;
        private LiteDBContext context;
        private LiteDatabase db;
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void AddTextureTest()
        {
            //Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            using (db = context.litedb)
            {
                TextureModel texture = new TextureModel()
                {
                    Name = "Pink",
                    IsGrained = false,
                    RealWorldHeight = 15,
                    RealWorldWidth = 15
                };

                // Act
                repo.AddTexture(texture);

                // Assert
                Assert.AreEqual("Pink", repo.Textures.FindById(texture.ID).Name);
                Assert.AreEqual(4, repo.Textures.Count());

                Console.WriteLine("ID: " + texture.ID + " Name: " + texture.Name +
                    " Width: " + texture.RealWorldWidth + " Height: " + texture.RealWorldHeight);

            }

            // Clean Up
            DeleteTexturesTest();
        }

        [Test]
        public void DeleteTexturesTest()
        {
            //Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            bool delete = false;
            using (db = context.litedb)
            {
                TextureModel texture = new TextureModel()
                {
                    Name = "Pink",
                    IsGrained = false,
                    RealWorldHeight = 15,
                    RealWorldWidth = 15
                };

                // Act
                repo.AddTexture(texture);

                // Act
                delete = DeleteTextures(repo.Textures);

                //Assert
                Assert.IsTrue(delete);
            }
        }

        public bool DeleteTextures(ILiteCollection<TextureModel> textures)
        {
            int count = textures.Count();
            int deletedcount = textures.DeleteAll();
            if (deletedcount == count)
                return true;
            else
                return false;
        }
    }
}