using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LiteDB;

namespace LitedbTestProj.Repos
{
    public class TextureRepo
    {
        private LiteDBContext context;
        private IGenericRepo<TextureModel> genericRepo;

        private ILiteCollection<TextureModel> textures;

        public TextureRepo(LiteDBContext c, IGenericRepo<TextureModel> repository)
        {
            genericRepo = repository;
            context = c;
            textures = context.litedb.GetCollection<TextureModel>("textures");
            if (textures.Count() == 0)
                context.LoadDefaultTextureDirectoryIntoDatabase(Textures);
            for (int i = 1; i <= textures.Count(); i++)
            {
                var t = textures.FindById(i);
                genericRepo.Insert(t);
            }

        }
        public ILiteCollection<TextureModel> Textures => genericRepo.GetAll();

        public void AddTexture(TextureModel texture)
        {
            if (ModelVerification(texture))
            {
                genericRepo.Insert(texture);
            }
        }

        public bool DeleteTexture(TextureModel texture)
        {
            bool deleted = false;
            if (ModelVerification(texture))
            {
                genericRepo.Delete(texture.ID);
                deleted = true;
            }
            return deleted;
        }

        public bool DeleteTextures(ILiteCollection<TextureModel> textures)
        {
            bool deleted = genericRepo.DeleteAll();
            if (deleted)
                return true;
            else return false;
        }

        public TextureModel GetTextureById(int id)
        {
            var texture = genericRepo.GetById(id);
            return texture;
        }

        public TextureModel GetTextureByName(string name)
        {
            var texture = genericRepo.GetAll().FindOne(x => x.Name == name);
            return texture;
        }

        public bool UpdateTexture(TextureModel texture, string keyVar, string valueVar)
        {
            bool updated;
            if (ModelVerification(texture))
            {
                if (keyVar != "Name" && keyVar != "name" &&
                 keyVar != "IsGrained" && keyVar != "isgrained" &&
                 keyVar != "RealWorldWidth" && keyVar != "realworldwidth" &&
                 keyVar != "RealWorldHeight" && keyVar != "realworldheight")
                {
                    throw new Exception();
                }
                else
                {
                    if (keyVar == "Name" || keyVar == "name")
                    {
                        texture.Name = valueVar;
                    }
                    else if (keyVar == "IsGrained" || keyVar == "isgrained")
                    {
                        if (valueVar == "true" || valueVar == "True") { texture.IsGrained = true; }
                        else if (valueVar == "false" || valueVar == "False") { texture.IsGrained = false; }
                        else { throw new Exception(); }
                    }
                    else if (keyVar == "RealWorldWidth" || keyVar == "realworldwidth")
                    {
                        var realWorldWidth = Convert.ToDouble(valueVar);
                        texture.RealWorldWidth = realWorldWidth;
                    }
                    else if (keyVar == "RealWorldHeight" || keyVar == "realworldheight")
                    {
                        var realWorldHeight = Convert.ToDouble(valueVar);
                        texture.RealWorldHeight = realWorldHeight;
                    }
                }
            }

            updated = genericRepo.Update(texture);

            return updated;
        }

        public static bool ModelVerification(TextureModel model)
        {
            bool isValid = false;
            if (true)
            {
                isValid = true;
            }
            return isValid;
        }
    }
}
