using System;
using System.Collections.Generic;
using System.Text;
using LiteDB;
using System.Linq;
using System.Threading.Tasks;

public class TextureModel
{
    private string _texture;
    [BsonId]
    public int ID { get; set; }
    public string Name { get; set; }
    public byte[] ImageBits { get; set; }
    public bool IsGrained { get; set; }
    public double RealWorldHeight { get; set; }
    public double RealWorldWidth { get; set; }
}
