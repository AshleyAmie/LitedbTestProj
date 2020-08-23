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
            context.Textures.Insert(texture);
        }

        public TextureModel GetTextureById(int id)
        {
            throw new NotImplementedException();
        }

        public TextureModel GetTextureByName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
