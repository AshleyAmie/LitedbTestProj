using NUnit.Framework;
using LitedbTestProj.Models;
using System.Collections.Generic;
using LitedbTestProj.Repos;
using System.Linq;
using System;
using LiteDB;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace LitedbNUnitTests
{
    /// <summary>
    /// Tests for Texture Model CRUD operations
    /// </summary>
    public class TextureTests
    {
        private ITextureRepo repo;
        private LiteDBContext context;
        private LiteDatabase db;

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


                // Clean Up
                DeleteTextures();
            }
        }

        [Test]
        public void DeleteTexturesTest()
        {
            //Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            bool delete;
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
                delete = repo.DeleteTextures(repo.Textures);

                //Assert
                Assert.IsTrue(delete);
            }
        }

        [Test]
        public void DeleteTextureTest()
        {
            //Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            bool delete;
            TextureModel t1;
            using (db = context.litedb)
            {
                TextureModel texture = new TextureModel()
                {
                    Name = "Pink",
                    IsGrained = false,
                    RealWorldHeight = 15,
                    RealWorldWidth = 15
                };
                t1 = context.Textures.FindById(1);

                // Act
                repo.AddTexture(texture);

                // Act
                delete = repo.DeleteTexture(t1);

                //Assert
                Assert.IsTrue(delete);

                //Clean Up
                DeleteTextures();
            }
        }

        [Test]
        public void GetTexturesTest()
        {
            // Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);

            // Act
            using (db = context.litedb)
            {
                var textures = repo.Textures;
                var textures2 = db.GetCollection<TextureModel>("textures");

                // Assert
                Assert.AreEqual(textures2.Count(), textures.Count());
                Assert.IsTrue(textures2.FindById(1).ToString() == textures.FindById(1).ToString());
                Assert.IsTrue(textures2.FindById(textures2.Count()).ToString() == textures.FindById(textures.Count()).ToString());
            }

        }

        [Test]
        public void GetTextureByIDTest()
        {
            // Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);

            // Act
            using (db = context.litedb)
            {
                var texture = repo.GetTextureById(1);
                var texture2 = context.Textures.FindById(1);

                // Assert
                Assert.AreEqual(texture2.Name, texture.Name);
            }
        }

        [Test]
        public void GetTextureByNameTest()
        {
            // Arrange
            context = new LiteDBContext();
            repo = new TextureRepo(context);

            // Act
            using (db = context.litedb)
            {
                var texture = repo.GetTextureByName("Shiny");
                var texture2 = context.Textures.FindOne(x => x.Name == "Shiny");

                // Assert
                Assert.AreEqual(texture2.Name, texture.Name);
                Assert.AreEqual(texture2.ID, texture.ID);
            }

        }

        [Test]
        public void UpdateTextureNameTest()
        {
            // Arrange 
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            TextureModel t2, t1;
            bool updated;

            // Act
            using (db = context.litedb)
            {
                t2 = repo.GetTextureById(1);
                t1 = context.Textures.FindById(1);

                updated = repo.UpdateTexture(t2, "Name", "New Name");

                // Assert
                Assert.AreNotEqual(t1.Name, t2.Name);
                Assert.IsTrue(updated);

                Console.WriteLine("t1: " + t1.Name + "  " + "t2: " + t2.Name);
            }

            // CleanUp
            DeleteTexturesTest();

        }

        [Test]
        public void UpdateTextureIsGrainedTest()
        {
            // Arrange 
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            TextureModel t2, t1;
            bool updated;

            // Act
            using (db = context.litedb)
            {
                t2 = repo.GetTextureById(1);
                t1 = context.Textures.FindById(1);

                updated = repo.UpdateTexture(t2, "IsGrained", "true");

                // Assert
                Assert.IsTrue(t2.IsGrained);
                Assert.IsFalse(t1.IsGrained);
                Assert.IsTrue(updated);

                Console.WriteLine("t1: " + t1.Name + "  " + t1.IsGrained + " " + 
                    "t2: " + t2.Name + " " + t2.IsGrained.ToString());
            }

            // CleanUp
            DeleteTexturesTest();

        }

        [Test]
        public void UpdateTextureRealWorldWidthTest()
        {
            // Arrange 
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            TextureModel t2, t1;
            bool updated;

            // Act
            using (db = context.litedb)
            {
                t2 = repo.GetTextureById(1);
                t1 = context.Textures.FindById(1);

                updated = repo.UpdateTexture(t2, "realworldwidth", "12");

                // Assert
                Assert.IsTrue(updated);
                Assert.IsTrue(12 == t2.RealWorldWidth);
                Assert.AreNotEqual(t1.RealWorldWidth, t2.RealWorldWidth);
                Assert.IsTrue(20 == t1.RealWorldWidth);

                Console.WriteLine("t1: " + t1.Name + "  " + t1.RealWorldWidth.ToString() + " " +
                    "t2: " + t2.Name + " " + t2.RealWorldWidth.ToString());
            }

            // CleanUp
            DeleteTexturesTest();
        }

        [Test]
        public void UpdateTextureRealWorldHeightTest()
        {
            // Arrange 
            context = new LiteDBContext();
            repo = new TextureRepo(context);
            TextureModel t2, t1;
            bool updated;

            // Act
            using (db = context.litedb)
            {
                t2 = repo.GetTextureById(1);
                t1 = context.Textures.FindById(1);

                updated = repo.UpdateTexture(t2, "realworldheight", "12");

                // Assert
                Assert.IsTrue(updated);
                Assert.IsTrue(12 == t2.RealWorldHeight);
                Assert.AreNotEqual(t1.RealWorldHeight, t2.RealWorldHeight);
                Assert.IsTrue(20 == t1.RealWorldHeight);

                Console.WriteLine("t1: " + t1.Name + "  " + t1.RealWorldHeight.ToString() + " " +
                    "t2: " + t2.Name + " " + t2.RealWorldHeight.ToString());

                // CleanUp
                DeleteTextures();
            }
        }

        /// <summary>
        /// Deletes the test-created repository of textures
        /// </summary>
        public void DeleteTextures()
        {
            repo.Textures.DeleteAll();
        }
    }
}