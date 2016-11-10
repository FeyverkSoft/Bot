using System;
using System.Collections.Generic;
using ImgComparer.Neuro.Interface;

namespace ImgComparer.Neuro.Domain
{
    [Serializable]
    public class SaveInfo
    {
        public List<Dictionary<String, INeuron>> Neurons { get; set; }

        public Int32 X { get; set; }

        public Int32 Y { get; set; }
    }
}
