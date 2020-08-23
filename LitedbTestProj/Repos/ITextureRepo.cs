using LiteDB;
using System;
using System.Collections.Generic;
using System.Text;

namespace LitedbTestProj.Repos
{
    public interface ITextureRepo
    {
        ILiteCollection<TextureModel> Textures { get; }
        void AddTexture(TextureModel texture);
        TextureModel GetTextureById(int id);
        TextureModel GetTextureByName(string name);
    }
}
