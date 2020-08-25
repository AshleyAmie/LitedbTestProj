using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using LiteDB;

namespace LitedbTestProj.Repos
{
    public class TextureRepo : ITextureRepo
    {
        private LiteDBContext context;

        private ILiteCollection<TextureModel> textures;

        public TextureRepo(LiteDBContext c)
        {
            context = c;
            textures = context.Textures;
        }
        public ILiteCollection<TextureModel> Textures => textures;

        public void AddTexture(TextureModel texture)
        {
            if (ModelVerification(texture))
            {
                context.Textures.Insert(texture);
            }
        }

        public bool DeleteTexture(TextureModel texture)
        {
            bool deleted = false;
            if (ModelVerification(texture))
            {
                deleted = context.Textures.Delete(texture.ID);
            }
            return deleted;
        }

        public bool DeleteTextures(ILiteCollection<TextureModel> textures)
        {
            var totalCount = textures.Count();
            var deleteCount = context.Textures.DeleteAll();
            if (totalCount == deleteCount)
                return true;
            else return false;
        }

        public TextureModel GetTextureById(int id)
        {
            var texture = context.Textures.FindById(id);
            return texture;
        }

        public TextureModel GetTextureByName(string name)
        {
            var texture = context.Textures.FindOne(x => x.Name == name);
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

            updated = context.Textures.Update(texture);
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
