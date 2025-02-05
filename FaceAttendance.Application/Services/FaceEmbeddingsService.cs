using FaceAttendance.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FaceAttendance.Domain.Entities;
using FaceEmbeddingsClassification;

namespace FaceAttendance.Application.Services
{
    public class FaceEmbeddingsService : IFaceEmbeddingsService
    {
        private readonly Embeddings _embeddings;

        public FaceEmbeddingsService()
        {
            _embeddings = new Embeddings(); // Initialize your Embeddings class
        }

        public void AddEmbedding(float[] vector, string label)
        {
            _embeddings.Add(vector, label);
        }

        public void RemoveEmbedding(int index)
        {
            _embeddings.Remove(index);
        }

        public (string, float) GetLabelByDistance(float[] vector)
        {
            return _embeddings.FromDistance(vector);
        }

        public (string, float) GetLabelBySimilarity(float[] vector)
        {
            return _embeddings.FromSimilarity(vector);
        }
    }

}
