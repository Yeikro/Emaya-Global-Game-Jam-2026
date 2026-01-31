using UnityEngine;

/// <summary>
/// Controla un blendshape (shapekey) según la apertura vertical de la boca
/// obtenida desde FaceDataReceiver.
/// </summary>
[ExecuteAlways]
public class MouthShapeKeyController : MonoBehaviour
{
    [Header("Referencias")]
    public FaceDataReceiver receiver;
    public SkinnedMeshRenderer meshRenderer;

    [Header("Configuración del ShapeKey")]
    public int shapeKeyIndex = 0;   // índice del blendshape a modificar
    public int shapeKeyIndex2 = 1;   // índice del blendshape a modificar
    [Range(0.1f, 2f)]
    public float factorDivisor = 1.0f; // para ajustar sensibilidad
    [Range(0.1f, 2f)]
    public float factorDivisor2 = 1.0f; // para ajustar sensibilidad
    public bool usarDatosNormalizados = true;

    [Header("Debug")]
    public float distanciaHorizontal;
    public float distanciaVertical;
    public float valorShapeKey;
    public float valorShapeKey2;

    public float valorInicial;
    public float valorInicial2;
    private void Update()
    {
        if (receiver == null || meshRenderer == null)
            return;

        Vector3[] boca = usarDatosNormalizados ? receiver.bocaNormalizada : receiver.boca;
        if (boca == null || boca.Length < 4)
            return;

        // Puntos de la boca
        Vector3 izquierda = boca[0]; // comisura izquierda
        Vector3 derecha = boca[1];   // comisura derecha
        Vector3 arriba = boca[2];    // labio superior
        Vector3 abajo = boca[3];     // labio inferior

        // Distancias
        distanciaHorizontal = Vector3.Distance(izquierda, derecha);
        distanciaVertical = Vector3.Distance(arriba, abajo);

        // Normalizar valor vertical por el divisor
        valorShapeKey = Mathf.Clamp01((distanciaVertical / factorDivisor))*100f-valorInicial;
        valorShapeKey2 = Mathf.Clamp01((distanciaHorizontal / factorDivisor2))*100f-valorInicial2;

        // Aplicar al shapekey
        meshRenderer.SetBlendShapeWeight(shapeKeyIndex, valorShapeKey);
        meshRenderer.SetBlendShapeWeight(shapeKeyIndex2, valorShapeKey2);
    }

    private void OnDrawGizmos()
    {
        if (receiver == null) return;

        Vector3[] boca = usarDatosNormalizados ? receiver.bocaNormalizada : receiver.boca;
        if (boca == null || boca.Length < 4) return;

        Gizmos.color = Color.red;
        foreach (var p in boca)
            Gizmos.DrawSphere(transform.position + p, 0.002f);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position + boca[2], transform.position + boca[3]); // vertical
        Gizmos.DrawLine(transform.position + boca[0], transform.position + boca[1]); // horizontal
    }
}
