using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Chinchillada.BehaviourSelections.Utilities
{
    /// <summary>
    /// Static class containing extension methods for <see cref="Transform"/>.
    /// </summary>
    internal static class TransformExtensions
    {
        /// <summary>
        /// Finds components of the given type at all direct (first-layer) children of the <paramref name="transform"/>.
        /// </summary>
        public static IEnumerable<T> GetComponentsInDirectChildren<T>(this Transform transform)
        {
            return transform.GetComponentsAtLayer<T>(1);
        }

        /// <summary>
        /// Get all components of the give type at the given <paramref name="layer"/> under <paramref name="transform"/>.
        /// </summary>
        /// <typeparam name="T">The type of components we want.</typeparam>
        /// <param name="transform">The we're looking in the children of.</param>
        /// <param name="layer">The layer we want to get the components on.</param>
        /// <returns>All components of the given type at the given layer under the behaviour.</returns>
        public static IEnumerable<T> GetComponentsAtLayer<T>(this Transform transform, int layer)
        {
            //Get the layer.
            IEnumerable<Transform> layerTransforms = transform.GetChildLayer(layer);
            return layerTransforms.SelectMany(transformInLayer => transformInLayer.GetComponents<T>());
        }

        /// <summary>
        /// Gets the children transforms at the given layer in the transform tree under this <paramref name="transform"/>.
        /// </summary>
        /// <param name="transform">The transform we want children from.</param>
        /// <param name="layer">The layer we want.</param>
        /// <returns></returns>
        public static IEnumerable<Transform> GetChildLayer(this Transform transform, int layer = 1)
        {
            //Start with current transform.
            List<Transform> currentLayer = new List<Transform> { transform.transform };

            //Iterate through layers.
            for (int layerIndex = 0; layerIndex < layer; layerIndex++)
            {
                List<Transform> nextLayer = new List<Transform>();

                //Get all the children of all transforms in the current layer.
                foreach (Transform transformInLayer in currentLayer)
                {
                    //Get all children.
                    int childCount = transformInLayer.childCount;
                    for (int childIndex = 0; childIndex < childCount; childIndex++)
                    {
                        Transform child = transformInLayer.GetChild(childIndex);
                        nextLayer.Add(child);
                    }

                    //Set as current layer.
                    currentLayer = nextLayer;
                }
            }

            return currentLayer;
        }
    }
}
