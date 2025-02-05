using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FaceAttendance.Application.Interfaces
{
    public interface IFaceEmbeddingsService
    {
        void AddEmbedding(float[] vector, string label);
        void RemoveEmbedding(int index);
        (string, float) GetLabelByDistance(float[] vector);
        (string, float) GetLabelBySimilarity(float[] vector);
    }
}
