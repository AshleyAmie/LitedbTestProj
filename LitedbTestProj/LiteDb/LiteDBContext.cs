using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using LiteDB;

public class LiteDBContext
{
    public LiteDatabase litedb;
    public string databasePath;
    public ConnectionString dbPath;
    public string FilePath;
    public ILiteCollection<TextureModel> Textures { get; set; }

    public LiteDBContext()
    {
        CreateTables();
    }

    // Creates the Texture table
    public void CreateTables()
    {
        databasePath = Path.Combine(@"C:\Users\Ashley Johansson\Documents\Intern\TestProj\LitedbTestProj\LitedbTestProj\TempDB");
        FilePath = Path.Combine(databasePath, "Litedb.db");
        bool createDefaults = false;

        // if the chosen directory path doesn't exist, let's make one
        if (!Directory.Exists(databasePath))
        {
            // Load in default values in to tables for texture directory
            createDefaults = true;
            Directory.CreateDirectory(databasePath);
        }

        dbPath = new ConnectionString(FilePath);
        // creates a new Litedb if it doesn't exist
        litedb = new LiteDatabase(FilePath);

        // Gets the Texture collection if it exists, otherwise, it creates one
        //Textures = litedb.GetCollection<TextureModel>("textures");
        //if (Textures.Count() <= 0)
        //{
        //    createDefaults = true;
        //}

        if (createDefaults)
        {
            var textures = litedb.GetCollection<TextureModel>("textures");
            LoadDefaultTextureDirectoryIntoDatabase(textures);
        }
    }

    public void LoadDefaultTextureDirectoryIntoDatabase(ILiteCollection<TextureModel> textures)
    {
        // Create some default textures
        TextureModel t1 = new TextureModel
        {
            Name = "Shiny",
            IsGrained = false,
            RealWorldHeight = 20,
            RealWorldWidth = 20
        };

        TextureModel t2 = new TextureModel
        {
            Name = "Woody",
            IsGrained = true,
            RealWorldHeight = 15,
            RealWorldWidth = 20
        };

        TextureModel t3 = new TextureModel
        {
            Name = "Splintered",
            IsGrained = true,
            RealWorldHeight = 20,
            RealWorldWidth = 15
        };


        textures.Insert(t1);
        textures.Insert(t2);
        textures.Insert(t3);
    }
}
