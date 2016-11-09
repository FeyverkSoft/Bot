using System;
using Neuro.Interface;

namespace Neuro.Domain
{
    [Serializable]
    public class SaveStructure
    {
        public Int32 NeuronCount { get; set; }
        public Int32 ImageWidth { get; set; }
        public Int32 ImageHeight { get; set; }
        public IPerceptron Perceptron { get; set; }
    }
}
